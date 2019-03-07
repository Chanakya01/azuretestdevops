using Microsoft.EntityFrameworkCore;
using Valuelabs.API.Models;

namespace Valuelabs.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options){}

    
        public DbSet<Value> Values {get; set;}
        public DbSet<User> Users {get;set;}
        public DbSet<Photo> Photos {get;set;}
    }
}