using movieStoreDash.Data.models;
using movieStoreDash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RestSharp;

namespace movieStoreDash.Services
{
    public class MovieStoreDashRepository : IMovieStoreDashRepository
    {
        public ActorDTO GetActorInfo(int actorId)
        {
            //"~/image/ospan-ali-Dh-Ip3YKAEM-unsplash.jpg"
            //"~/image/mathilde-langevin-e6UgBVsXecA-unsplash.jpg"

            string imgUrl = string.Empty;

            if (actorId % 2 > 0)
                imgUrl = "/image/ospan-ali-Dh-Ip3YKAEM-unsplash.jpg";
            else
                imgUrl = "/image/mathilde-langevin-e6UgBVsXecA-unsplash.jpg";

            using (var db = new sakilaContext()) 
            {
                var actor = db.Actor.Where(a => a.ActorId == actorId).FirstOrDefault();

                ActorDTO aDTO = new ActorDTO();

                aDTO.actorID = actorId;
                aDTO.bio = "Pork chop cow pig cupim. Prosciutto sirloin bacon short loin pork chop, tail beef ribs pork rump. Chuck shoulder tongue porchetta, capicola jerky venison. Kevin burgdoggen biltong brisket. Bresaola pig pastrami tri-tip shoulder chuck, kielbasa swine ham pork chop filet mignon pork belly chislic burgdoggen ribeye. Ground round landjaeger tenderloin meatloaf beef ball tip fatback spare ribs filet mignon boudin meatball.";
                aDTO.imageUrl = imgUrl;
                aDTO.firstname = actor.FirstName;
                aDTO.lastname = actor.LastName;

                

                var actorSocialMedia = db.Socialmedia.Where(a => a.ActorId == actorId).FirstOrDefault();
                if (actorSocialMedia != null)
                {
                    aDTO.socialMediaURL = actorSocialMedia.Url;

                    aDTO.autoOpenURL = CheckSocialMediaResponseHeaders(actorSocialMedia.Url);

                }
                else
                    aDTO.socialMediaURL = string.Empty;
               

                return aDTO;
            }
        }

        private bool CheckSocialMediaResponseHeaders(string urlToCheck) 
        {
            var client = new RestClient(urlToCheck);//"https://www.instagram.com/p/CFujbiBBSeh/");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
           // request.AddHeader("Cookie", "ig_did=2C870A38-D1B7-440D-B865-6A37C6750668; csrftoken=5zViP50aHkQniVUg3TU3OeVn9EYTSj12; mid=X3o4oQAEAAF6SbAP_3PYnu7tRUO2; urlgen=\"{\\\"68.230.105.87\\\": 22773}:1kPBAH:3_xc_JM0e-sQnf-FuiIWtQ4y2-I\"");
            IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);

            var xFrameOptions = response.Headers.ToList()
                .Find(x => x.Name == "X-Frame-Options");

            if (xFrameOptions != null) 
            {
                string headerVal = xFrameOptions.Value.ToString().ToUpper();

                if (headerVal == "SAMEORIGIN" || headerVal == "DENY")
                    return true;
            }


            var contentSecurityPolicy = response.Headers.ToList()
                .Find(x => x.Name == "Content-Security-Policy");

            if (contentSecurityPolicy != null) 
            {
                string headerVal = contentSecurityPolicy.Value.ToString().ToUpper();

                if (headerVal.Contains("FRAME-ANCESTORS 'SELF'"))
                    return true;
             
            }

 
            return false;
        }

        public List<Actor> GetFilmActors(int filmId)
        {
            using (var db = new sakilaContext()) 
            {
                var filmActors = (from filmActor in db.FilmActor
                                  join actor in db.Actor on filmActor.ActorId equals actor.ActorId
                                  where filmActor.FilmId == filmId
                                  select actor).ToList();

                return filmActors;
            }
                 
        }

        public HomeIndexDTO GetHomeIndexDTO()
        {
            using (var db = new sakilaContext()) 
            {
                var films = db.Film.ToList(); //.GetRange(0, 20);

                HomeIndexDTO homeIndexDTO = new HomeIndexDTO();

                homeIndexDTO.films = films;

                return homeIndexDTO;
            }
        }

        public void UpdateActorInfo(int actorId, string bio, string firstName, string lastName)
        {
            using (var db = new sakilaContext()) 
            {
                var actorToUpdate = db.Actor.Where(a => a.ActorId == actorId).FirstOrDefault();

                if (actorToUpdate != null) 
                {
                    actorToUpdate.FirstName = firstName;
                    actorToUpdate.LastName = lastName;
                    actorToUpdate.LastUpdate = DateTimeOffset.Now;

                    db.SaveChanges();
                }
            }
        }
    }
}
