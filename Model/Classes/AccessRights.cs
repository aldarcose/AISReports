using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedDbWorker;
using Dapper;

namespace Model.Classes
{
    /// <summary>
    /// Права доступа
    /// таб code_access_tab
    /// </summary>
    public class AccessRight
    {
        public long? Id {get;set;}
        /// <summary>
        /// Кто изменил
        /// </summary>
        public long? OperatorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long? ParentId { get; set; }
        public bool? IsGroup { get; set; }
        public bool? HasAccess { get; set; }
        public IEnumerable<AccessRight> Children { get; set; }
    }

    /// <summary>
    /// Группы прав
    /// </summary>
    public class AccessGroup
    {
        public long? Id { get; set; }
        public long? OperatorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? CanModify { get; set; }
        public DateTime? ExpirationDate {get;set;}
        /// <summary>
        ///  входит ли пользователь в группу
        /// </summary>
        public bool IsInGroup { get; set; }
    }

    public class AcccessGroupRight
    {
        public long? Id { get; set; }
        public long? OperatorId { get; set; }
        public long? GroupId { get;set; }
        public long? AccessRight { get; set; }
    }

    public class AcccessGroupEntry
    {
        public long? Id { get; set; }
        public long? OperatorId { get; set; }
        public long? UserId { get; set; }
        public long? GroupId { get; set; }
    }

    public class UserRight
    {
        public long? Id { get; set; }
        public long? OperatorId { get; set; }
        public long? AccessRightId { get; set; }
        public IEnumerable<AccessRight> Rights {get;set;}
        
    }

    public enum Permissions:long
    {
        Admin=2,
        AdminLPU=3,
        Register=4,
        Doctor=5 
    }

    public class RightsRepository 
    {
        public IEnumerable<UserRight> GetIndividualRights(long operatorId)
        {
            using(var db=new DbWorker())
            {
                var sql = @"select distinct 
                            code_access_id accessrightid,
                            false isgroup,
                            true hasacccess
                            from users_access_tab
                            where users_id=@OperId
                           ";
                var result = db.Connection.Query<UserRight>(sql, new  { OperId=operatorId });
                if (result!=null)
                {
                    foreach(var item in result)
                    {
                        item.OperatorId = operatorId;
                    }
                }
                return result;
            }
        }

        public IEnumerable<AccessRight> GetGroupRights(long operatorId)
        {
            using(var db = new DbWorker())
            {
                var sql = @"
                            SELECT distinct
                            ga.code_access_id accessrightid,
                            true isgroup,
                            true hasacccess
                            FROM users_tab u
                            LEFT JOIN groups_entry_tab g ON g.users_id = u.id
                            LEFT JOIN groups_access_tab ga ON ga.groups_id = g.groups_id
                            LEFT JOIN groups_tab gr ON gr.id = g.groups_id
                            WHERE gr.expiration_date >= 'now' ::text::date
                            and u.id=@OperId
                            ";
                var result = db.Connection.Query<AccessRight>(sql, new { OperId=operatorId });
                return result;
            }
        }

        public IEnumerable<AccessRight> GetRights(long operatorId)
        {
            using (var db = new DbWorker())
            {
                var sql = @"
                            SELECT DISTINCT code_access accessrightid, name, description 
                            FROM users_access_view WHERE users_id = @OperId
                            ";
                var result = db.Connection.Query<AccessRight>(sql, new { OperId = operatorId });
                return result;
            }
        }

        public void AddAccessRight(Operator loggedOper, Operator oper, long rightId)
        {

                var sql = @"insert into user_access_tab(operator_id, users_id, code_access_id)
                            values(@LoggedOperId, @OperId, @RightId)
                           ";
                using (var db = new DbWorker())
                {
                    var result = db.Connection.Execute(sql, 
                        new { LoggedOperId=loggedOper.Id, OperId=oper.Id, RightId=rightId });
                    return;
                }
            
        }

        public void RemoveAccessRight(Operator loggedOper, Operator oper, long rightId)
        {
            var sql = @"delete from user_access_tab
                        where users_id=@OperId and code_access_id=@RightId
                       ";
            using (var db = new DbWorker())
            {
                var result = db.Connection.Execute(sql,
                    new { OperId = oper.Id, RightId = rightId });
                return;
            }
        }

        public void AddAccessRightToGroup(Operator loggedOper, long groupId, long rightId)
        {
            var sql = @"insert into group_access_tab(operator_id, group_id, code_access_id)
                        values(@LoggedOper, @GroupId, @RightId)
                       ";
            using (var db = new DbWorker())
            {
                var result = db.Connection.Execute(sql,
                    new { LoggedOper = loggedOper.Id, GroupId = groupId, RightId = rightId });
                return;
            }
        }

        public void RemoveAccessRightForGroup(Operator loggedOper, long groupId, long rightId)
        {
            var sql = @"delete from group_access_tab
                        where group_id=@GroupId and code_access_id=@RightId
                       ";
            using (var db = new DbWorker())
            {
                var result = db.Connection.Execute(sql,
                    new { GroupId = groupId, RightId = rightId });
                return;
            }
        }

        public void AddUserToGroup(Operator loggedOper, Operator oper, long groupId)
        {
            var sql = @"insert into group_entry_tab(operator_id,users_id, group_id)
                        values(@LoggedOper, @OperId, @GroupId)
                       ";
            using (var db = new DbWorker())
            {
                var result = db.Connection.Execute(sql,
                    new { LoggedOper = loggedOper.Id, OperId = oper.Id, GroupId = groupId });
                return;
            }
        }
    }
}
