using BookSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookSystem.ViewModels
{
    public class EditLivroViewModel
    {
        public Livro Livro { get; set; }

        public string CategoriaEscolhida { get; set; }
        public string AutorEscolhido { get; set; }
        public SelectList? Autores { get; set; }
        public SelectList? Categorias { get; set; }
    }
}
