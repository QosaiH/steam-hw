using Steam_HW1.DAL;

namespace Steam_HW1.Models
{
    public class Game
    {
        int appID;
        string name;
        DateTime releaseDate;
        double price;
        string description;
        string headerImage;
        string website;
        bool windows;
        bool mac;
        bool linux;
        int scoreRank;
        string recommendations;
        string publisher;
        int numberOfPurchases = 0;
        string geners;
        string tags;
        string categories;

        public Game(int appID, string name, DateTime releaseDate, double price, string description, string headerImage,
         string website, bool windows, bool mac, bool linux, int scoreRank, string recommendations, string publisher, int numberOfPurchases, string geners, string tags, string categories)
        {
            AppID = appID;
            Name = name;
            ReleaseDate = releaseDate;
            Price = price;
            Description = description;
            HeaderImage = headerImage;
            Website = website;
            Windows = windows;
            Mac = mac;
            Linux = linux;
            ScoreRank = scoreRank;
            Recommendations = recommendations;
            Publisher = publisher;
            NumberOfPurchases = numberOfPurchases;
            Geners = geners;
            Tags = tags;
            Categories = categories;
        }
        public Game() { }

        
        public static Game GetById(int id)
        {
            DBservices dbs = new DBservices();
             return dbs.GetGameById(id);
        }
        
        public static List<Game> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.GetGamesList();
        }

        public static List<Game> GetByPrice(double price, int id)
        {
            DBservices dbs = new DBservices();
            return dbs.PriceFilter(price, id);
        }

        public static List<Game> GetByRank(int scoreRank, int id)
        {
            DBservices dbs = new DBservices();
            return dbs.RankFilter(scoreRank, id);

        }

        public List<Game> DeleteById(int appID)
        {
            DBservices dbs = new DBservices();
            List<Game> gamesList = dbs.GetGamesList();
            for (int i = gamesList.Count - 1; i >= 0; i--)
            {
                if (gamesList[i].appID == appID)
                {
                    gamesList.RemoveAt(i);
                }
            }
            return gamesList;
        }

        public static Object GetGamesInfo()
        {
            DBservices dbs = new DBservices();
            return dbs.GetGamesinfo();
        }

        public int AppID { get => appID; set => appID = value; }
        public string Name { get => name; set => name = value; }
        public DateTime ReleaseDate { get => releaseDate; set => releaseDate = value; }
        public double Price { get => price; set => price = value; }
        public string Description { get => description; set => description = value; }
        public string HeaderImage { get => headerImage; set => headerImage = value; }
        public string Website { get => website; set => website = value; }
        public bool Windows { get => windows; set => windows = value; }
        public bool Mac { get => mac; set => mac = value; }
        public bool Linux { get => linux; set => linux = value; }
        public int ScoreRank { get => scoreRank; set => scoreRank = value; }
        public string Recommendations { get => recommendations; set => recommendations = value; }
        public string Publisher { get => publisher; set => publisher = value; }
        public int NumberOfPurchases { get => numberOfPurchases; set => numberOfPurchases = value; }
        public string Geners { get => geners; set => geners = value; }
        public string Tags { get => tags; set => tags = value; }
        public string Categories { get => categories; set => categories = value; }


    }
}
