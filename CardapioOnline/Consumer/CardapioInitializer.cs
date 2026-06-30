using CardapioOnline.Models;
using Compartilhado.Eventos;
using MassTransit;

namespace CardapioOnline.Consumer;

public class CardapioInitializer:IHostedService
{
    private readonly Cardapio _cardapio;
    
    private IServiceProvider _serviceProvider;

    public CardapioInitializer(Cardapio cardapio, IServiceProvider serviceProvider)
    {
        _cardapio = cardapio;
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Cardapio Initializer acionado por produtos API....");
        using var scope = _serviceProvider.CreateScope();
        var response = await scope.ServiceProvider.GetRequiredService<IRequestClient<ObterProdutosRequest>>()
                        .GetResponse<ObterProdutosResponse>(
                                 new ObterProdutosRequest(), 
                                 cancellationToken
                                 );
        
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

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
