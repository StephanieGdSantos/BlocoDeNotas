using BlocoDeNotas.Data;
using BlocoDeNotas.Models;

namespace BlocoDeNotas.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _bancoContext;
        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public UsuarioModel CriarConta(UsuarioModel usuario)
        {
            _bancoContext.Usuario.Add(usuario);
            _bancoContext.SaveChanges();
            return usuario;
        }

        public UsuarioModel Editar(UsuarioModel usuario)
        {
            int usuarioID = usuario.Id;
            if (usuarioID == null) throw new Exception("Houve um erro na atualização da conta.");

            UsuarioModel UsuarioBD = _bancoContext.Usuario.FirstOrDefault(u => u.Id == usuarioID);
            UsuarioBD.Nome = usuario.Nome;
            UsuarioBD.Email = usuario.Email;
            UsuarioBD.Senha = usuario.Senha;

            _bancoContext.Usuario.Update(UsuarioBD);
            _bancoContext.SaveChanges();
            return UsuarioBD;
        }

        public UsuarioModel Selecionar(int id)
        {
            UsuarioModel UsuarioBD = _bancoContext.Usuario.FirstOrDefault(u => u.Id == id);
            return UsuarioBD;
        }

        public bool Excluir(int id)
        {
            UsuarioModel UsuarioBD = Selecionar(id);

            if (UsuarioBD == null) throw new Exception("Houve um erro na exclusão do usuário.");

            _bancoContext.Usuario.Remove(UsuarioBD);
            _bancoContext.SaveChanges();
            return true;
        }
    }
}
