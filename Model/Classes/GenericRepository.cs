using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedDbWorker;
using Dapper;

namespace Model.Classes
{
    public class GenericRepository<T> where T:DbObject
    {
        public T Get(object id) 
        {
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
            using (var db = new DbWorker())
            {
                return db.Connection.Get<T>(id);
            }
        }

        public void Update(T item)
        {
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
            using (var db = new DbWorker())
            {
                db.Connection.Update(item);
            }
        }

        public long? Insert(T item)
        {
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
            using (var db = new DbWorker())
            {
                return db.Connection.Insert(item);
            }
        }

        public void AddOrUpdate(T item)
        {
            if (item.Id!=null)
            {
                Update(item);
            }else
            {
                item.Id = Insert(item);
            }
        }


    }
}
