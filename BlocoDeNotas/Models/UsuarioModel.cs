using System.ComponentModel.DataAnnotations;

namespace BlocoDeNotas.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite seu e-mail.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Insira uma senha.")]
        public string Senha { get; set; }
    }
}
