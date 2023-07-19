using System.ComponentModel.DataAnnotations;

namespace BookSystem.Models
{
    public class Categoria
    {
        public int ID { get; set; }
        
        [StringLength(20)]
        [Required(ErrorMessage ="Campo Obrigatório")]
        public string Nome { get; set; } = string.Empty;
        
        [StringLength(20)]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        public ICollection<Livro> ?Livros { get; set; }
    }
}
