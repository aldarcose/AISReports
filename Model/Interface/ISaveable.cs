using System;
using System.ComponentModel;
using Model.Annotations;

namespace Model.Interface
{
    public interface ISaveable
    {
        event EventHandler Saved;
        event EventHandler Saving;
        [Browsable(false)]
        bool IsSaved { get; set; }
        void OnSaving();
        void OnSaved();
        bool CanSave(Operator @operator, out string message);
        void Save(Operator @operator);
    }
}