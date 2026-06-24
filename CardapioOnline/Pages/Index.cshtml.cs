using CardapioOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CardapioOnline.Pages
{
    public class IndexModel : PageModel
    {
       private readonly Cardapio _cardapio;
        public Cardapio Cardapio => _cardapio;

        public IndexModel(Cardapio cardapio)
        {
            _cardapio = cardapio;
        }

        public void OnGet()
        {
            
        }

        public PartialViewResult OnGetCarregarTabela()
        {
            var produtos = _cardapio.ObterProdutos();
            return new PartialViewResult
            {
                ViewName = "_TabelaProdutosPartial",
                ViewData = new ViewDataDictionary<List<ProdutoViewModel>>(ViewData, produtos)
            };
        }
    }

   
}
