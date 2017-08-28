using System;
using System.ComponentModel;
using SharedDbWorker.Classes;

namespace Model.Interface
{
    public interface ILoadData
    {
        event EventHandler Loading;
        event EventHandler Loaded;

        void OnLoading();
        void OnLoaded();

        [Browsable(false)]
        bool IsLoading { get; }
        [Browsable(false)]
        bool IsLoaded { get; }
        /// <summary>
        /// Инициализирует объект по идентификатору (создается новое подключение к БД)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool LoadData(long id);
        /// <summary>
        /// Инициализирует объект результатом поиска.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        bool LoadData(DbResult result);
    }
}