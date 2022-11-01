using LeitorXML.Interfaces;
using LeitorXML.Models;
using System.Xml.Serialization;

namespace LeitorXML.Repositories
{
    public class PaisRepository : IPaisInterface
    {
        public PaisRepository()
        {
  
        }

        public Pais[]? GetPaises()
        {
            XmlSerializer ser = new(typeof(PaisContainer));
            FileStream myFileStream = new(AppContext.BaseDirectory + "\\XML\\a.xml", FileMode.Open);

            Pais[]? testeee = ((PaisContainer)ser.Deserialize(myFileStream)).Paises;

            return testeee;
        }
    }
}
