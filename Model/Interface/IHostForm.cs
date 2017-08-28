using System;
using Model.Classes;

namespace Model.Interface
{
    public interface IHostForm
    {
        long GetOperatorId();

        Operator GetOperator();

        string FormCaptionText { get; set; } 
        /*
         
         */
    }
}