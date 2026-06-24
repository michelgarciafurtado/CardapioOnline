using CardapioOnline.Models;
using Compartilhado.Eventos;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace CardapioOnline.Consumer;

public class ProdutoCriadoConsumer:IConsumer<ProdutoCriadoEvento>
{
    private readonly Cardapio _cardapio;
    private readonly IHubContext<CardapioHub> _hubContext;

    public ProdutoCriadoConsumer(Cardapio cardapio, IHubContext<CardapioHub> hubContext)
    {
        _cardapio = cardapio;
        _hubContext = hubContext;
    }

    public Task Consume(ConsumeContext<ProdutoCriadoEvento> context)
    {
        var produto = context.Message;
        _cardapio.Produtos.Add(new ProdutoViewModel
        {
            Nome = produto.nome,
            Descricao = produto.descricao,
            Preco = produto.preco,
            Categoria = produto.categoria
        });

        
        return Task.CompletedTask;
    }
}
