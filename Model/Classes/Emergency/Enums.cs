using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Classes.Emergency
{
    public enum HandingTo
    {
        // доставлен в мед. учреждение
        Med_Org = 1,
        // передан в ФГУЗ ЦГиЭ
        San_Org = 2,
        // передан в ОВД, ГИБДД, УФКСН
        Pol_Org = 3}

    /*
    // Дисп. учет по данному заболеванию
    public enum DispRegistrationEnum
    {
        // состоит
        Registered = 1,
        // не состоит
        NotRegistered = 2,
        // неизвестно
        Unknown = 3
    }

    // Результат оказания неотложной медицинской помощи
    public enum EmergencyAidResultEnum
    {
        // состоит
        Improvement = 1,
        // не состоит
        Worsening = 2,
        // неизвестно
        NoChange = 3
    }

    
    // Причина вызова
    public enum CallReasonType
    {
        // несчастный случай
        Accident = 1,
        // внезапное заболевание
        SuddenDisease = 2,
        // экстренная перевозка
        EmergencyTransportation = 3,
        // плановая перевозка
        PlannedTransportation = 4,
        // обострение хронического заболевания
        ChronicIllnessWorsening = 5,
        // травма
        Trauma = 6,
        // другое
        Other = 7
    }

    public enum CallResultType
    {
        // оказана помощь, оставлен на месте
        AidProvided = 1,
        // доставлен в травмпункт
        DeliveredToEmergencyRoom = 2,
        // доставлен в больницу
        DeliveredToHospital = 3,
        // передан бригаде СМП
        DeliveredToSMP = 4,
        // отказ от транспортировки в стационар
        StacionarTransportationRefuse = 5,
        // смерть в присутствии бригады
        DeathByStandBrigade = 6,
        // смерть в автомобиле СМП
        DeathInSMPCar = 7,
        // безрезультатный выезд
        NoResult = 8
    }

    public enum NoResultReason
    {
        // больной не найден на месте
        NoPatient = 1,
        // отказ от помощи (от осмотра)
        PatientRefused = 2,
        // адрес не найден
        AddressNotFound = 3,
        // ложный вызов
        FalseCall = 4,
        // смерть до приезда бригады СМП
        DeathBeforeSMPBrigadeArrived = 5,
        // больной увезен до прибытия НМП
        PatientGoneBeforeNMPBrigadeArrived = 6,
        // больной обследован врачом поликлиники до прибытия НМП
        PatientExaminedByDoctorBeforeBrigadeArrived = 7,
        // вызов отменен
        CallAborted = 8,
        // пациент практически здоров
        PatientIsNotIll = 9
    }
     * */
}
