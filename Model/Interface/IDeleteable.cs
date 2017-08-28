using System;
using System.ComponentModel;
using Model.Classes;

namespace Model.Interface
{
    public interface IDeleteable
    {
        event EventHandler<CancelableEventArgs> Deleting;
        event EventHandler Deleted;

        void OnDeleting();
        void OnDeleted();
        bool CanDeleteFromDb(Operator @operator, out string message);
        void DeleteFromDb(Operator @operator);
    }
}