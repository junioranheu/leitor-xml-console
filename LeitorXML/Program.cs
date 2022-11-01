using LeitorXML.Interfaces;
using LeitorXML.Models;
using LeitorXML.Repositories;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Iniciando projeto");

var serviceProvider = new ServiceCollection()
                     .AddSingleton<IPaisInterface, PaisRepository>()
                     // .AddSingleton<ITesteInterface, BarService>()
                     .BuildServiceProvider();

Console.WriteLine("Lendo XML");

var pais = serviceProvider.GetService<IPaisInterface>();
Pais[]? xmlPaises = pais?.GetPaises();

Console.WriteLine($"\nResultado: {xmlPaises?.Length}");

if (xmlPaises is not null)
{
    foreach (var item in xmlPaises)
    {
        Console.WriteLine($"Código: +{item.Codigo} | País: {item.Nome}");
    }
}

Console.WriteLine("\nFim");
Console.ReadKey();
