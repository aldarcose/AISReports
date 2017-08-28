using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SharedDbWorker.Classes;
using System.Threading;

namespace SharedDbWorker
{
    public interface IDbWorker
    {
        int GetUserId(string login, string password);

        /// <summary>
        /// Executes NonQuery Command
        /// </summary>int Execute(DbQuery query);


        /// <summary>
        /// Executes Scalar Command
        /// </summary>
        object GetScalarResult(DbQuery query);

        /// <summary>
        /// Executes Reader Command and returns first row
        /// </summary>
        DbResult GetResult(DbQuery query);

        /// <summary>
        /// Executes Reader Command
        /// </summary>
        List<DbResult> GetResults(DbQuery query, IProgressControl pc);

        /// <summary>
        /// Opens connection
        /// </summary>
        /// <returns>True if opened</returns>
        bool Open();

        /// <summary>
        /// Closes connection
        /// </summary>
        void Close();
    }

    /// <summary>
    /// Интерфейс контрола для длительной операции
    /// </summary>
    public interface IProgressControl
    {
        void SetStatus(string text);

        void SetMaximum(int maximum);

        void SetProgress(int progress);
    }
}
