using movieStoreDash.Data.models;
using movieStoreDash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RestSharp;
using MySql.Data.MySqlClient;
using System.Data;

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

        public async Task<int> RunTest() 
        {
            try
            {
                string connStr = "server=localhost;port=3306;database=sakila;uid=root;password=Abc123!;";
                string spName = "spUpdateStaff";
                int updateCnt = 0;

                using (MySqlConnection myConnection = new MySqlConnection(connStr)) 
                {
                    myConnection.Open();

                    MySqlTransaction myTransaction = await myConnection.BeginTransactionAsync();

                    try
                    {
                        using (MySqlCommand myCommand = new MySqlCommand(spName, myConnection, myTransaction)) 
                        {
                            myCommand.CommandType = CommandType.StoredProcedure;
                            myCommand.Parameters.AddWithValue("@staff_id_in",3);
                            myCommand.Parameters.AddWithValue("@first_name", "Bradley3");
                            myCommand.Parameters.AddWithValue("@last_name", "Newell");
                            myCommand.Parameters.AddWithValue("@address_id", 1);
                            myCommand.Parameters.AddWithValue("@picture", null);
                            myCommand.Parameters.AddWithValue("@email", "bradleyN@gmail.com");
                            myCommand.Parameters.AddWithValue("@store_id", 1);
                            myCommand.Parameters.AddWithValue("@active", 1);
                            myCommand.Parameters.AddWithValue("@username", "brad");
                            myCommand.Parameters.AddWithValue("@password", "PASSWORD");
                            myCommand.Parameters.AddWithValue("@last_update", DateTime.Now);
                      
                            updateCnt = await myCommand.ExecuteNonQueryAsync();
                         
                            await myTransaction.CommitAsync();
                        }
                    }
                    catch (Exception ex) 
                    {
                        myTransaction.RollbackAsync();

                        var test = false;
                        return 0;
                    }
                }

                return updateCnt;

            }
            catch (Exception ex) 
            {
                var test = false;
                return 0;
            }
        }

        /*
        CREATE DEFINER=`root`@`localhost` PROCEDURE `spUpdateStaff`(
	            IN staff_id_in tinyint,
                IN first_name varchar(45),
                IN last_name varchar(45),
                IN address_id smallint,
                IN picture blob,
                in email varchar(50),
                in store_id tinyint,
                in active tinyint,
                in username varchar(16),
                in password varchar(40),
                in last_update timestamp
            )
            BEGIN
	
                update staff
                set 
		            first_name = first_name,
		            last_name = last_name,
                    address_id = address_id,
                    picture = picture,
                    email = email,
                    store_id = store_id,
                    active = active,
                    username = username,
                    password = password,
                    last_update = last_update
	            where staff_id = staff_id_in;
    
    
            END 

         */


        public void RunTest_OLD() 
        {

            try
            {

                string connStr = "server=localhost;port=3306;database=sakila;uid=root;password=Abc123!;";

                string spName = "spTranTest";

                using (MySqlConnection myConnection = new MySqlConnection(connStr))
                {
                    myConnection.Open();

                    MySqlTransaction myTransaction = myConnection.BeginTransaction();

                    try
                    {
                        using (MySqlCommand myCommand = new MySqlCommand(spName, myConnection, myTransaction))
                        {
                            if (myCommand.ExecuteNonQuery() > 0)
                            {
                                var test = true;

                                //throw new ArgumentNullException();

                                myTransaction.Commit();
                            }
                            else
                            {
                                var test2 = false;
                            }

                            myTransaction.Commit();
                        }
                    }
                    catch (Exception ex) 
                    {
                        myTransaction.Rollback();

                        var test3 = false;
                    }

                }
            }
            catch (Exception ex) 
            {                

                var test3 = false;
            }

        }

        //public void RunTest()
        //{
        //    bool testRes;

        //    string connStr = "server=localhost;port=3306;database=sakila;uid=root;password=Abc123!;";

        //    string cmdPart1 = "UPDATE table1 SET value = (CASE id ";

        //    string cmdPart2 = string.Empty;

        //    string cmdPart3 = " END) WHERE id IN(";  //1, 2, 3, 4, 5);

        //    string cmdPart4 = string.Empty;

        //    string cmdPart5 = ");";

        //    var list = new List<KeyValuePair<int, string>>();
        //    list.Add(new KeyValuePair<int, string>(1, "John"));
        //    list.Add(new KeyValuePair<int, string>(2, "Mike"));
        //    list.Add(new KeyValuePair<int, string>(3, "Scottie"));
        //    list.Add(new KeyValuePair<int, string>(4, "Horace"));
        //    list.Add(new KeyValuePair<int, string>(5, "Bill"));

        //    using (MySqlConnection myConnection = new MySqlConnection(connStr)) 
        //    {
        //        //string myUpdateQuery =
        //        foreach (var kvp in list) 
        //        {
        //            cmdPart2 = cmdPart2 + "WHEN " + kvp.Key.ToString() + " THEN '" + kvp.Value + "' ";

        //            cmdPart4 = cmdPart4 + kvp.Key.ToString() + ",";
        //        }

        //        //string founderMinus1 = founder.Remove(founder.Length - 1, 1);
        //        cmdPart4 = cmdPart4.Remove(cmdPart4.Length - 1, 1);

        //        string myUpdateQuery = cmdPart1 + cmdPart2 + cmdPart3 + cmdPart4 + cmdPart5;

        //        MySqlCommand mySqlCommand = new MySqlCommand(myUpdateQuery);
        //        mySqlCommand.Connection = myConnection;
        //        myConnection.Open();

        //        var test = mySqlCommand.ExecuteNonQuery();

        //        if (test > 0)
        //        {
        //            testRes = true;
        //        }
        //        else 
        //        {
        //            testRes = false;
        //        }

        //    }



        //}

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
