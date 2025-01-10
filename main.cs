public class Game
{
    private List<Player> players;
    private List<Card> availableCards;
    private List<string> gemTypes;
    private int targetPoints;

    public Game()
    {
        players = new List<Player>();
        availableCards = new List<Card>();
        gemTypes = new List<string> { "Ruby", "Emerald", "Diamond", "Sapphire", "Onyx" };
        targetPoints = 15;
    }

    public void Start()
    {
        InitializePlayers();
        InitializeCards();

        int currentPlayerIndex = 0;

        while (true)
        {
            Console.Clear();
            DisplayGameState();

            Player currentPlayer = players[currentPlayerIndex];
            Console.WriteLine($"{currentPlayer.Name}'s Turn!");

            bool turnComplete = false;
            while (!turnComplete)
            {
                Console.WriteLine("Choose an action:");
                Console.WriteLine("1. Collect Gems");
                Console.WriteLine("2. Purchase Card");
                Console.WriteLine("3. View Your Gems");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        CollectGems(currentPlayer);
                        turnComplete = true; // Valid action completed
                        break;
                    case "2":
                        if (PurchaseCard(currentPlayer))
                            turnComplete = true; // Valid action completed
                        break;
                    case "3":
                        DisplayPlayerGems(currentPlayer);
                        break; // Allow re-prompting for valid action
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

            if (currentPlayer.Points >= targetPoints)
            {
                Console.WriteLine($"{currentPlayer.Name} wins with {currentPlayer.Points} points!");
                break;
            }

            currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        }
    }

    private void InitializePlayers()
    {
        Console.WriteLine("Enter the number of players:");
        int playerCount = int.Parse(Console.ReadLine());

        for (int i = 0; i < playerCount; i++)
        {
            Console.WriteLine($"Enter name for Player {i + 1}:");
            string playerName = Console.ReadLine();
            players.Add(new Player(playerName));
        }
    }

    private void InitializeCards()
    {
        availableCards.Add(new Card("Card 1", new Dictionary<string, int> { { "Ruby", 3 }, { "Emerald", 2 } }, 1));
        availableCards.Add(new Card("Card 2", new Dictionary<string, int> { { "Diamond", 4 } }, 2));
        availableCards.Add(new Card("Card 3", new Dictionary<string, int> { { "Sapphire", 2 }, { "Onyx", 3 } }, 1));
        // Add more cards as needed
    }

    private void CollectGems(Player player)
    {
        Console.WriteLine("Choose a gem type to collect:");
        for (int i = 0; i < gemTypes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {gemTypes[i]}");
        }

        if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= gemTypes.Count)
        {
            string chosenGem = gemTypes[choice - 1];
            player.AddGems(chosenGem, 1);
            Console.WriteLine($"{player.Name} collected a {chosenGem}.");
        }
        else
        {
            Console.WriteLine("Invalid choice. No gems collected.");
        }
    }

    private bool PurchaseCard(Player player)
    {
        Console.WriteLine("Available cards:");
        for (int i = 0; i < availableCards.Count; i++)
        {
            Card card = availableCards[i];
            Console.WriteLine($"{i + 1}. {card.Name} (Cost: {string.Join(", ", card.Cost.Select(c => $"{c.Key}: {c.Value}"))}, Points: {card.Points})");
        }

        Console.WriteLine("Choose a card to purchase:");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= availableCards.Count)
        {
            Card selectedCard = availableCards[choice - 1];
            if (player.HasEnoughGems(selectedCard.Cost))
            {
                player.PurchaseCard(selectedCard);
                availableCards.RemoveAt(choice - 1);
                Console.WriteLine($"{player.Name} purchased {selectedCard.Name}.");
                return true;
            }
            else
            {
                Console.WriteLine("Not enough gems to purchase this card.");
                return false;
            }
        }
        else
        {
            Console.WriteLine("Invalid choice. No card purchased.");
            return false;
        }
    }

    private void DisplayGameState()
    {
        Console.WriteLine("Game State:");
        foreach (var player in players)
        {
            Console.WriteLine($"{player.Name}: {player.Points} Points");
        }

        Console.WriteLine("\nAvailable Cards:");
        foreach (var card in availableCards)
        {
            Console.WriteLine($"{card.Name} (Cost: {string.Join(", ", card.Cost.Select(c => $"{c.Key}: {c.Value}"))}, Points: {card.Points})");
        }
        Console.WriteLine();
    }

    private void DisplayPlayerGems(Player player)
    {
        Console.WriteLine($"{player.Name}'s Gems:");
        foreach (var gem in player.Gems)
        {
            Console.WriteLine($"{gem.Key}: {gem.Value}");
        }
    }
}
