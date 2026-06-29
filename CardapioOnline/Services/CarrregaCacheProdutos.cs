using CardapioOnline.Consumer;

namespace CardapioOnline.Services
{
    public class CarrregaCacheProdutos
    {
        private readonly IEnumerable<ProdutoObtido> _produtos;

        public void AtualizarCache(IEnumerable<ProdutoObtido> produtos)
        {
            _produtos = produtos;
        }

    }
}
