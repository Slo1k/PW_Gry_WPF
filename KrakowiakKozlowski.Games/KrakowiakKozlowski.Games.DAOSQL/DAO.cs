using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KrakowiakKozlowski.Games.CORE;
using KrakowiakKozlowski.Games.INTERFACES;
using Microsoft.EntityFrameworkCore;

namespace KrakowiakKozlowski.Games.DAOSQL
{
    public class DAO : IDAO
    {
        DataContext context;

        public DAO()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlite("Data source=DAOSQL.db");
            context = new DataContext();
        }

        public IEnumerable<IGame> GetAllGames()
        {
			return context.Games.Select(gm => gm.ToIGame(context.Producers.ToList()));
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return context.Producers.Select(pr => pr.ToIProducer());
        }

        public IGame AddNewGame(int id, string title, int releaseYear, GameGenre genre, int score, int producerId)
        {
            DO.Game newGame = new DO.Game()
            {
                Id = id,
                Title = title,
                ReleaseYear = releaseYear,
                Genre = genre,
                Score = score,
                ProducerID = producerId
            };
			context.Games.Add(newGame);
            context.SaveChanges();
            return newGame.ToIGame(context.Producers.ToList());
        }

        public IProducer AddNewProducer(int id, string name, string country)
        {
            DO.Producer newProducer = new DO.Producer()
            {
                Id = id,
                Name = name,
                Country = country
            };
            context.Producers.Add(newProducer);
            context.SaveChanges();
            return newProducer.ToIProducer();
        }

        public void UpdateGame(IGame game)
        {
            var gameToUpdate = context.Games.FirstOrDefault(gm => gm.Id.Equals(game.Id));
            gameToUpdate.Title = game.Title;
            gameToUpdate.Score = game.Score;
            gameToUpdate.ReleaseYear = game.ReleaseYear;
            gameToUpdate.Genre = game.Genre;
            gameToUpdate.ProducerID = game.Producer.Id;
            
            context.Games.Entry(gameToUpdate).CurrentValues.SetValues(gameToUpdate);
        }

        public void UpdateProducer(IProducer producer)
        {
            var producerToUpdate = context.Producers.FirstOrDefault(pr => pr.Id.Equals(producer.Id));
            producerToUpdate.Name = producer.Name;
            producerToUpdate.Country = producer.Country;

            context.Producers.Entry(producerToUpdate).CurrentValues.SetValues(producerToUpdate);
        }

        public void RemoveGame(int id)
        {
            var game = context.Games.FirstOrDefault(gm => gm.Id.Equals((id)));
            context.Games.Remove(game);
            context.SaveChanges();
        }

        public void RemoveProducer(int id)
        {
            var producer = context.Producers.FirstOrDefault(pr => pr.Id.Equals((id)));
            context.Producers.Remove(producer);
            context.SaveChanges();
        }

        public IGame GetGameById(int id)
        {
            return context.Games.FirstOrDefault(gm => gm.Equals(id)).ToIGame(context.Producers.ToList());
        }

        public IProducer GetProducerById(int id)
        {
            return context.Producers.FirstOrDefault(pr => pr.Equals(id)).ToIProducer();
        }
    }
}
