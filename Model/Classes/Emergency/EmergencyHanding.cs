using System;

namespace Model.Classes.Emergency
{
    /// <summary>
    /// Данные о передаче
    /// </summary>
    public class EmergencyHanding
    {
        /// <summary>
        /// Кому передано
        /// </summary>
        public HandingTo To { get; set; }

        public string FIO { get; set; }
        public DateTime Time { get; set; }

        public string CodeNumber { get; set; }

        public string GetText()
        {
            return string.Format("время: {0}; кому: {1}; № (код): {2}", Time.ToString("t"), FIO, CodeNumber);
        }

        public override string ToString()
        {
            return GetText();
        }
    }
}
