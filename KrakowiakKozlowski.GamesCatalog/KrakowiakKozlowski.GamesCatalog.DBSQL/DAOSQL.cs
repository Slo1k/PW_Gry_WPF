using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KrakowiakKozlowski.GamesCatalog.INTERFACES;

namespace KrakowiakKozlowski.GamesCatalog.DBSQL
{
    public class DAOSQL: DbContext, IDAO
    {
        public DbSet<GameDBSQL> GameRelation { get; set; }
        public DbSet<ProducerDBSQL> ProducerRelation { get; set; }

        public string DbPath { get; }

        public DAOSQL()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join("", "gamescatalog.db");
        }

        public DAOSQL(string dbFilePath)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(dbFilePath);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite($"Data Source={DbPath}");

        public IProducer AddNewProducer(IProducer producer)
        {
            producer.Id = ProducerRelation.Max(prod => prod.Id) + 1;
            Add(new  ProducerDBSQL()
            {
                Id = producer.Id,
                Name = producer.Name,
                Country = producer.Country,
            });

            SaveChanges();
            return producer;
        }

        public IGame AddNewGame(IGame game)
        {
            game.Id = GameRelation.Max(gm => gm.Id) + 1;
            Add(new GameDBSQL()
            {
                Id = game.Id,
                Genre = game.Genre,
                ReleaseYear = game.ReleaseYear,
                Score = game.Score,
                Title = game.Title,
                ProducerId = game.Producer.Id,
            });

            SaveChanges(); 
            return game;
        }

        public IEnumerable<IProducer> GetAllProducers() => ProducerRelation.Select(producer => producer.ToIProducer());
        public IEnumerable<IGame> GetAllGames() => GameRelation.Select(game => game.ToIGame(ProducerRelation.ToList()));

        public void RemoveProducer(int id)
        {
            var producer = ProducerRelation.FirstOrDefault(prod => prod.Id.Equals(id));
            Remove(producer);
            SaveChanges();
        }

        public void RemoveGame(int id)
        {
            var game = GameRelation.FirstOrDefault(gm =>  gm.Id.Equals(id));
            Remove(game);
            SaveChanges();
        }

        public void UpdateProducer(IProducer updatedProducer)
        {
            var producer = ProducerRelation.FirstOrDefault(prod => prod.Id.Equals(updatedProducer.Id));
            producer.Name = updatedProducer.Name;
            producer.Country = updatedProducer.Country;

            Entry(producer).CurrentValues.SetValues(producer);
        }

        public void UpdateGame(IGame updatedGame)
        {
            var game = GameRelation.FirstOrDefault(gm => gm.Id.Equals(gm.Id));
            game.Title = updatedGame.Title;
            game.ProducerId = updatedGame.Producer.Id;
            game.ReleaseYear = updatedGame.ReleaseYear;
            game.Score = updatedGame.Score;
            game.Genre = updatedGame.Genre;

            Entry(game).CurrentValues.SetValues(game);
        }
    }
}
