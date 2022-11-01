using LeitorXML.Data;
using LeitorXML.Interfaces;
using LeitorXML.Models;
using LeitorXML.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

bool resetarBd = false;

Console.WriteLine("Iniciando projeto");
var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .AddUserSecrets<Program>()
            .Build();

Console.WriteLine("Iniciando conexão com a base de dados");
string con = config.GetConnectionString("BaseDados"); // Connection string;
var secretPass = config["SecretSenhaBancoDados"]; // secrets.json;
con = con.Replace("[secretSenhaBancoDados]", secretPass); // Alterar pela senha do secrets.json;

var serviceProvider = new ServiceCollection()
                     .AddDbContext<Context>(options => options.UseMySql(con, ServerVersion.AutoDetect(con)))
                     .AddSingleton<IPaisInterface, PaisRepository>()
                     // .AddSingleton<ITesteInterface, BarService>()
                     .BuildServiceProvider();

bool isContinuar = true;
if (resetarBd)
{
    try
    {
        Console.WriteLine("Restaurando a base de dados");
        var context = serviceProvider.GetRequiredService<Context>();

        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
    }
    catch (Exception ex)
    {
        string erroBD = ex.Message.ToString();
        Console.WriteLine($"Falha ao resetar a base de dados: {erroBD}");
        isContinuar = false;
    }
}

if (isContinuar)
{
    Console.WriteLine("\nIniciando leitura dos arquivos XMLs");
    var pais = serviceProvider.GetService<IPaisInterface>();
    Pais[]? xmlPaises = await pais?.GetPaises();

    Console.WriteLine($"\nResultado: {xmlPaises?.Length}");
    if (xmlPaises is not null)
    {
        foreach (var item in xmlPaises)
        {
            Console.WriteLine($"Código: +{item.Codigo} | País: {item.Nome}");
        }
    }
}

Console.WriteLine("\nFim");
