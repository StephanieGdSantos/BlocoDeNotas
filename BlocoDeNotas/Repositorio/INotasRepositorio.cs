using BlocoDeNotas.Data;
using BlocoDeNotas.Models;
using System.Runtime.InteropServices;

namespace BlocoDeNotas.Repositorio
{
    public interface INotasRepositorio
    {
        Task<NotasModel> Selecionar(int id);    
        Task<List<NotasModel>> ListarNotas(int usuarioID);
        Task<NotasModel> Adicionar(NotasModel nota);
        Task<NotasModel> Editar(NotasModel nota);
        Task<bool> Excluir(int id);
    }
}
