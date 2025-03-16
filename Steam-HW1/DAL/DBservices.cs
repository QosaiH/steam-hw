using Steam_HW1.Models;
using System.Data.SqlClient;

namespace Steam_HW1.DAL
{
    /// <summary>
    /// DBServices is a class created by me to provides some DataBase Services
    /// </summary>
    public class DBservices
    {

        public DBservices()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //--------------------------------------------------------------------------------------------------
        // This method creates a connection to the database according to the connectionString name in the web.config 
        //--------------------------------------------------------------------------------------------------
        public SqlConnection connect(String conString)
        {

            // read the connection string from the configuration file
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
            string cStr = configuration.GetConnectionString("myProjDB");
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

        //---------------------------------------------------------------------------------
        // Create the SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateCommandWithStoredProcedure(String spName, SqlConnection con, Game game)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            return cmd;
        }
        //---------------------------------------------------------------------------------
        // Create sql command
        //---------------------------------------------------------------------------------
        private SqlCommand CreateCommandWithStoredProcedureGeneral(String spName, SqlConnection con, Dictionary<string, object> paramDic)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            if (paramDic != null)
                foreach (KeyValuePair<string, object> param in paramDic)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);

                }


            return cmd;
        }

        //---------------------------------------------------------------------------------
        // Get Games List From DB
        //---------------------------------------------------------------------------------
        public List<Game> GetGamesList()
        {
            SqlConnection con;
            SqlCommand cmd;
            SqlDataReader reader;
            List<Game> games = new List<Game>();

            try
            {
                con = connect("igroup15_test2"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            // Create the command for the stored procedure or SQL query
            cmd = CreateCommandWithStoredProcedure("GetGamesList", con, null);

            reader = cmd.ExecuteReader(); // Execute the command and get a data reader

            // Read the data
            while (reader.Read())
            {
                Game game = new Game
                {
                    AppID = reader.GetInt32(reader.GetOrdinal("AppID")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    ReleaseDate = reader.GetDateTime(reader.GetOrdinal("Release_date")),
                    Price = reader.GetDouble(reader.GetOrdinal("Price")),
                    Description = reader.GetString(reader.GetOrdinal("Description")),
                    HeaderImage = reader.GetString(reader.GetOrdinal("Header_image")),
                    Website = reader.GetString(reader.GetOrdinal("Website")),
                    Windows = reader.GetBoolean(reader.GetOrdinal("Windows")),
                    Mac = reader.GetBoolean(reader.GetOrdinal("Mac")),
                    Linux = reader.GetBoolean(reader.GetOrdinal("Linux")),
                    ScoreRank = reader.GetInt32(reader.GetOrdinal("Score_rank")),
                    Recommendations = reader.GetString(reader.GetOrdinal("Recommendations")),
                    Publisher = reader.GetString(reader.GetOrdinal("Developers")),
                    NumberOfPurchases = reader.GetInt32(reader.GetOrdinal("NumberOfPurchases")),
                    Categories = reader.GetString(reader.GetOrdinal("Categories")),
                    Geners = reader.GetString(reader.GetOrdinal("Genres")),
                    Tags = reader.GetString(reader.GetOrdinal("Tags"))
                };

                games.Add(game); // Add the game to the list
            }
            try
            {
                return games;
            }
            catch (Exception ex)
            {
                // Write to log
                throw; // Rethrow the exception
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //---------------------------------------------------------------------------------
        // Get GamesUser List From DB
        //---------------------------------------------------------------------------------
        public List<GamesUser> GetUserGames(int id)
        {
            SqlConnection con;
            SqlCommand cmd;
            SqlDataReader reader;
            List<GamesUser> GamesUsers = new List<GamesUser>();
            

            try
            {
                con = connect("igroup15_test2"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            // Create the command for the stored procedure or SQL query
            cmd = CreateCommandWithStoredProcedureGeneral("GetUserGames", con, new Dictionary<string, object> { { "@id", id } });

            reader = cmd.ExecuteReader(); // Execute the command and get a data reader

            // Read the data
            while (reader.Read())
            {
                GamesUser gamesUser = new GamesUser
                { 
                AppId = reader.GetInt32(reader.GetOrdinal("AppID")),
                UserId = reader.GetInt32(reader.GetOrdinal("id"))
            };
                GamesUsers.Add(gamesUser); // Add the game to the list
            }
            try
            {
                return GamesUsers;
            }
            catch (Exception ex)
            {
                // Write to log
                throw; // Rethrow the exception
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }


        //---------------------------------------------------------------------------------
        // Get Users List From DB
        //---------------------------------------------------------------------------------
        public List<Userr> GetUsersList()
        {
            SqlConnection con;
            SqlCommand cmd;
            SqlDataReader reader;
            List<Userr> users = new List<Userr>();

            try
            {
                con = connect("igroup15_test2"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            // Create the command for the stored procedure or SQL query
            cmd = CreateCommandWithStoredProcedure("GetUsersList", con, null);

            reader = cmd.ExecuteReader(); // Execute the command and get a data reader

            // Read the data
            while (reader.Read())
            {
                Userr userr = new Userr
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Name = reader.GetString(reader.GetOrdinal("name")),
                    Email = reader.GetString(reader.GetOrdinal("email")),
                    Password = reader.GetString(reader.GetOrdinal("password")),
                    IsActive   = reader.GetBoolean(reader.GetOrdinal("isActive")),
                };

                users.Add(userr); // Add the game to the list
            }
            try
            {
                return users;
            }
            catch (Exception ex)
            {
                // Write to log
                throw; // Rethrow the exception
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }



        //---------------------------------------------------------------------------------
        // Insert New user to DB 
        //--------------------------------------------------------------------------------*/==
        public int Insertuser(Userr userr)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("igroup15_test2"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@name", userr.Name);
            paramDic.Add("@email", userr.Email);
            paramDic.Add("@password", userr.Password);

            cmd = CreateCommandWithStoredProcedureGeneral("InsertUser ", con, paramDic);          // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public int userLogin(string email, string password)
        {

            SqlConnection con;
            SqlCommand cmd;
            SqlDataReader reader;


            try
            {
                con = connect("igroup15_test2"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@email", email);
            paramDic.Add("@password", password);

            cmd = CreateCommandWithStoredProcedureGeneral("UserExist", con, paramDic);          // create the command
            reader = cmd.ExecuteReader(); // Execute the command and get a data reader
            reader.Read();

            try
            {
                bool isActive = reader.GetBoolean(reader.GetOrdinal("isActive"));
                if (isActive == true)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            catch
            {
                return 0;
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public int UpdateUser(Userr userr)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("igroup15_test2"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@name", userr.Name);
            paramDic.Add("@email", userr.Email);
            paramDic.Add("@password", userr.Password);
            paramDic.Add("@userid", userr.Id);

            cmd = CreateCommandWithStoredProcedureGeneral("UpdateUserInfo ", con, paramDic);

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public int UpdateIsActive(int id, bool isActive)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("igroup15_test2"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@isActive", isActive);
            paramDic.Add("@id", id);

            cmd = CreateCommandWithStoredProcedureGeneral("isActiveUpdate ", con, paramDic);

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //---------------------------------------------------------------------------------
        // Insert New GameUser to DB 
        //--------------------------------------------------------------------------------*/==
        public int InsertGameUser(GamesUser gamesUser)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("igroup15_test2"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@gameID", gamesUser.AppId);
            paramDic.Add("@userID", gamesUser.UserId);

            cmd = CreateCommandWithStoredProcedureGeneral("BuyGame", con, paramDic);          // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public int deleteGame(GamesUser gamesUser)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("igroup15_test2"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@gameID", gamesUser.AppId);
            paramDic.Add("@userID", gamesUser.UserId);

            cmd = CreateCommandWithStoredProcedureGeneral("DeleteGame", con, paramDic);

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public Game GetGameById(int id)
        {
            SqlConnection con;
            SqlCommand cmd;
            SqlDataReader reader;

            try
            {
                con = connect("igroup15_test2"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            // Create the command for the stored procedure or SQL query
            cmd = CreateCommandWithStoredProcedureGeneral("GetGameById", con, new Dictionary<string, object> { { "@AppID", id } });

            reader = cmd.ExecuteReader(); // Execute the command and get a data reader
            reader.Read();

                Game game = new Game
                {
                    AppID = reader.GetInt32(reader.GetOrdinal("AppID")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    ReleaseDate = reader.GetDateTime(reader.GetOrdinal("Release_date")),
                    Price = reader.GetDouble(reader.GetOrdinal("Price")),
                    Description = reader.GetString(reader.GetOrdinal("Description")),
                    HeaderImage = reader.GetString(reader.GetOrdinal("Header_image")),
                    Website = reader.GetString(reader.GetOrdinal("Website")),
                    Windows = reader.GetBoolean(reader.GetOrdinal("Windows")),
                    Mac = reader.GetBoolean(reader.GetOrdinal("Mac")),
                    Linux = reader.GetBoolean(reader.GetOrdinal("Linux")),
                    ScoreRank = reader.GetInt32(reader.GetOrdinal("Score_rank")),
                    Recommendations = reader.GetString(reader.GetOrdinal("Recommendations")),
                    Publisher = reader.GetString(reader.GetOrdinal("Developers")),
                    NumberOfPurchases = reader.GetInt32(reader.GetOrdinal("NumberOfPurchases"))
                };
            try
            {
                return game;
            }
            catch (Exception ex)
            {
                // Write to log
                throw; // Rethrow the exception
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        public List<Game> RankFilter(int rank, int id)
        {
            SqlConnection con;
            SqlCommand cmd;
            SqlDataReader reader;
            List<Game> games = new List<Game>();

            try
            {
                con = connect("igroup15_test2"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@Score_rank", rank);
            paramDic.Add("@id", id);



            // Create the command for the stored procedure or SQL query
            cmd = CreateCommandWithStoredProcedureGeneral("RankFilter", con, paramDic);

            reader = cmd.ExecuteReader(); // Execute the command and get a data reader

            // Read the data
            while (reader.Read())
            {
                Game game = new Game
                {
                    AppID = reader.GetInt32(reader.GetOrdinal("AppID")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    ReleaseDate = reader.GetDateTime(reader.GetOrdinal("Release_date")),
                    Price = reader.GetDouble(reader.GetOrdinal("Price")),
                    Description = reader.GetString(reader.GetOrdinal("Description")),
                    HeaderImage = reader.GetString(reader.GetOrdinal("Header_image")),
                    Website = reader.GetString(reader.GetOrdinal("Website")),
                    Windows = reader.GetBoolean(reader.GetOrdinal("Windows")),
                    Mac = reader.GetBoolean(reader.GetOrdinal("Mac")),
                    Linux = reader.GetBoolean(reader.GetOrdinal("Linux")),
                    ScoreRank = reader.GetInt32(reader.GetOrdinal("Score_rank")),
                    Recommendations = reader.GetString(reader.GetOrdinal("Recommendations")),
                    Publisher = reader.GetString(reader.GetOrdinal("Developers")),
                    NumberOfPurchases = reader.GetInt32(reader.GetOrdinal("NumberOfPurchases"))
                };

                games.Add(game); // Add the game to the list
            }
            try
            {
                return games;
            }
            catch (Exception ex)
            {
                // Write to log
                throw; // Rethrow the exception
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public List<Game> PriceFilter(double price, int id)
        {
            SqlConnection con;
            SqlCommand cmd;
            SqlDataReader reader;
            List<Game> games = new List<Game>();

            try
            {
                con = connect("igroup15_test2"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@price",price );
            paramDic.Add("@id", id);


            // Create the command for the stored procedure or SQL query
            cmd = CreateCommandWithStoredProcedureGeneral("PriceFilter", con, paramDic);

            reader = cmd.ExecuteReader(); // Execute the command and get a data reader

            // Read the data
            while (reader.Read())
            {
                Game game = new Game
                {
                    AppID = reader.GetInt32(reader.GetOrdinal("AppID")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    ReleaseDate = reader.GetDateTime(reader.GetOrdinal("Release_date")),
                    Price = reader.GetDouble(reader.GetOrdinal("Price")),
                    Description = reader.GetString(reader.GetOrdinal("Description")),
                    HeaderImage = reader.GetString(reader.GetOrdinal("Header_image")),
                    Website = reader.GetString(reader.GetOrdinal("Website")),
                    Windows = reader.GetBoolean(reader.GetOrdinal("Windows")),
                    Mac = reader.GetBoolean(reader.GetOrdinal("Mac")),
                    Linux = reader.GetBoolean(reader.GetOrdinal("Linux")),
                    ScoreRank = reader.GetInt32(reader.GetOrdinal("Score_rank")),
                    Recommendations = reader.GetString(reader.GetOrdinal("Recommendations")),
                    Publisher = reader.GetString(reader.GetOrdinal("Developers")),
                    NumberOfPurchases = reader.GetInt32(reader.GetOrdinal("NumberOfPurchases"))
                };

                games.Add(game); // Add the game to the list
            }
            try
            {
                return games;
            }
            catch (Exception ex)
            {
                // Write to log
                throw; // Rethrow the exception
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public Object GetUsersinfo()
        {
            SqlConnection con;
            SqlCommand cmd;
            SqlDataReader reader;
            List<Object> users = new List<Object>();

            try
            {
                con = connect("igroup15_test2"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            // Create the command for the stored procedure or SQL query
            cmd = CreateCommandWithStoredProcedure("usersInfo", con, null);
            reader = cmd.ExecuteReader(); // Execute the command and get a data reader

            // Read the data
            while (reader.Read())
            {
                users.Add(new
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Name = reader.GetString(reader.GetOrdinal("name")),
                    BoughtGames = reader.GetInt32(reader.GetOrdinal("bought_games")),
                    TotalSpent = Math.Round(reader.GetDouble(reader.GetOrdinal("total_spent")),2),
                IsActive = reader.GetBoolean(reader.GetOrdinal("isActive")),
                });
            }
            try
            {
                return users;
            }
            catch (Exception ex)
            {
                // Write to log
                throw; // Rethrow the exception
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public Object GetGamesinfo()
        {
            SqlConnection con;
            SqlCommand cmd;
            SqlDataReader reader;
            List<Object> games = new List<Object>();

            try
            {
                con = connect("igroup15_test2"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            // Create the command for the stored procedure or SQL query
            cmd = CreateCommandWithStoredProcedure("GamesInfo", con, null);
            reader = cmd.ExecuteReader(); // Execute the command and get a data reader

            // Read the data
            while (reader.Read())
            {
                games.Add(new
                {
                    Id = reader.GetInt32(reader.GetOrdinal("AppID")),
                    Title = reader.GetString(reader.GetOrdinal("title")),
                    Downloads = reader.GetInt32(reader.GetOrdinal("Downloads")),
                    TotalRevenue = reader.GetDouble(reader.GetOrdinal("total_Revenue")),
                });
            }
            try
            {
                return games;
            }
            catch (Exception ex)
            {
                // Write to log
                throw; // Rethrow the exception
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

    }
    }
       