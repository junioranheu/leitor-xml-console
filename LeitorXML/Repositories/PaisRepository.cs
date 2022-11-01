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

        public async Task<List<Pais>>? GetPaises()
        {
            string[] xmls = Directory.GetFiles(AppContext.BaseDirectory + $"\\XML\\{GetDescricaoEnum(ListaXmlsEnum.Pais)}\\", "*.xml", SearchOption.TopDirectoryOnly);

            if (xmls?.Length > 0)
            {
                XmlSerializer ser = new(typeof(PaisContainer));
                List<Pais> listaPaises = new();

                foreach (var xml in xmls)
                {
                    FileStream fileStream = new(xml, FileMode.Open);
                    Pais[]? xmlPaises = ((PaisContainer)ser.Deserialize(fileStream)).Paises;

                    if (xmlPaises is not null)
                    {
                        foreach (var item in xmlPaises)
                        {
                            Pais pais = new()
                            {
                                Codigo = !String.IsNullOrEmpty(item.Codigo) ? item.Codigo : "-",
                                Nome = !String.IsNullOrEmpty(item.Nome) ? item.Nome : "-"
                            };

                            listaPaises.Add(pais);
                        }
                    }
                }

                if (listaPaises?.Count > 0)
                {
                    await _context.AddRangeAsync(listaPaises);
                    await _context.SaveChangesAsync();
                }

                return listaPaises;
            }

            return null;
        }
    }
}
