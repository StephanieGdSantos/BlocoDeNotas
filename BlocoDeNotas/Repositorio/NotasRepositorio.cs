using BlocoDeNotas.Data;
using BlocoDeNotas.Models;

namespace BlocoDeNotas.Repositorio
{
    public class NotasRepositorio : INotasRepositorio
    {
        private readonly BancoContext _bancoContext;
        public NotasRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public NotasModel Selecionar(int id)
        {
            return _bancoContext.Nota.FirstOrDefault(x => x.Id == id);
        }
        public List<NotasModel> ListarNotas()
        {
            return _bancoContext.Nota.ToList();
        }
        public NotasModel Adicionar(NotasModel nota)
        {
            _bancoContext.Nota.Add(nota);
            _bancoContext.SaveChanges();
            return nota;
        }
        public NotasModel Editar(NotasModel nota)
        {
            NotasModel notaDB = Selecionar(nota.Id);
            if (notaDB == null) throw new Exception("Houve um erro na atualização da nota.");

            notaDB.Titulo = nota.Titulo;
            notaDB.Descricao = nota.Descricao;
            notaDB.DataCriacao = nota.DataCriacao;

            _bancoContext.Nota.Update(notaDB);
            _bancoContext.SaveChanges();
            return notaDB;
        }
        public bool Excluir(int id)
        {
            NotasModel notaDB = Selecionar(id);

            if (notaDB == null) throw new Exception("Houve um erro na exclusão do nota.");

            _bancoContext.Nota.Remove(notaDB);
            _bancoContext.SaveChanges();
            return true;
        }
    }
}
