using BlocoDeNotas.Models;
using Microsoft.EntityFrameworkCore;

namespace BlocoDeNotas.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {

        }

        public DbSet<NotasModel> Nota { get; set; }
        public DbSet<UsuarioModel> Usuario { get; set; }
    }
}