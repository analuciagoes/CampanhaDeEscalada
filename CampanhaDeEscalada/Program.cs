using System.Text.Json;
using System.Text;
using System.Globalization;
public class Venda
{
    public string vendedor { get; set; } = string.Empty;
    public decimal valor { get; set; }
}
public class VendasData
{
    public List<Venda> vendas { get; set; } = new();
}
class Program
{
    static void Main(string[] args)
    {

        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
     
        CultureInfo.CurrentCulture = new CultureInfo("pt-BR");
        
        try
        {
            string jsonString = File.ReadAllText("vendas.json", Encoding.UTF8);
            
               var options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            
            var vendas = JsonSerializer.Deserialize<VendasData>(jsonString, options);
            
            if (vendas?.vendas == null)
            {
                Console.WriteLine("Erro ao ler os dados de vendas.");
                return;
            }
            var resultados = vendas.vendas
                .GroupBy(v => v.vendedor)
                .Select(g => new
                {
                    Vendedor = g.Key,
                    TotalVendas = g.Sum(v => v.valor),
                    TotalComissao = g.Sum(v => CalcularComissao(v.valor)),
                    QuantidadeVendas = g.Count()
                })
                .OrderBy(r => r.Vendedor);

            Console.WriteLine("=== RELATÓRIO DE COMISSÕES ===\n");
            
            foreach (var resultado in resultados)
            {
                Console.WriteLine($"Vendedor: {resultado.Vendedor}");
                Console.WriteLine($"Quantidade de vendas: {resultado.QuantidadeVendas}");
                Console.WriteLine($"Total em vendas: {resultado.TotalVendas:C}");
                Console.WriteLine($"Total em comissões: {resultado.TotalComissao:C}");
                Console.WriteLine(new string('-', 40));
            }
                      
            var totalGeralVendas = resultados.Sum(r => r.TotalVendas);
            var totalGeralComissoes = resultados.Sum(r => r.TotalComissao);
            
            Console.WriteLine($"TOTAL GERAL EM VENDAS: {totalGeralVendas:C}");
            Console.WriteLine($"TOTAL GERAL EM COMISSÕES: {totalGeralComissoes:C}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
    static decimal CalcularComissao(decimal valorVenda)
    {
        if (valorVenda < 100m)
            return 0m;
        else if (valorVenda < 500m)
            return valorVenda * 0.01m;
        else
            return valorVenda * 0.05m;
    }
}
