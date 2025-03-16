using Steam_HW1.DAL;

namespace Steam_HW1.Models
{
    public class GamesUser
    {
        int appId;
        int userId;


        public GamesUser(int appId, int userId)
        {
             AppId = appId;
            UserId = userId;

        }

        public GamesUser() { }

        public static List<GamesUser> Read(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.GetUserGames(id);
        }

        public static bool Insert(GamesUser gamesUser)
        {
            DBservices dbs = new DBservices();
            List<GamesUser> gamesList = dbs.GetUserGames(gamesUser.UserId);
            if(gamesList.Any( g  => g.appId==gamesUser.appId )) {
             
                    return false; // ID already exists
                }
        
           dbs.InsertGameUser(gamesUser);
            return true;
        }

        public static int DeleteGamesUser(GamesUser gamesUser)
        {
            DBservices dbs = new DBservices();
            return dbs.deleteGame(gamesUser);

        }


        public int AppId { get => appId; set => appId = value; }
        public int UserId { get => userId; set => userId = value; }

    }
}
