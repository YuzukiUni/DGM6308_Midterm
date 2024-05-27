using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6308_BlackJack_Midterm_Ver.B
{
    // Game rule reference:https://www.instructables.com/Learn-to-play-two-people-blackjack/
    /*Other Reference before Start this project:https://blog.csdn.net/weixin_43328198/article/details/85311232
      https://blog.csdn.net/Czhenya/article/details/123198795?utm_medium=distribute.pc_relevant.none-task-blog-2~default~baidujs_baidulandingword~default-1-123198795-blog-85311232.235^v43^pc_blog_bottom_relevance_base9&spm=1001.2101.3001.4242.2&utm_relevant_index=2
     */
    class Program
    {
        static void Main(string[] args)
        {
            // Read the Input number between 2 to 4 to decide how many players play this game
            Console.Write("Enter the number of players (2-4): ");
            int numPlayers = int.Parse(Console.ReadLine());
            // If invalid, end the game
            if (numPlayers < 2 || numPlayers > 4)
            {
                Console.WriteLine("Invalid Number, Please enter again :");
                return;
            }

            // Set up the deck
            List<string> deck = new List<string>();
            Deck(deck);
            Random randomCards = new Random();
            // Intialize the Starting points and coins for players
            int[] playerPoints = new int[numPlayers];
            int[] playerCoins = new int[numPlayers];
            for (int i = 0; i < numPlayers; i++)
            {
                playerPoints[i] = 0;
                playerCoins[i] = 100;
            }
            int round = 0;

            while (true)
            {                
                // Starting each round, and type the numbers of round at the beginning
                round++;
                Console.WriteLine("ROUND " + round);
                Console.WriteLine("--------------------------------------------");
                // Set up bet for each round
                int betLimit;
                if (round < 3)
                {
                    betLimit = 10;
                }
                else if (round < 6)
                {
                    betLimit = 15;
                }
                else if (round < 10)
                {
                    betLimit = 20;
                }
                else if (round < 15)
                {
                    betLimit = 25;
                }
                else
                {
                    betLimit = 30;
                }
                // Starting a bet for players
                int[] playerBets = new int[numPlayers];
                for (int i = 0; i < numPlayers; i++)
                {
                    playerBets[i] = betLimit;
                    // If players have not enough coins to bet, do "all in " process
                    if (playerBets[i] > playerCoins[i])
                    {
                        Console.WriteLine("Player" + (i + 1) + " doesn't have enough coins. Your bet is set to " + playerCoins[i]);
                        playerBets[i] = playerCoins[i];
                    }
                    Console.WriteLine("Player" + (i + 1) + "'s bet is " + playerBets[i]);
                }
                // Set up player lists to draw cards one by one
                // Ref: https://github.com/dotnet/dotnet-console-games/tree/main/Projects/Blackjack
                int[] playerTotals = new int[numPlayers];
                for (int i = 0; i < numPlayers; i++)
                {
                    playerTotals[i] += DrawCard(deck, randomCards, "Player" + (i + 1));
                }

                Console.WriteLine("--------------------------------------------");
                // Show player's draw cards
                for (int i = 0; i < numPlayers; i++)
                {
                    Console.WriteLine("Player" + (i + 1) + "'s total is " + playerTotals[i]);
                }
                Console.WriteLine("--------------------------------------------");
                // Game Play
                bool gameContinue = true;
                while (gameContinue)
                {
                    for (int i = 0; i < numPlayers; i++)
                    {
                        while (playerTotals[i] < 21)
                        {
                            gameContinue = true;
                            Console.WriteLine("--------------------------------------------");
                            Console.WriteLine("Press 'D' to draw another card, 'B' to add to your bet, 'H' to hold: ");
                            var key = Console.ReadKey(true).Key;
                            Console.WriteLine("--------------------------------------------");
                            // Let player press each button to hand/draw/add bet
                            if (key != ConsoleKey.D && key != ConsoleKey.B && key != ConsoleKey.H && key != ConsoleKey.C)
                            {
                                Console.WriteLine("Invalid input. Please try again.");
                                Console.WriteLine("--------------------------------------------");
                                continue;
                            }

                            if (key == ConsoleKey.B)
                            {
                                Console.Write("Enter your additional bet (limit " + betLimit + "): ");
                                int additionalBet = int.Parse(Console.ReadLine());
                                // Over Bet for add bet
                                if (additionalBet > betLimit || additionalBet > playerCoins[i] - playerBets[i])
                                {
                                    //Determine the maximum bet a player can place
                                    Console.WriteLine("Invalid bet. Your additional bet is set to " + Math.Min(betLimit, playerCoins[i] - playerBets[i]));
                                    Console.WriteLine("--------------------------------------------");
                                    additionalBet = Math.Min(betLimit, playerCoins[i] - playerBets[i]);
                                }
                                playerBets[i] += additionalBet;
                            }
                            // Player draws card
                            if (key == ConsoleKey.D)
                            {
                                playerTotals[i] += DrawCard(deck, randomCards, "Player" + (i + 1));
                                Console.WriteLine("--------------------------------------------");
                                Console.WriteLine("Player" + (i + 1) + "'s total is now " + playerTotals[i]);
                                Console.WriteLine("--------------------------------------------");
                                // Busted
                                if (playerTotals[i] > 21)
                                {
                                    Console.WriteLine("Player" + (i + 1) + " busted.");
                                    Console.WriteLine("--------------------------------------------");
                                    gameContinue = false;
                                    break;
                                }
                                if (!gameContinue)
                                    break;
                            }
                            // Hold the card, automatically hold if number=21
                            if (key == ConsoleKey.H)
                            {
                                Console.WriteLine("--------------------------------------------");
                                Console.WriteLine("Player " + (i + 1) + " hold the hand");
                                Console.WriteLine("--------------------------------------------");
                                gameContinue = false;
                                break;
                            }
                        }
                    }
                }
                // Adding a score
                bool[] playerBusted = new bool[numPlayers];
                for (int i = 0; i < numPlayers; i++)
                {
                    // Busted if over 21
                    if (playerTotals[i] > 21)
                    {
                        Console.WriteLine("Player" + (i + 1) + " busted.");
                        playerBusted[i] = true;
                    }
                }

                int maxPoints = 0;
                // if not busted, find the highest point for player
                for (int i = 0; i < numPlayers; i++)
                {   
                    if (!playerBusted[i]) maxPoints = Math.Max(maxPoints, playerTotals[i]);
                }
                // Find a winners based on Max Points
                List<string> winners = new List<string>();
                for (int i = 0; i < numPlayers; i++)
                {
                    if (playerTotals[i] == maxPoints)
                    {
                        winners.Add("Player" + (i + 1));
                        playerPoints[i] += 10;
                    }
                }


                // Check if there are any winners
                // If exist winner
                if (winners.Count > 0)
                {
                    int totalBet = playerBets.Sum();

                    for (int i = 0; i < numPlayers; i++)
                    {
                       
                        // Starting bet for each players
                        playerCoins[i] -= playerBets[i];
                    }
                    // If only exists 1 winner
                    if (winners.Count == 1)
                    {
                        string winner = winners[0];
                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine($"{winner} wins this round!");
                        Console.WriteLine("--------------------------------------------");
                        for (int i = 0; i < numPlayers; i++)
                        {
                            if (winner == "Player" + (i + 1)) playerCoins[i] += totalBet;
                        }
                    }
                    else // have 2 or more winners
                    {
                        int share = totalBet / winners.Count;
                        foreach (string winner in winners)
                        {
                            Console.WriteLine("--------------------------------------------");
                            Console.WriteLine($"{winner} wins this round!");
                            Console.WriteLine("--------------------------------------------");
                            for (int i = 0; i < numPlayers; i++)
                            {
                                if (winner == "Player" + (i + 1)) playerCoins[i] += share;
                            }
                        }
                    }
                }
                else // No Winner
                {
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine("All players busted. No one wins this round.");
                    Console.WriteLine("--------------------------------------------");
                }
                for (int i = 0; i < numPlayers; i++)
                {
                    // Reference:https://blog.csdn.net/weixin_43328198/article/details/85311232
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine($"Player{i + 1} Scores：{playerPoints[i]} Coins：{playerCoins[i]}");
                    Console.WriteLine("--------------------------------------------");
                }

                // Win condition
                int activePlayers = numPlayers;
                for (int i = 0; i < numPlayers; i++)
                {
                    // Eliminated players with no bets
                    if (playerCoins[i] <= 0) 
                        activePlayers--;
                }

                // When there is only one active player left, the score of all players is recorded
                Dictionary<string, int> players = new Dictionary<string, int>();
                for (int i = 0; i < numPlayers; i++)
                {
                    // In order to display the score table at the end of the game
                    players.Add("Player" + (i + 1), playerPoints[i]);
                }
                // Reference:https://blog.csdn.net/qq_39108767/article/details/86648448
                var sortedPlayers = from player in players
                                    orderby player.Value descending
                                    select player;
                int rank = 1;
                foreach (var player in sortedPlayers)
                {
                    //  Tracking 1st Place for each round
                    if (rank == 1)
                    {
                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine(player.Key + " leads the game!");
                        Console.WriteLine("--------------------------------------------");
                    }
                    else
                    {
                        // Rank 2nd, 3rd, 4th for players for each round 
                        Console.WriteLine("Rank " + rank + ": " + player.Key + " with " + player.Value + " points");
                    }
                    rank++;
                }

                if (activePlayers == 1)
                {
                    break;
                }
                else
                { // Player who gets 50 points also wins this game
                    for (int i = 0; i < numPlayers; i++)
                    {
                        if (playerPoints[i] == 50)
                        {
                            Console.WriteLine("Player" + (i + 1) + " wins the game with " + playerPoints[i] + " points!");
                            return; // End the game immediately
                        }
                    }
                    // Before starting a new round, Check if the player has enough coins to bet
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // If not bets, the players out of the game, and the game should be end immediately.
                        if (playerCoins[i] <= 0)
                        {
                            Console.WriteLine("--------------------------------------------");
                            Console.WriteLine($"Player{i + 1} doesn't have enough coins. Player{i + 1} out of the game, and get 3rd/4th place !");
                            Console.WriteLine("--------------------------------------------");
                            // Remove the player from the game
                            numPlayers--;

                        }
                    }
                }
            }
        }

        // Ref:https://github.com/dotnet/dotnet-console-games/tree/main/Projects/Blackjack
        // Reference:https://stackoverflow.com/questions/13038026/randomly-drawing-5-cards-from-a-deck-in-java
        static int DrawCard(List<string> deck, Random randomCards, string player)
        {
            // If the deck is empty, reshuffling deck
            if (deck.Count == 0)
            {
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("Deck is empty! Reshuffling the deck ");
                Console.WriteLine("--------------------------------------------");
                Deck(deck);
            }
            // Find the card in deck
            int cardIndex = randomCards.Next(deck.Count);
            string card = deck[cardIndex];
            // Remove card from cardlists after draw
            deck.RemoveAt(cardIndex);

            // Split the card into suit and rank
            string[] parts = card.Split(' ');
            string suit = parts[0];
            string rank = parts[1];
            // Show the card that player draws
            Console.WriteLine(player + " drew a " + suit + " " + rank);

            // Ace should be a value of 11
            if (rank == "A")
            {
                return 11;
            }
            else if (rank == "J" || rank == "Q" || rank == "K")
            {
                return 10;
            }
            else
            {
                return int.Parse(rank);
            }
        }
        // Ref:https://github.com/dotnet/dotnet-console-games/tree/main/Projects/Blackjack
        static void Deck(List<string> deck)
        {
            List<string> suits = new List<string> { "spades", "hearts", "clubs", "diamonds" };
            List<string> ranks = new List<string> { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

            deck.Clear();
            foreach (string suit in suits)
            {
                foreach (string rank in ranks)
                {
                    deck.Add(suit + " " + rank);
                }
            }
        }
    }
}
