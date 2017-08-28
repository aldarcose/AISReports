using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model.Classes.Codifiers
{
    /// <summary>
    /// ЛПУ (lpu_tab)
    /// </summary>
    public class MO
    {
        [Display(AutoGenerateField = false)]
        public long Id { get; set; }
        [Display(Name = "Код")]
        public string Code { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Полное название")]
        public string FullName { get; set; }
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        [Display(Name = "ОГРН")]
        public string Ogrn { get; set; }
        [Display(AutoGenerateField = false)]
        public string Okvd { get; set; }

        public override string ToString()
        {
            return FullName;
        }
    }
}
