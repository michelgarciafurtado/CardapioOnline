namespace CardapioOnline.Consumer;

public record ProdutoObtido(int Id,
    string Nome,
    string Descricao,
    decimal Preco,
    int CategoriaId,
    string CategoriaNome
);
