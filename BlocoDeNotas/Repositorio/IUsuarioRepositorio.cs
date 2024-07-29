using BlocoDeNotas.Models;

namespace BlocoDeNotas.Repositorio
{
    public interface IUsuarioRepositorio
    {
        Task<UsuarioModel> CriarConta(UsuarioModel usuario);
        Task<UsuarioModel> Editar(UsuarioModel usuario);
        Task<UsuarioModel> Selecionar(int id);
        Task<bool> Excluir(int id);
    }
}
