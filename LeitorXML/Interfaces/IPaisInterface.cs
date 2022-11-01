using LeitorXML.Models;

namespace LeitorXML.Interfaces
{
    internal interface IPaisInterface
    {
        Task<List<Pais>>? GetPaises();
    }
}
