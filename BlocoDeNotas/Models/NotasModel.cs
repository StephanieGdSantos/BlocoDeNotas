using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlocoDeNotas.Models
{
    public class NotasModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o título da nota.")]
        public required string Titulo { get; set; }

        [Required(ErrorMessage = "Digite a descrição da nota.")]
        public required string Descricao { get; set; }
        public string DataCriacao { get; set; }
        public int UsuarioId { get; set; } // Chave estrangeira para a tabela Usuario
    }
}
