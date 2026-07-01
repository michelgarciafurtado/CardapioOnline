using CardapioOnline.Models;
using Compartilhado.Eventos;
using MassTransit;

namespace CardapioOnline.Consumer;

public class CardapioUpdater : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Cardapio _cardapio;

    public CardapioUpdater(IServiceProvider serviceProvider, Cardapio cardapio)
    {
        _serviceProvider = serviceProvider;
        _cardapio = cardapio;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("CardapioUpdater iniciado. Atualizando produtos a cada 2 minutos...");
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<ObterProdutosRequest>>();

                var response = await requestClient.GetResponse<ObterProdutosResponse>(
                    new ObterProdutosRequest(), stoppingToken);



                // Atualiza o cardápio em memória
                _cardapio.Produtos.Clear();

                foreach (var produto in response.Message.Produtos)
                {
                    _cardapio.AdicionarProduto(new ProdutoViewModel
                    {
                        Id = produto.Id,
                        Nome = produto.Nome,
                        Descricao = produto.Descricao,
                        Preco = produto.Preco,
                        Categoria = produto.CategoriaNome
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CardapioUpdater: erro ao atualizar produtos - {ex.Message}");
            }

            // Espera 2 minutos antes da próxima atualização
            await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
        }
    }

}