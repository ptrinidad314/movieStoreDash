using movieStoreDash.Data.models;
using movieStoreDash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace movieStoreDash.Services
{
    public interface IMovieStoreDashRepository
    {
        HomeIndexDTO GetHomeIndexDTO();
        List<Actor> GetFilmActors(int filmId);
        ActorDTO GetActorInfo(int actorId);
        void UpdateActorInfo(int actorId, string bio, string firstName, string lastName);
    }
}
