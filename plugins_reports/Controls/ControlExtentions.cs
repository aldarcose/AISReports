﻿

using System;
using System.Windows.Forms;

namespace Reports.Controls
{
    /// <summary>
    /// Расширения облегчающие работу с элементами управления в многопоточной среде.
    /// </summary>
    /// <remarks>
    /// Данный класс был взят <see href="http://sergeyteplyakov.blogspot.ru/2009/03/windows-forms-controlinvoke.html">отсюда</see>
    /// </remarks>
    public static class ControlExtentions
    {
        /// <summary>
        /// Вызов делегата через control.Invoke, если это необходимо.
        /// </summary>
        /// <param name="control">Элемент управления</param>
        /// <param name="doit">Делегат с некоторым действием</param>
        public static void InvokeIfNeeded(this Control control, Action doit)
        {
            if (control.InvokeRequired)
                control.Invoke(doit);
            else
                doit();
        }
        /// <summary>
        /// Вызов делегата через control.Invoke, если это необходимо.
        /// </summary>
        /// <typeparam name="T">Тип параметра делегата</typeparam>
        /// <param name="control">Элемент управления</param>
        /// <param name="doit">Делегат с некоторым действием</param>
        /// <param name="arg">Аргумент делагата с действием</param>
        public static void InvokeIfNeeded<T>(this Control control, Action<T> doit, T arg)
        {
            if (control.InvokeRequired)
                control.Invoke(doit, arg);
            else
                doit(arg);
        }
    }
}
