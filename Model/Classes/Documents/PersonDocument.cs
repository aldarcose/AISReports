using Model.Classes.Codifiers;
using Model.Interface;

namespace Model.Classes.Documents
{
    public class PersonDocument : BaseDocument, IValidatable
    {
        public PersonDocument()
        {
            this.Type = new PersonDocumentType();
        }
        public PersonDocumentType Type { get; set; }
        public string Organization { get; set; }
        public bool Validate(out string errorMessage)
        {
            errorMessage = "";
            return true;
        }
    }
}
