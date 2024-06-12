using BlocoDeNotas.Models;

namespace BlocoDeNotas.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel CriarConta(UsuarioModel usuario);
        UsuarioModel Editar(UsuarioModel usuario);
        UsuarioModel Selecionar(int id);
        bool Excluir(int id);
    }
}
