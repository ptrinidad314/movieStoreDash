using movieStoreDash.Data.models;
using movieStoreDash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RestSharp;
using MySql.Data.MySqlClient;

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

            if (response.ResponseUri.Scheme == "http")
                return true;

 
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

        public void UpdateActorInfo(int actorId, string bio, string firstName, string lastName, string socialMediaURL)
        {
            using (var db = new sakilaContext()) 
            {
                var actorToUpdate = db.Actor.Where(a => a.ActorId == actorId).FirstOrDefault();

                if (actorToUpdate != null) 
                {
                    actorToUpdate.FirstName = firstName;
                    actorToUpdate.LastName = lastName;
                    actorToUpdate.LastUpdate = DateTimeOffset.Now;
                    
                }

                var socialMedia = db.Socialmedia.Where(s => s.ActorId == actorId).FirstOrDefault();

                if (socialMedia != null)
                {
                    socialMedia.Url = socialMediaURL;
                }
                else if(socialMediaURL != string.Empty)
                {
                    Socialmedia actorSocialMedia = new Socialmedia();
                    actorSocialMedia.ActorId = (short)actorId;
                    actorSocialMedia.Url = socialMediaURL;

                    db.Socialmedia.Add(actorSocialMedia);
                }

                db.SaveChanges();

            }
        }

        public void RunTest()
        {
            bool testRes;

            string connStr = "server=localhost;port=3306;database=sakila;uid=root;password=Abc123!;";

            string cmdPart1 = "UPDATE table1 SET value = (CASE id ";

            string cmdPart2 = string.Empty;

            string cmdPart3 = " END) WHERE id IN(";  //1, 2, 3, 4, 5);

            string cmdPart4 = string.Empty;

            string cmdPart5 = ");";

            var list = new List<KeyValuePair<int, string>>();
            list.Add(new KeyValuePair<int, string>(1, "John"));
            list.Add(new KeyValuePair<int, string>(2, "Mike"));
            list.Add(new KeyValuePair<int, string>(3, "Scottie"));
            list.Add(new KeyValuePair<int, string>(4, "Horace"));
            list.Add(new KeyValuePair<int, string>(5, "Bill"));

            using (MySqlConnection myConnection = new MySqlConnection(connStr)) 
            {
                //string myUpdateQuery =
                foreach (var kvp in list) 
                {
                    cmdPart2 = cmdPart2 + "WHEN " + kvp.Key.ToString() + " THEN '" + kvp.Value + "' ";

                    cmdPart4 = cmdPart4 + kvp.Key.ToString() + ",";
                }

                //string founderMinus1 = founder.Remove(founder.Length - 1, 1);
                cmdPart4 = cmdPart4.Remove(cmdPart4.Length - 1, 1);

                string myUpdateQuery = cmdPart1 + cmdPart2 + cmdPart3 + cmdPart4 + cmdPart5;

                MySqlCommand mySqlCommand = new MySqlCommand(myUpdateQuery);
                mySqlCommand.Connection = myConnection;
                myConnection.Open();

                var test = mySqlCommand.ExecuteNonQuery();

                if (test > 0)
                {
                    testRes = true;
                }
                else 
                {
                    testRes = false;
                }

            }



        }

        /*
          var list = new List<KeyValuePair<string, int>>();
        list.Add(new KeyValuePair<string, int>("Cat", 1));
        list.Add(new KeyValuePair<string, int>("Dog", 2));
        list.Add(new KeyValuePair<string, int>("Rabbit", 
         
         */

        /*
         UPDATE table1 
            SET value = 
              (CASE id WHEN 1 THEN 'Karl'
                       WHEN 2 THEN 'Tom'
                       WHEN 3 THEN 'Mary'
                       WHEN 4 THEN 'Yellow'
                       WHEN 5 THEN 'Wonton'
               END)
            WHERE id IN (1,2,3,4,5);
         */

        /*
         public void InsertRow(string myConnectionString)
          {
          // If the connection string is null, use a default.
          if(myConnectionString == "")
          {
          myConnectionString = "Database=Test;Data Source=localhost;User Id=username;Password=pass";
          }
          MySqlConnection myConnection = new MySqlConnection(myConnectionString);
          string myInsertQuery = "INSERT INTO Orders (id, customerId, amount) Values(1001, 23, 30.66)";
          MySqlCommand myCommand = new MySqlCommand(myInsertQuery);
          myCommand.Connection = myConnection;
          myConnection.Open();
          myCommand.ExecuteNonQuery();
          myCommand.Connection.Close();
          }
         
         */

        /*
           update user
             set ext_flag = 'Y', admin_role = 'admin', ext_id = 
             case 
             when user_id = 2 then 345
             when user_id = 4 then 456
             when user_id = 5 then 789
             end
             **WHERE user_id  in (2,4,5)**
          */
    }
}
