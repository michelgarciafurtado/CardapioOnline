using CardapioOnline.Models;
using MassTransit;

namespace CardapioOnline.Consumer
{
    public class CardapioInitializer:IHostedService
    {
        private readonly Cardapio _cardapio;
        private readonly IRequestClient<ObterProdutoRequest> _requestClient;

        public CardapioInitializer(Cardapio cardapio, IRequestClient<ObterProdutoRequest> requestClient)
        {
            _cardapio = cardapio;
            _requestClient = requestClient;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var response = await _requestClient.GetResponse<ObterProdutoResponse>(new ObterProdutoRequest(), cancellationToken);

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

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
