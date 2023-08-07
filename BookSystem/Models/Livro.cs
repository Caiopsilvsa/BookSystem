using System.ComponentModel.DataAnnotations;

namespace BookSystem.Models
{
    public class Livro
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
        public string? Foto { get; set; }

        public virtual Autor? Autor { get; set; }
        public int? AutorID { get; set; }
        public virtual Categoria? Categoria { get; set; }
        public int? CategoriaID { get; set; }

    }
}
