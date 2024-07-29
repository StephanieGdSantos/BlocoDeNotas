using BlocoDeNotas.Data;
using BlocoDeNotas.Models;
using Microsoft.EntityFrameworkCore;

namespace BlocoDeNotas.Repositorio
{
    public class NotasRepositorio : INotasRepositorio
    {
        private readonly BancoContext _bancoContext;
        public NotasRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public async Task<NotasModel> Selecionar(int id)
        {
            return await _bancoContext.Nota.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<NotasModel>> ListarNotas(int usuarioID)
        {
            return await _bancoContext.Nota.Where(nota => nota.UsuarioId.Equals(usuarioID)).ToListAsync();
        }
        public async Task<NotasModel> Adicionar(NotasModel nota)
        {
            _bancoContext.Nota.AddAsync(nota);
            await _bancoContext.SaveChangesAsync();
            return nota;
        }
        public async Task<NotasModel> Editar(NotasModel nota)
        {
            NotasModel notaDB = await Selecionar(nota.Id);
            if (notaDB == null) throw new Exception("Houve um erro na atualização da nota.");

            notaDB.Titulo = nota.Titulo;
            notaDB.Descricao = nota.Descricao;
            notaDB.DataCriacao = DateTime.Now.ToString("dd/MM/yyyy");

            _bancoContext.Nota.Update(notaDB);
            await _bancoContext.SaveChangesAsync();
            return notaDB;
        }
        public async Task<bool> Excluir(int id)
        {
            NotasModel notaDB = await Selecionar(id);

            if (notaDB == null) throw new Exception("Houve um erro na exclusão do nota.");

            _bancoContext.Nota.Remove(notaDB);
            await _bancoContext.SaveChangesAsync();
            return true;
        }
    }
}
