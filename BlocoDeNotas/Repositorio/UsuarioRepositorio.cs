using BlocoDeNotas.Data;
using BlocoDeNotas.Models;
using Microsoft.EntityFrameworkCore;

namespace BlocoDeNotas.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _bancoContext;
        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public async Task<UsuarioModel> CriarConta(UsuarioModel usuario)
        {
            _bancoContext.Usuario.Add(usuario);
            await _bancoContext.SaveChangesAsync();
            return usuario;
        }

        public async Task<UsuarioModel> Editar(UsuarioModel usuario)
        {
            int usuarioID = usuario.Id;
            if (usuarioID == null) throw new Exception("Houve um erro na atualização da conta.");

            UsuarioModel UsuarioBD = await _bancoContext.Usuario.FirstOrDefaultAsync(u => u.Id == usuarioID);
            UsuarioBD.Nome = usuario.Nome;
            UsuarioBD.Email = usuario.Email;
            UsuarioBD.Senha = usuario.Senha;

            _bancoContext.Usuario.Update(UsuarioBD);
            await _bancoContext.SaveChangesAsync();
            return UsuarioBD;
        }

        public async Task<UsuarioModel> Selecionar(int id)
        {
            UsuarioModel UsuarioBD = await _bancoContext.Usuario.FirstOrDefaultAsync(u => u.Id == id);
            return UsuarioBD;
        }

        public async Task<bool> Excluir(int id)
        {
            UsuarioModel UsuarioBD = await Selecionar(id);

            if (UsuarioBD == null) throw new Exception("Houve um erro na exclusão do usuário.");

            _bancoContext.Usuario.Remove(UsuarioBD);
            await _bancoContext.SaveChangesAsync();
            return true;
        }
    }
}
