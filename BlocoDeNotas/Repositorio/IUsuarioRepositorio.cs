using BlocoDeNotas.Models;

namespace BlocoDeNotas.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel CriarConta(UsuarioModel usuario);
    }
}
