internal class Program
{

    private const string VERSION = "1.0";

    public class Player
    {
        private string? _name;
        private string? _gender;
        public int Id { get; private set; }

        public string? Name
        {
            get => _name;
            set => _name = value;
        }

        public string? Gender
        {
            get => _gender;
            set => _gender = value;
        }

        public Player(string? name, string gender)
        {
            _name = name;
            _gender = gender;
            Id = PlayersManager.PLAYERS.Count + 1;
            PlayersManager.PLAYERS.Add(Id, this);
        }

        public override string ToString() => $"{Id};{Name};{Gender}";
    }

    public class PlayersManager
    {
        public static SortedList<int, Player> PLAYERS = new SortedList<int, Player>();

        public static void DeletePlayer(string id)
        {
            string tempFile = "players.temp.yml";
            try
            {
                using (StreamReader reader = new StreamReader("players.yml"))
                using (StreamWriter writer = new StreamWriter(tempFile))
                {
                    string line;
                    bool found = false;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(';');
                        if (parts.Length > 0 && parts[0] == id)
                        {
                            found = true;
                            continue;
                        }
                        writer.WriteLine(line);
                    }
                    if (!found) Console.WriteLine($"Player with ID {id} not found.");
                }
                File.Delete("players.yml");
                File.Move(tempFile, "players.yml");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Local-DB Delete player error: {e.Message}");
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
        }

        public static void SaveAllPlayers()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("players.yml"))
                {
                    foreach (var player in PLAYERS.Values)
                    {
                        writer.WriteLine(player);
                    }
                    Console.WriteLine($"Local-DB Saved {PLAYERS.Count} players!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Local-DB Save players error: {e.Message}");
            }
        }
    }

    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("(!) List of commands\n\n" +
                "1. /createPlayer <name> <gender> - Create an object of player\n" +
                "2. /deletePlayer <id> - Delete specified player\n" +
                "3. /savePlayers - Save all players in yml file\n" +
                "4. /playersList - Get list of registered players\n" +
                "5. /whoseCheese - Says which player has a cheese in his pocket (random)\n" +
                "6. /clearChat - Clear console's chat\n" +
                "7. /version - Version of the application");

            Console.Write("\n(!) Select needed number of command: ");
            switch (Console.ReadLine())
            {
                case "1":
                    CreatePlayer();
                    break;
                case "2":
                    DeletePlayer();
                    break;
                case "3":
                    PlayersManager.SaveAllPlayers();
                    break;
                case "4":
                    DisplayPlayersList();
                    break;
                case "5":
                    WhoseCheese();
                    break;
                case "6":
                    Console.Clear();
                    break;
                case "7":
                    Console.WriteLine($"(!) Version of the application: {VERSION}");
                    break;
                default:
                    Console.WriteLine("(!) Invalid command. Please try again.");
                    break;
            }

            Console.Write("(?) Would you like to continue using our application? (Y/N): ");
            if (Console.ReadLine().ToUpper() != "Y")
            {
                break;
            }
        }
    }

    private static void CreatePlayer()
    {
        Console.Write("(?) Name of the player: ");
        string? name = Console.ReadLine();
        string? gender = null;

        while (true)
        {
            Console.Write("(?) Gender of the player (male/m or female/f): ");
            gender = Console.ReadLine()?.ToLower();
            if (gender == "male" || gender == "m" || gender == "female" || gender == "f")
            {
                break;
            }
            Console.WriteLine("(!) Invalid gender. Please try again.");
        }

        Player player = new Player(name, gender);
        Console.WriteLine("(!) Successfully created player!");
    }

    private static void DeletePlayer()
    {
        Console.Write("(?) Enter player ID to delete: ");
        string? id = Console.ReadLine();

        if (int.TryParse(id, out int playerId) && PlayersManager.PLAYERS.ContainsKey(playerId))
        {
            PlayersManager.PLAYERS.Remove(playerId);
            PlayersManager.DeletePlayer(id);
            Console.WriteLine("(!) Player successfully deleted!");
        }
        else
        {
            Console.WriteLine("(!) Invalid or non-existent player ID.");
        }
    }

    private static void DisplayPlayersList()
    {
        Console.WriteLine($"(!) Registered {PlayersManager.PLAYERS.Count} players!");
        foreach (var player in PlayersManager.PLAYERS.Values)
        {
            Console.WriteLine($"{player.Id}: {player.Name} ({player.Gender})");
        }
    }

    private static void WhoseCheese()
    {
        if (PlayersManager.PLAYERS.Count == 0)
        {
            Console.WriteLine("(!) No players registered!");
            return;
        }
        Console.WriteLine($"A player with name {PlayersManager.PLAYERS.GetValueAtIndex(new Random()
            .Next(PlayersManager.PLAYERS.Count)).Name} has cheese in his pocket!");
    }
}
