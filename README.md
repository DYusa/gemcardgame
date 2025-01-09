Overview
This game is a simplified version of Splendor, a resource management and strategy game. Players collect gems, purchase cards using those gems, and compete to reach a target score first.

Key Features:
Gem Collection: Players can collect gems on their turn.
Card Purchase: Players spend gems to purchase cards.
Point System: Some cards grant points, and the first player to reach the target score wins.
Classes
1. Gem
Represents the different types of gems available in the game.

Properties:
string Name: The name of the gem (e.g., "Ruby", "Emerald").
Constructor:
Gem(string name): Initializes a new Gem with the given name.
2. Card
Represents a card that players can purchase.

Properties:
string Name: The name of the card.
Dictionary<string, int> Cost: The gem cost to purchase the card. Keys are gem types, and values are the number of gems required.
int Points: The points the card provides when purchased.
Methods:
bool CanBePurchased(Dictionary<string, int> playerGems): Checks if a player has enough gems to purchase the card.
Constructor:
Card(string name, Dictionary<string, int> cost, int points): Initializes a new card with a name, cost, and points.
3. Player
Represents a player in the game.

Properties:
string Name: The player's name.
Dictionary<string, int> Gems: The player's collection of gems.
int Points: The player's current score.
Methods:
void AddGems(string gemType, int amount): Adds gems of the specified type to the player’s collection.
void PurchaseCard(Card card): Deducts the cost of the card from the player's gems and adds the card's points to their score.
bool HasEnoughGems(Dictionary<string, int> cost): Checks if the player has enough gems to purchase a card.
Constructor:
Player(string name): Initializes a new player with the given name.
4. Game
Handles the main game logic and flow.

Properties:
List<Player> players: The list of players in the game.
List<Card> availableCards: The list of available cards that players can purchase.
List<string> gemTypes: The types of gems available in the game.
int targetPoints: The score required to win the game.
Methods:
void Start(): Begins the game and manages turns until a player wins.
void InitializePlayers(): Prompts the user to enter the number of players and their names.
void InitializeCards(): Creates and adds a set of cards to the game.
void CollectGems(Player player): Allows a player to collect a gem of their choice.
void PurchaseCard(Player player): Allows a player to purchase a card if they have enough gems.
void DisplayGameState(): Displays the current state of the game (players' scores and available cards).
void DisplayPlayerGems(Player player): Displays a player's collection of gems.
Constructor:
Game(): Initializes the game by setting up players, cards, and gem types.
How to Play
Start the Game:

Run the program.
Enter the number of players and their names.
Gameplay:

Players take turns performing one of the following actions:
Collect Gems:
Choose a type of gem to add to your collection.
Purchase Card:
Choose a card to buy if you have enough gems to cover its cost.
View Your Gems:
Check the gems you currently have.
Win Condition:

The game ends when a player reaches or exceeds the target score (default: 15 points).
The first player to reach the target score wins.
Example Walkthrough
Turn 1:
Player 1 chooses to collect a "Ruby."
Player 2 chooses to collect a "Diamond."
Turn 2:
Player 1 checks the available cards and purchases a card costing 3 Rubies.
Player 2 collects another "Diamond."
Gameplay Continues:
Players alternate turns, collecting gems or purchasing cards.
End Game:
When a player's score reaches the target points, the game announces the winner and ends.
