using System;
using Model;
using Model.Classes;
using Model.Classes.Laboratory;
using SharedDbWorker.Classes;

namespace ARMPlugin.Presenter
{
    public interface ILabResearchControl
    {
        event EventHandler<LabOrderResearchEventArgs> LabOrderSelected;
        void RefreshLabOrders();
        Patient Patient { get; set; }
        DbQuery OrdersQuery { get; set; }
        LabOrder SelectedLabOrder { get; }
        Action CreateOrderAction { get; set; }
        bool IsAddButtonVisible { get; set; }
        Action OpenOrderAction { get; set; } 
    }

    public class LabOrderResearchEventArgs : EventArgs
    {
        public LabOrder SelectedLabOrder { get; set; }
    }
}