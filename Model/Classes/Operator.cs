using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Model.Classes;
using Model.Interface;
using SharedDbWorker;
using SharedDbWorker.Classes;
using Dapper;

namespace Model
{
    // user_tab
    public class Operator : ILoadData
    {
        public Operator()
        {
            AccessList = new List<AccessRequirements>();
            IsLoading = false;
            IsLoaded = false;
        }
        public Operator(int id) : this()
        {
            LoadData(id);
        }

        public long Id { get; set; }

        public long GetId()
        {
            return Id;
        }
        public long PatientId { get; set; }

        public long DoctorId { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public long? LPUId { get; set; }
        public string LPUName { 
            get {
                
                return LPUId.HasValue ? CodifiersHelper.GetLPUName(LPUId.Value) : string.Empty ;
                } 
        }
        public DateTime? ExpirationDate {get;set;}
        public String Comment { get; set; }
        
        /// <summary>
        /// Кто изменил данные
        /// </summary>
        public long? OperatorId { get; set; }
        
        /// <summary>
        /// Оператор, который вносит изменения
        /// </summary>
        public long? LoggedOperator {get;set;}

        public IEnumerable<AccessRight> Permissions { get; set; }

        public List<AccessRequirements> AccessList { get; private set; }

        public void AddUserAccess(AccessRequirements accessRequirements)
        {
            if (AccessList.Any(t=> t.Code == accessRequirements.Code))
                return;

            AccessList.Add(accessRequirements);
        }
        public event EventHandler Loading;
        public event EventHandler Loaded;
        public void OnLoading()
        {
            IsLoading = true;
            if (Loading != null)
            {
                Loading(this, null);
            }
        }

        public void OnLoaded()
        {
            IsLoading = false;
            if (Loaded != null)
            {
                Loaded(this, null);
            }
        }
        public bool IsLoading { get; private set; }
        public bool IsLoaded { get; private set; }

        public bool LoadData(long id)
        {
            OnLoading();

            Id = id;
            var loadresult = false;
            using (var dbWorker = new DbWorker())
            {
                var fioQuery = new DbQuery("fioQuery");
                fioQuery.Sql =
                    @"SELECT d.fam, d.nam, d.mid, d.dan_id 
                      FROM dan_tab d 
                      LEFT JOIN users_tab u ON u.dan_id = d.id 
                      WHERE u.id = @id";
                fioQuery.AddParamWithValue("@id", Id);

                var result = dbWorker.GetResult(fioQuery);
                if (result != null)
                {
                    this.LastName = DbResult.GetString(result.Fields[0], "");
                    this.FirstName = DbResult.GetString(result.Fields[1], "");
                    this.MiddleName = DbResult.GetString(result.Fields[2], "");
                    this.PatientId = DbResult.GetNumeric(result.Fields[3], -1);
                    var accessQuery = new DbQuery("accessQuery");
                    accessQuery.Sql =
                        @"SELECT DISTINCT code_access, name, description 
                            FROM users_access_view WHERE users_id = @id";
                    accessQuery.AddParamWithValue("@id", Id);

                    var codes = dbWorker.GetResults(accessQuery);
                    if (codes != null)
                    {
                        foreach (var code in codes)
                        {
                            var userAccess = new AccessRequirements();
                            userAccess.Code = DbResult.GetInt(code.Fields[0], -1);
                            userAccess.Name = DbResult.GetString(code.Fields[1], "");
                            userAccess.Description = DbResult.GetString(code.Fields[2], "");
                        }
                        loadresult = true;
                    }

                    //запрашиваем доп инфу
                    var lpuId = dbWorker.Connection.Query<long>("select sp_lpu_id from users_tab where id=@Id",
                                new { Id = Id}).FirstOrDefault();
                    if (lpuId!=default(long))
                    {
                        LPUId = lpuId;
                    }
                }

                DoctorId = getDoctorId();
            }


            Permissions = new OperatorRepository().GetUserPermissions(Id);

            IsLoaded = loadresult;


            OnLoaded();

            return loadresult;
        }

        

        public bool LoadData(DbResult result)
        {
            throw new NotImplementedException();
        }

        private long getDoctorId()
        {
            long id = -1;
            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetDoctorId");
                q.Sql = "Select id from public.doctor_tab where users_id = @user;";
                q.AddParamWithValue("@user", Id);
                var result = db.GetScalarResult(q);
                id = DbResult.GetNumeric(result, -1);
            }
            return id;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}{2}", this.LastName, string.IsNullOrEmpty(this.FirstName) ? string.Empty : this.FirstName[0] + ".",
                string.IsNullOrEmpty(this.MiddleName) ? string.Empty : this.MiddleName[0] + ".");
        }

        public string FIO { 
            get {
                return string.Format("{0} {1} {2}", this.LastName, string.IsNullOrEmpty(this.FirstName) ? string.Empty : this.FirstName,
                string.IsNullOrEmpty(this.MiddleName) ? string.Empty : this.MiddleName);
                } 
        }

    }

    public class AccessRequirements
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    
    public class OperatorRepository
    {
        public void AddOrUpdate(Operator oper)
        {
            var parameters = new DynamicParameters();
            string sql = string.Empty;
            if (!oper.ExpirationDate.HasValue)
            {
                oper.ExpirationDate = new DateTime(DateTime.Now.Year+30, DateTime.Now.Month,DateTime.Now.Day);
            }
            using(var db = new DbWorker())
            {
                parameters.Add("Login", oper.Login);
                parameters.Add("Password", oper.Password);
                parameters.Add("PatientId", oper.PatientId);
                parameters.Add("ExpDate", oper.ExpirationDate);
                parameters.Add("LPUId", oper.LPUId);
                parameters.Add("Comment", oper.Comment);

                if (!IsExist(oper))
                {
                    sql = @"insert into users_tab(
                                login,password,dan_id, 
                                expiration_date, sp_lpu_id,comment) 
                                values(@Login, md5(@Password), @PatientId,
                                    @ExpDate, @LPUId, @Comment)
                                returning id;
                                ";
                    var result = db.Connection.Query<long>(sql, parameters).FirstOrDefault();
                    if (result!=default(long))
                    {
                        oper.Id = (long)result;
                    }
                }
                else
                {
                    sql = @"update users_tab set 
                            login=@Login, 
                            password=md5(@Password), 
                            dan_id=@PatientId,
                            expiration_date=@ExpDate, 
                            sp_lpu_id=@LPUId, 
                            comment=@Comment
                            where id=@Id
                           ";
                    parameters.Add("Id", oper.Id);
                    var result = db.Connection.Execute(sql, parameters);
                }

                
                
            }
        }

        public bool IsExist(Operator oper)
        {
            if (oper.Id!=default(long) && oper.Id!=-1)
            {
                return true;
            }

            if (!string.IsNullOrEmpty(oper.Login))
            {
                using(var db=new DbWorker())
                {
                    var sql = @"select exists(select login from users_tab where login=@Login)";
                    var result = db.Connection.Query<bool>(sql, new { Login=oper.Login }).First();
                    if (result)
                    {
                        oper.Id = GetId(oper).Value;
                    }
                    return result;
                }
            }
            return false;
        }

        public long? GetId(Operator oper)
        {
            using(var db = new DbWorker())
            {
                var id = db.Connection.Query<long?>("select id from users_tab where login=@Login", new { Login = oper.Login }).FirstOrDefault();
                return id;
            }
        }

        public bool Validate(Operator oper, out string errMsg)
        {
            oper.Login=oper.Login.Trim();
            oper.Password = oper.Password.Trim();
            if (string.IsNullOrWhiteSpace(oper.Login))
            {
                errMsg = "Недопустимый логин";
                return false;
            }
            if (string.IsNullOrWhiteSpace(oper.Password))
            {
                errMsg = "Недопустимый пароль";
                return false;
            }

            if (oper.PatientId == default(long) || oper.PatientId<0)
            {
                errMsg = "Не указан пользователь";
                return false;
            }

            if (oper.LPUId == default(long) || oper.LPUId < 0)
            {
                errMsg = "Не указано учреждение пользователя";
                return false;
            }

            errMsg = null;
            return true;
        }

        /// <summary>
        /// Удаление пользователя путем изменения даты окончания
        /// </summary>
        /// <param name="oper"></param>
        public void Delete(Operator oper)
        {
            oper.ExpirationDate = DateTime.Now;
            AddOrUpdate(oper);
        }


        public IEnumerable<Operator> GetUsers(long personId)
        {
            using(var db=new DbWorker())
            {
                var sql = @"select 
                            u.id, u.login, u.expiration_date expirationdate,
                            u.dan_id patientid,
                            d.fam lastname, d.nam firstname, d.mid middlename
                            from users_tab u
                            join dan_tab d on d.dan_id=u.dan_id
                            where u.dan_id=@PersonId 
                            and u.expiration_date>now()
                           ";
                var result = db.Connection.Query<Operator>(sql, 
                            new { PersonId=personId });
                return result;
            }
        }

        public IEnumerable<Operator> GetAllUsers(long lpuId=0, bool showDeleted=false)
        {
            var parameters = new DynamicParameters();
            using (var db = new DbWorker())
            {
                var whereList = new List<String>();
                
                var sql = @"select 
                            u.id, u.login, u.expiration_date expirationdate,
                            u.dan_id patientid,
                            d.fam lastname, d.nam firstname, d.mid middlename,
                            u.sp_lpu_id lpuid
                            from users_tab u
                            join dan_tab d on d.dan_id=u.dan_id
                           ";

                if (lpuId!=0)
                {
                    whereList.Add("u.sp_lpu_id=@LPUId");
                    parameters.Add("LPUId", lpuId);
                }
                
                if (!showDeleted)
                {
                    whereList.Add("u.expiration_date>now()");
                }

                if (whereList.Count>0)
                {
                    sql += " where " + string.Join(" and ", whereList);
                    var result = db.Connection.Query<Operator>(sql, parameters);
                    return result;
                }else
                {
                    var result = db.Connection.Query<Operator>(sql, parameters);
                    return result;
                }
            }
        }

        

        public IEnumerable<AccessGroup> GetGroups()
        {
            using(var db = new DbWorker())
            {
                var sql = @" select id, name, description, expiration_date expirationdate
                             from groups_tab 
                             where  expiration_date>now()";
                var result = db.Connection.Query<AccessGroup>(sql);
                return result;

            }
        }

        public IEnumerable<AccessGroup> GetUserGroups(long operId)
        {
            using (var db = new DbWorker())
            {
                var sql = @" select g.id, name, g.description, g.expiration_date expirationdate, true isingroup
                             from groups_tab g
                             join groups_entry_tab ge on ge.groups_id=g.id
                             where  expiration_date>now() and ge.users_id=@OperId and  ge.id is not null";
                var result = db.Connection.Query<AccessGroup>(sql, new { OperId=operId });
                return result;
            }
        }


        public IEnumerable<AccessRight> GetRights()
        {
            using(var db = new DbWorker())
            {
                var sql = @"select id, name, parent_id parentid,
                            description 
                            from code_access_tab
                           ";
                var result = db.Connection.Query<AccessRight>(sql);
                return result;
            }
        }

        public IEnumerable<AccessRight> GetUserPermissions(long operId)
        {
            using (var db = new DbWorker())
            {
                var sql = @"SELECT DISTINCT code_access id, name, description 
                            FROM users_access_view WHERE users_id = @OperId
                           ";
                var result = db.Connection.Query<AccessRight>(sql, new { OperId=operId });
                return result;
            }
        }

        public IEnumerable<AccessRight> GetIndividualUserPerms(long operId)
        {
            var sql = @"select distinct a.id, a.name, a.parent_id parentid,
                        a.description, false isgroup 
                        from code_access_tab a
                        left join users_access_tab ua on ua.code_access_id=a.id
                        where ua.id is not null and ua.users_id=@OperId
                        ";
            using(var db = new DbWorker())
            {
                var result = db.Connection.Query<AccessRight>(sql, new { OperId=operId });
                return result;
            }
        }

        public IEnumerable<AccessRight> GetGroupUserPerms(long operId)
        {
            var sql = @"select a.id, a.name, a.parent_id parentid,
                        a.description, true isgroup
                        from code_access_tab a
                        left join (SELECT distinct ga.code_access_id
	                        from groups_access_tab ga
	                        left join groups_tab g on g.id=ga.groups_id
	                        left join groups_entry_tab ge on ge.groups_id=g.id 
                        where g.expiration_date>now() and ge.users_id=@OperId) sub on sub.code_access_id=a.id
                        where sub.code_access_id is not null
                        ";
            using (var db = new DbWorker())
            {
                var result = db.Connection.Query<AccessRight>(sql, new { OperId = operId });
                return result;
            }
        }

        public void AddUserToGroup(long userId, long groupId, long loggedUserId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("UserId", userId);
            parameters.Add("GroupId", groupId);
            using (var db = new DbWorker())
            {
                var sql = "select exists(select id from groups_entry_tab where groups_id=@GroupId and users_id=@UserId)";

                var result = db.Connection.Query<bool>(sql, parameters).First();

                if (!result)
                {
                    sql = @"insert into groups_entry_tab(operator_id, users_id, groups_id) 
                            values(@LoggedUserId,  @UserId, @GroupId)
                            ";
                    parameters.Add("LoggedUserId", loggedUserId);
                    db.Connection.Execute(sql, parameters);
                }
                
            }
        }

        public void RemoveUserFromGroup(long userId, long groupId,long loggedUserId)
        {
            var sql = @"delete from groups_entry_tab
                        where users_id=@OperId and groups_id=@GroupId
                       ";
            using (var db = new DbWorker())
            {
                var result = db.Connection.Execute(sql,
                    new { OperId = userId, GroupId = groupId });
                return;
            }
        }



    }
}
