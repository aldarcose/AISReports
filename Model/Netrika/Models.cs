using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Netrika
{
    public class District
    {
        public string DistrictName { get; set; }
        public int? Id { get; set; }
    }

    public class Clinic
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public int? DistrictId { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string LPUType { get; set; }
    }

    public class Speciality
    {
        
        public int? CountFreeParticipantIE {get;set;}
        public int? CountFreeTicket { get; set; }
        public string FerIdSpeсiality { get; set; }
        public string IdSpeсiality { get; set; }
        public System.Nullable<System.DateTime> LastDate { get; set; }
        public string NameSpeciality { get; set; }
        public System.Nullable<System.DateTime> NearestDate { get; set; }
        
    }

    public class Doctor
    {
        public string AriaNumber{get;set;}
        public int CountFreeParticipantIE{get;set;}
        public int CountFreeTicket{get;set;}
        public string IdDoc{get;set;}
        public System.Nullable<System.DateTime> LastDate{get;set;}
        public string Name{get;set;}
        public System.Nullable<System.DateTime> NearestDate{get;set;}
        public string Snils{get;set;}
    }

    public class Appointment
    {
        public string IdAppointment { get; set; }
        public System.DateTime VisitEnd { get; set; }
        public System.DateTime VisitStart { get; set; }
        public bool? RecordableDay { get; set; }
    }

    public class Patient
    {
        public string AriaNumber{get;set;}
        public System.Nullable<System.DateTime> Birthday{get;set;}
        public string CellPhone{get;set;}
        public string Document_N{get;set;}
        public string Document_S{get;set;}
        public string HomePhone{get;set;}
        public string IdPat{get;set;}
        public string Name{get;set;}
        public string Polis_N{get;set;}
        public string Polis_S{get;set;}
        public string SecondName{get;set;}
        public string Surname{get;set;}
        
    }

    public class ServSession
    {
        public int? IdHistory { get; set; }
        public string IdPat { get; set; }
        public bool? Success { get; set; }
    }


    public class PatientHistory
    {
        public int? IdHistory { get; set; }
        public string IdAppointment { get; set; }
        public DoctorInfo Doctor { get; set; }
        public DateTime? VisitStart { get; set; }
    }


    public class DoctorInfo
    {
        public string AriaNumber { get; set; }
        public string IdDoc { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name,IdDoc);
        }
    }

    public class WorkingTime
    {
        public string DenyCause { get; set; }
        public bool? RecordableDay { get; set; }
        public DateTime? VisitStart { get; set; }
        public DateTime? VisitEnd { get; set; }
    }

    public static class Utils
    {
        public static Patient ConvertPatient(Model.Classes.Patient patient)
        {
            var pat = new Patient()
            {
                Surname = patient.LastName,
                Name = patient.FirstName,
                Birthday = patient.BirthDate
            };

            pat.Polis_S = (patient.Policy != null) ? patient.Policy.Serial : null;
            pat.Polis_N = (patient.Policy != null) ? patient.Policy.Number : null;

            return pat;
        }
    }

    
    public class NetrikaError
    {
        public string Description {get;set;}
        public int? IdError {get;set;} 
    }

    public class NetrikaRegistryException: Exception
    {
        public List<NetrikaError> ErrorList = new List<NetrikaError>();

        public NetrikaRegistryException(IEnumerable<NetrikaError> errors)
        {
            ErrorList.AddRange(errors);
        }
    }


}
