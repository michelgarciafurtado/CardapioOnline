using CardapioOnline.Models;
using MassTransit;

namespace CardapioOnline.Consumer
{
    public class ProdutoCriadoConsumer:IConsumer<Models.ProdutoViewModel>
    {
        public Cardapio cardapio = new Cardapio();
        public Task Consume(ConsumeContext<Models.ProdutoViewModel> context)
        {
            var produto = context.Message;
            cardapio.Produtos.Add(new ProdutoViewModel
            {
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                Categoria = produto.Categoria
            });
            return Task.CompletedTask;
        }
    }
}
