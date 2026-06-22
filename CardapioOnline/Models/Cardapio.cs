namespace CardapioOnline.Models
{
    public class Cardapio
    {
        public List<ProdutoViewModel> Produtos { get; set; } = new List<ProdutoViewModel>();

        public void AdicionarProduto(ProdutoViewModel produto)
        {
            Produtos.Add(produto);
        }

        public void RemoverProduto(ProdutoViewModel produto)
            {
                Produtos.Remove(produto);
        }   

        public List<ProdutoViewModel> ObterProdutos()
        {
            return Produtos;
        }
    }
}
