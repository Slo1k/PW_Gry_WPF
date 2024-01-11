using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KrakowiakKozlowski.Games.INTERFACES;
using Microsoft.EntityFrameworkCore;

namespace KrakowiakKozlowski.Games.DAOSQL
{
    public class DAO : IDAO
    {
        DataContext context;

        public DAO()
        {
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

        public IGame AddNewGame(IGame game)
        {
			context.Games.Add((DO.Game)game);
            return game;
        }

        public IProducer AddNewProducer(IProducer producer)
        {
            context.Producers.Add((DO.Producer)producer);
            return producer;
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
