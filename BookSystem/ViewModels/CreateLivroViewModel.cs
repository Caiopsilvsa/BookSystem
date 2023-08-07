using BookSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BookSystem.ViewModels
{
    public class CreateLivroViewModel
    {
        public int ID { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Título")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo Obrigatório")]
        [DataType(DataType.Currency)]
        [Display(Name = "Preço")]
        public int Preco { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public int Quantidade { get; set; }
        public IFormFile? Foto { get; set; }

        public virtual Autor? Autor { get; set; }
        public int? AutorID { get; set; }
        public virtual Categoria? Categoria { get; set; }
        public int? CategoriaID { get; set; }

        public SelectList? Categorias { get; set; }
        [Required(ErrorMessage = "Campo Obrigatorio")]
        public string CategoriaEscolhida { get; set; }
        public SelectList? Autores { get; set; }
        [Required(ErrorMessage = "Campo Obrigatorio")]
        public string AutorEscolhido { get; set; }
             
    }
}
