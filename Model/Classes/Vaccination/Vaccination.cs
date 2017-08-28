using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Classes.Vaccination
{
    /// <summary>
    /// Прививка (public priviv_tab)
    /// </summary>
    public class Vaccination
    {
        //priviv_id
        public long? Id { get; set; }
        
        //dan_id
        public long? PatientId { get; set; }
        
        //infection
        public long? InfectionId { get; set; }
        public string InfectionName { get; set; }
        
        //vid_priviv
        public long? Type { get; set; }
        public string TypeName { get; set; }
        
        //Препарат
        public long? PreparatId { get; set; }
        public string PreparatName { get; set; }
        
        /// <summary>
        /// Единица измерения
        /// </summary>
        public long? Unit { get; set; }
        
        public string UnitName { get; set; }
        
        /// <summary>
        /// Дозировка (doza)
        /// </summary>
        public double? Dosage { get; set; }
        
        /// <summary>
        /// Дата прививки (date_priviv)
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Серия (seria)
        /// </summary>
        public string Series { get; set; }

        public long? OperatorId { get; set; }

        public long? DoctorId { get; set; }

        public long? ReactionId { get; set; }
        public string ReactionName { get; set; }

        public long? NurseId { get; set; }

    }

    /// <summary>
    /// Препарат
    /// </summary>
    public class Preparat
    {
        public long? Id { get; set; }
        public string Name { get;set; }
        public string Code { get;set; }
    }

    public class VaccinationType
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

    }

    public class Reaction 
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class Unit
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class Infection
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

}
