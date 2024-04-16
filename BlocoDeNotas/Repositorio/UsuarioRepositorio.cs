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
    }
}
