using CardapioOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CardapioOnline.Pages
{
    public class IndexModel : PageModel
    {
       public Cardapio Cardapio { get; set; } = new Cardapio();
        public void OnGet()
        {
            Cardapio.ObterProdutos();
        }
    }

   
}
