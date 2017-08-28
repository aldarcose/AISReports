using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharedDbWorker.Classes
{
    public class DbQuery
    {
        public DbQuery(string name)
        {
            Name = name;
            CommandParams = new Dictionary<string, object>();
        }

        public string Name { get; set; }

        public string Sql { get; set; }

        public Dictionary<string, object> CommandParams { get; private set; }

        public bool CommandParamsContainsKey(string key)
        {
            return CommandParams.ContainsKey(key);
        }

        public void AddParamWithValue(string key, object value)
        {
            if (!CommandParamsContainsKey(key))
                CommandParams.Add(key, value);
        }
    }
}
