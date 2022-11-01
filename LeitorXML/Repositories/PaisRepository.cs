using LeitorXML.Data;
using LeitorXML.Enums;
using LeitorXML.Interfaces;
using LeitorXML.Models;
using System.Xml.Serialization;
using static LeitorXML.Utils.Biblioteca;

namespace LeitorXML.Repositories
{
    public class PaisRepository : IPaisInterface
    {
        public readonly Context _context;

        public PaisRepository(Context context)
        {
            _context = context;
        }

        public async Task<Pais[]>? GetPaises()
        {
            XmlSerializer ser = new(typeof(PaisContainer));
            FileStream myFileStream = new(AppContext.BaseDirectory + $"\\XML\\{GetDescricaoEnum(ListaXmlsEnum.Paises)}", FileMode.Open);

            Pais[]? xmlPaises = ((PaisContainer)ser.Deserialize(myFileStream)).Paises;

            if (xmlPaises is not null)
            {
                List<Pais> listaPaises = new();

                foreach (var item in xmlPaises)
                {
                    Pais pais = new()
                    {
                        Codigo = item.Codigo ?? "",
                        Nome = item.Nome ?? ""
                    };

                    listaPaises.Add(pais);
                }

                await _context.AddRangeAsync(listaPaises);
                await _context.SaveChangesAsync();
            }

            return xmlPaises;
        }
    }
}
