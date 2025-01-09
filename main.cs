using System;
using System.Collections.Generic;
using System.Linq;

namespace SplendorGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }

    public class Gem
    {
        public string Name { get; }
        public Gem(string name)
        {
            Name = name;
            Count = count;
        }
    }

    public class Card
    {
        public string Name { get; }
        public Dictionary<string, int> Cost { get; }
        public int Points { get; }

        public Card(string name, Dictionary<string, int> cost, int points)
        {
            Name = name;
            Cost = cost;
            Points = points;
        }

        public bool CanBePurchased(Dictionary<string, int> playerGems)
        {
            return Cost.All(c => playerGems.ContainsKey(c.Key) && playerGems[c.Key] >= c.Value);
        }
    }

    public class Player
    {
        public string Name { get; }
        public Dictionary<string, int> Gems { get; }
        public int Points { get; private set; }

        public Player(string name)
        {
            Name = name;
            Gems = new Dictionary<string, int>();
            Points = 0;
        }

        public void AddGems(string gemType, int amount)
        {
            if (!Gems.ContainsKey(gemType))
                Gems[gemType] = 0;
            Gems[gemType] += amount;
        }

        public void PurchaseCard(Card card)
        {
            foreach (var cost in card.Cost)
            {
                Gems[cost.Key] -= cost.Value;
            }
            Points += card.Points;
        }

        public bool HasEnoughGems(Dictionary<string, int> cost)
        {
            return cost.All(c => Gems.ContainsKey(c.Key) && Gems[c.Key] >= c.Value);
        }
    }

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

                Console.WriteLine("Choose an action:");
                Console.WriteLine("1. Collect Gems");
                Console.WriteLine("2. Purchase Card");
                Console.WriteLine("3. View Your Gems");

                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    CollectGems(currentPlayer);
                }
                else if (choice == "2")
                {
                    PurchaseCard(currentPlayer);
                }
                else if (choice == "3")
                {
                    DisplayPlayerGems(currentPlayer);
                    continue;
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

            int choice = int.Parse(Console.ReadLine()) - 1;
            if (choice >= 0 && choice < gemTypes.Count)
            {
                string chosenGem = gemTypes[choice];
                player.AddGems(chosenGem, 1);
                Console.WriteLine($"{player.Name} collected a {chosenGem}.");
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        private void PurchaseCard(Player player)
        {
            Console.WriteLine("Available cards:");
            for (int i = 0; i < availableCards.Count; i++)
            {
                Card card = availableCards[i];
                Console.WriteLine($"{i + 1}. {card.Name} (Cost: {string.Join(", ", card.Cost.Select(c => $"{c.Key}: {c.Value}"))}, Points: {card.Points})");
            }

            Console.WriteLine("Choose a card to purchase:");
            int choice = int.Parse(Console.ReadLine()) - 1;

            if (choice >= 0 && choice < availableCards.Count)
            {
                Card selectedCard = availableCards[choice];
                if (player.HasEnoughGems(selectedCard.Cost))
                {
                    player.PurchaseCard(selectedCard);
                    availableCards.RemoveAt(choice);
                    Console.WriteLine($"{player.Name} purchased {selectedCard.Name}.");
                }
                else
                {
                    Console.WriteLine("Not enough gems to purchase this card.");
                }
            }
            else
            {
                Console.WriteLine("Invalid choice.");
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
}
