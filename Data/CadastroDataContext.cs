using CadastroApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroApi.Data
{
    public class CadastroDataContext : DbContext
    {
        public CadastroDataContext(DbContextOptions<CadastroDataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
