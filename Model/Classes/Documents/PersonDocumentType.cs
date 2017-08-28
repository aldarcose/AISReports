using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Classes.Codifiers;
using Model.Interface;
using SharedDbWorker;
using SharedDbWorker.Classes;

namespace Model.Classes.Documents
{
    public class PersonDocumentType : BaseDocumentType, ILoadData
    {
        public int FomsCode { get; set; }
        public override string GetSerialRegexMask()
        {
            var mask = string.Empty;
            switch (FomsCode)
            {
                // Паспорт гражданина СССР: DocSer="R-ББ" DocNum="999999"
                case 1:
                    mask = DocumentMaskTemplate.GetRegexTemplate("R-ББ");
                    break;
                // Загранпаспорт гражданина СССР: DocSer="S" DocNum="00000009"
                case 2:
                    mask = DocumentMaskTemplate.GetRegexTemplate("S"); ;
                    break;
                // Свидетельство о рождении, выданное в Российской Федерации: DocSer="R-ББ" DocNum="999999"
                case 3:
                    mask = DocumentMaskTemplate.GetRegexTemplate("R-ББ");
                    break;
                // Удостоверение личности офицера: DocSer="ББ" DocNum="9999999"
                case 4:
                    mask = DocumentMaskTemplate.GetRegexTemplate("ББ");
                    break;
                // Справка об освобождении из места лишения свободы: DocSer="S" DocNum="00000009"
                case 5:
                    mask = DocumentMaskTemplate.GetRegexTemplate("S");
                    break;
                // Паспорт Минморфлота: DocSer="ББ" DocNum="999999"
                case 6:
                    mask = DocumentMaskTemplate.GetRegexTemplate("ББ");
                    break;
                // Военный билет: DocSer="ББ" DocNum="9999990"
                case 7:
                    mask = DocumentMaskTemplate.GetRegexTemplate("ББ");
                    break;
                // Дипломатический паспорт гражданина Российской Федерации: DocSer="99" DocNum="9999999"
                case 8:
                    mask = DocumentMaskTemplate.GetRegexTemplate("99");
                    break;
                // Паспорт иностранного гражданина: DocSer="S" DocNum="0000000009"
                case 9:
                    mask = DocumentMaskTemplate.GetRegexTemplate("S");
                    break;
                // Свидетельство о регистрации ходатайства о признании иммигранта беженцем на территории Российской Федерации: DocSer="S" DocNum="00000009"
                case 10:
                    mask = DocumentMaskTemplate.GetRegexTemplate("S");
                    break;
                // Вид на жительство: DocSer="S1" DocNum="00000009"
                case 11:
                    mask = DocumentMaskTemplate.GetRegexTemplate("S1");
                    break;
                // Удостоверение беженца в Российской Федерации: DocSer="S" DocNum="00000009"
                case 12:
                    mask = DocumentMaskTemplate.GetRegexTemplate("S");
                    break;
                // Временное удостоверение личности гражданина Российской Федерации: DocSer="S" DocNum="00000009"
                case 13:
                    mask = DocumentMaskTemplate.GetRegexTemplate("S");
                    break;
                // Паспорт гражданина Российской Федерации: DocSer="99 99" DocNum="9999990"
                case 14:
                    mask = DocumentMaskTemplate.GetRegexTemplate("99 99");
                    break;
                // Заграничный паспорт гражданина Российской Федерации: DocSer="99" DocNum="9999999"
                case 15:
                    mask = DocumentMaskTemplate.GetRegexTemplate("99");
                    break;
                // Паспорт моряка: DocSer="ББ" DocNum="9999990"
                case 16:
                    mask = DocumentMaskTemplate.GetRegexTemplate("ББ");
                    break;
                // Военный билет офицера запаса: DocSer="ББ" DocNum="999999"
                case 17:
                    mask = DocumentMaskTemplate.GetRegexTemplate("ББ");
                    break;
                // Иные документы: DocSer="S1" DocNum="0000000009"
                case 18:
                    mask = DocumentMaskTemplate.GetRegexTemplate("S1");
                    break;
                // Документ иностранного гражданина: DocSer="S1" DocNum="000000000009"
                case 19:
                    mask = DocumentMaskTemplate.GetRegexTemplate("S1");
                    break;
                // Документ лица без гражданства: DocSer="S1" DocNum="000000000009"
                case 20:
                    mask = DocumentMaskTemplate.GetRegexTemplate("S1");
                    break;
                // Разрешение на временное проживание: DocSer="S1" DocNum="000000000009"
                case 21:
                    mask = DocumentMaskTemplate.GetRegexTemplate("S1");
                    break;
                //Свидетельство о рождении, выданное не в Российской Федерации: DocSer="S1" DocNum="000000000009"
                case 22:
                    mask = DocumentMaskTemplate.GetRegexTemplate("S1");
                    break;
                // Свидетельство о предоставлении временного убежища на территории Российской Федерации: DocSer="99" DocNum="9999999"
                case 23:
                    mask = DocumentMaskTemplate.GetRegexTemplate("99");
                    break;
            }
            return mask;
        }

        public override string GetNumberRegexMask()
        {
            var mask = string.Empty;
            switch (FomsCode)
            {
                // Паспорт гражданина СССР: DocSer="R-ББ" DocNum="999999"
                case 1:
                    mask = DocumentMaskTemplate.GetRegexTemplate("999999");
                    break;
                // Загранпаспорт гражданина СССР: DocSer="S" DocNum="00000009"
                case 2:
                    mask = DocumentMaskTemplate.GetRegexTemplate("00000009");
                    break;
                // Свидетельство о рождении, выданное в Российской Федерации: DocSer="R-ББ" DocNum="999999"
                case 3:
                    mask = DocumentMaskTemplate.GetRegexTemplate("999999");
                    break;
                // Удостоверение личности офицера: DocSer="ББ" DocNum="9999999"
                case 4:
                    mask = DocumentMaskTemplate.GetRegexTemplate("9999999");
                    break;
                // Справка об освобождении из места лишения свободы: DocSer="S" DocNum="00000009"
                case 5:
                    mask = DocumentMaskTemplate.GetRegexTemplate("00000009");
                    break;
                // Паспорт Минморфлота: DocSer="ББ" DocNum="999999"
                case 6:
                    mask = DocumentMaskTemplate.GetRegexTemplate("999999");
                    break;
                // Военный билет: DocSer="ББ" DocNum="9999990"
                case 7:
                    mask = DocumentMaskTemplate.GetRegexTemplate("9999990");
                    break;
                // Дипломатический паспорт гражданина Российской Федерации: DocSer="99" DocNum="9999999"
                case 8:
                    mask = DocumentMaskTemplate.GetRegexTemplate("9999999");
                    break;
                // Паспорт иностранного гражданина: DocSer="S" DocNum="0000000009"
                case 9:
                    mask = DocumentMaskTemplate.GetRegexTemplate("0000000009");
                    break;
                // Свидетельство о регистрации ходатайства о признании иммигранта беженцем на территории Российской Федерации: DocSer="S" DocNum="00000009"
                case 10:
                    mask = DocumentMaskTemplate.GetRegexTemplate("00000009");
                    break;
                // Вид на жительство: DocSer="S1" DocNum="00000009"
                case 11:
                    mask = DocumentMaskTemplate.GetRegexTemplate("00000009");
                    break;
                // Удостоверение беженца в Российской Федерации: DocSer="S" DocNum="00000009"
                case 12:
                    mask = DocumentMaskTemplate.GetRegexTemplate("00000009");
                    break;
                // Временное удостоверение личности гражданина Российской Федерации: DocSer="S" DocNum="00000009"
                case 13:
                    mask = DocumentMaskTemplate.GetRegexTemplate("00000009");
                    break;
                // Паспорт гражданина Российской Федерации: DocSer="99 99" DocNum="9999990"
                case 14:
                    mask = DocumentMaskTemplate.GetRegexTemplate("9999990");
                    break;
                // Заграничный паспорт гражданина Российской Федерации: DocSer="99" DocNum="9999999"
                case 15:
                    mask = DocumentMaskTemplate.GetRegexTemplate("9999999");
                    break;
                // Паспорт моряка: DocSer="ББ" DocNum="9999990"
                case 16:
                    mask = DocumentMaskTemplate.GetRegexTemplate("9999990");
                    break;
                // Военный билет офицера запаса: DocSer="ББ" DocNum="999999"
                case 17:
                    mask = DocumentMaskTemplate.GetRegexTemplate("999999");
                    break;
                // Иные документы: DocSer="S1" DocNum="0000000009"
                case 18:
                    mask = DocumentMaskTemplate.GetRegexTemplate("0000000009");
                    break;
                // Документ иностранного гражданина: DocSer="S1" DocNum="000000000009"
                case 19:
                    mask = DocumentMaskTemplate.GetRegexTemplate("000000000009");
                    break;
                // Документ лица без гражданства: DocSer="S1" DocNum="000000000009"
                case 20:
                    mask = DocumentMaskTemplate.GetRegexTemplate("000000000009");
                    break;
                // Разрешение на временное проживание: DocSer="S1" DocNum="000000000009"
                case 21:
                    mask = DocumentMaskTemplate.GetRegexTemplate("000000000009");
                    break;
                //Свидетельство о рождении, выданное не в Российской Федерации: DocSer="S1" DocNum="000000000009"
                case 22:
                    mask = DocumentMaskTemplate.GetRegexTemplate("000000000009");
                    break;
                // Свидетельство о предоставлении временного убежища на территории Российской Федерации: DocSer="99" DocNum="9999999"
                case 23:
                    mask = DocumentMaskTemplate.GetRegexTemplate("9999999");
                    break;
            }
            return mask;
        }

        public event EventHandler Loading;
        public event EventHandler Loaded;
        public void OnLoading()
        {
            IsLoading = true;
            IsLoaded = false;
            if (Loading != null)
            {
                Loading(this, null);
            }
        }

        public void OnLoaded()
        {
            IsLoading = false;
            IsLoaded = true;
            if (Loaded != null)
            {
                Loaded(this, null);
            }
        }

        public bool IsLoading { get; private set; }
        public bool IsLoaded { get; private set; }
        public bool LoadData(long id)
        {
            Id = id;
            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetDocumentType");
                query.Sql = "select vid_doc_id, name, ffoms_code from public.vid_doc_tab where vid_doc_id = @id;";
                query.AddParamWithValue("@id", id);

                var result = dbWorker.GetResult(query);
                if (result != null && result.Fields.Count > 0)
                {
                    this.Name = DbResult.GetString(result.Fields[1], "");
                    this.FomsCode = DbResult.GetInt(result.Fields[2], -1);
                }
            }
            return true;
        }

        public bool LoadData(DbResult result)
        {
            throw new NotImplementedException();
        }
    }
}
