using System.ComponentModel.DataAnnotations;

namespace BlocoDeNotas.Models
{
    public class NotasModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o título da nota.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Digite a descrição da nota.")]
        public string Descricao { get; set; }
        public int IdUsuario { get; set; } // Chave estrangeira para a tabela Usuario

        public UsuarioModel Usuario { get; set; }
    }
}
