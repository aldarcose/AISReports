using System;
using System.ComponentModel;

namespace Model.Interface
{
    public interface ICreateable
    {
        event EventHandler Created;

        void OnCreated();
        bool CanCreateInDb(Operator @operator, out string message);
        void CreateInDb(Operator @operator);
    }
}