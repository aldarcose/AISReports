using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SharedDbWorker.Classes
{
    public class DbWorkerException : Exception
    {
        public DbWorkerException()
        {
            
        }
        public DbWorkerException(string message)
        {
            Message = message;
        }
        public string Sql { get; set; }
        public new string Message { get; set; }
    }
}
