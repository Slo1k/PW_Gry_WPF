using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace KrakowiakKozlowski.Games.DAOSQL
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source=..\\..\\..\\..\\KrakowiakKozlowski.Games.DAOSQL\\bin\\Debug\\net7.0\\DAOSQL.db")
            .LogTo(message => Debug.WriteLine(message), LogLevel.Information);
        }
        public DbSet<DO.Producer> Producers { get; set; }
        public DbSet<DO.Game> Games { get; set; }
    }
}
