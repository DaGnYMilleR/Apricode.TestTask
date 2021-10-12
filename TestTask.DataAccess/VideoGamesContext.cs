using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TestTask.Domain;

namespace TestTask.DataAccess
{
    public class VideoGamesContext : DbContext
    {
        public DbSet<DataBaseVideoGame> Games { get; set; }
        
        public VideoGamesContext(DbContextOptions<VideoGamesContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder
                .Entity<DataBaseVideoGame>()
                .Property(e => e.Genres)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<IEnumerable<Genre>>(v));

        }
    }
}