using BookSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BookSystem.ViewModels
{
    public class CreateLivroViewModel
    {
        public Livro? Livro { get; set; }

        public SelectList? Categorias { get; set; }
        [Required(ErrorMessage = "Campo Obrigatorio")]
        public string CategoriaEscolhida { get; set; }
        public SelectList? Autores { get; set; }
        [Required(ErrorMessage = "Campo Obrigatorio")]
        public string AutorEscolhido { get; set; }
             
    }
}
