using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Classes.Codifiers
{
    /// <summary>
    /// Человек, которому предоставлены федеральные льготы
    /// Список людей предоставляется пенсионым фондом
    /// </summary>
    public class PersonWithFedBenefit
    {
        /// <summary>
        /// Снилс
        /// </summary>
        public string Snils { get; set; }
        /// <summary>
        /// Серия полиса (необязательно)
        /// </summary>
        public string PolicySerial { get; set; }
        /// <summary>
        /// Номер полиса (необязательно)
        /// </summary>
        public string PolicyNumber { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Пол
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Код льготы
        /// </summary>
        public string BenefitCode { get; set; }
        /// <summary>
        /// Ид в системе, если он посещал поликлинику, иначе - пусто
        /// </summary>
        public long? PatientId { get; set; }
        
        /// <summary>
        /// Флаг показывающий, что льгота добавлена вручную
        /// </summary>
        public bool IsManualAdded { get; set; }

        
        public DateTime? DateEnd { get; set; }

        public string Status { get; set; }
    }
}
