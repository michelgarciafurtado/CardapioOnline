using CardapioOnline.Models;
using Compartilhado.Eventos;
using MassTransit;

namespace CardapioOnline.Consumer;

public class ProdutoCriadoConsumer:IConsumer<ProdutoCriadoEvento>
{
    public Cardapio cardapio = new Cardapio();
    public Task Consume(ConsumeContext<ProdutoCriadoEvento> context)
    {
        var produto = context.Message;
        cardapio.Produtos.Add(new ProdutoViewModel
        {
            Nome = produto.nome,
            Descricao = produto.descricao,
            Preco = produto.preco,
            Categoria = produto.categoria
        });

        Console.WriteLine(cardapio.Produtos[0].Nome);
        return Task.CompletedTask;
    }
}
