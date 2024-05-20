using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6308_Midterm_BlackJack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> deck = new List<string>();
            Deck(deck);
            Random randomCards = new Random();
            int playerAPoints = 0, playerBPoints = 0, playerCPoints = 0, playerDPoints = 0;
            int playerACoins = 100, playerBCoins = 100, playerCCoins = 100, playerDCoins = 100;
            int round = 0;

            while (true)
            {
                round++;
                Console.WriteLine("ROUND " + round);
                Console.WriteLine("--------------------------------------------");
                int betLimit;
                if (round < 3)
                {
                    betLimit = 5;
                }
                else if (round < 6)
                {
                    betLimit = 10;
                }
                else if (round < 10)
                {
                    betLimit = 15;
                }
                else if (round < 15)
                {
                    betLimit = 20;
                }
                else
                {
                    betLimit = 30;
                }

                int playerABet = betLimit;
                int playerBBet = betLimit;
                int playerCBet = betLimit;
                int playerDBet = betLimit;


                if (playerABet > playerACoins)
                {
                    Console.WriteLine("PlayerA doesn't have enough coins. Your bet is set to " + playerACoins);
                    playerABet = playerACoins;
                }
                if (playerBBet > playerBCoins)
                {
                    Console.WriteLine("PlayerB doesn't have enough coins. Your bet is set to " + playerBCoins);
                    playerBBet = playerBCoins;
                }
                if (playerCBet > playerCCoins)
                {
                    Console.WriteLine("PlayerC doesn't have enough coins. Your bet is set to " + playerCCoins);
                    playerCBet = playerCCoins;
                }
                if (playerDBet > playerDCoins)
                {
                    Console.WriteLine("PlayerD doesn't have enough coins. Your bet is set to " + playerDCoins);
                    playerDBet = playerDCoins;
                }

                Console.WriteLine("PlayerA's bet is " + playerABet);
                Console.WriteLine("PlayerB's bet is " + playerBBet);
                Console.WriteLine("PlayerC's bet is " + playerCBet);
                Console.WriteLine("PlayerD's bet is " + playerDBet);


                int playerATotal = 0, playerBTotal = 0, playerCTotal = 0, playerDTotal = 0;

                playerATotal += DrawCard(deck, randomCards, "PlayerA");
                playerBTotal += DrawCard(deck, randomCards, "PlayerB");
                playerCTotal += DrawCard(deck, randomCards, "PlayerC");
                playerDTotal += DrawCard(deck, randomCards, "PlayerD");


                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("PlayerA's total is " + playerATotal);
                Console.WriteLine("PlayerB's total is " + playerBTotal);
                Console.WriteLine("PlayerC's total is " + playerCTotal);
                Console.WriteLine("PlayerD's total is " + playerDTotal);
                Console.WriteLine("--------------------------------------------");


                bool gameContinue = true;
                while (gameContinue)
                {
                    // PlayerA's Turn
                    while (playerATotal < 21)
                    {
                        gameContinue = true;
                        // Let player press each button to hand/draw/add bet
                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine("Press 'D' to draw another card, 'B' to add to your bet, 'H' to hold: "); var key = Console.ReadKey(true).Key;
                        Console.WriteLine("--------------------------------------------");
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

                            if (additionalBet > betLimit || additionalBet > playerACoins - playerABet)
                            {
                                Console.WriteLine("Invalid bet. Your additional bet is set to " + Math.Min(betLimit, playerACoins - playerABet));
                                Console.WriteLine("--------------------------------------------");
                                additionalBet = Math.Min(betLimit, playerACoins - playerABet);
                            }
                            playerABet += additionalBet;
                        }
                        // Place D to draw cards
                        if (key == ConsoleKey.D)
                        {
                            playerATotal += DrawCard(deck, randomCards, "PlayerA");
                            Console.WriteLine("PlayerA's total is now " + playerATotal);
                            Console.WriteLine("--------------------------------------------");
                            // End the round if player busted 
                            if (playerATotal > 21)
                            {
                                Console.WriteLine("PlayerA busted.");
                                Console.WriteLine("--------------------------------------------");
                                gameContinue = false;
                                break;
                            }
                           if(!gameContinue)
                                break;
                        }

                        // Place H to hold the card
                        if (key == ConsoleKey.H)
                        {
                            Console.WriteLine("--------------------------------------------");
                            Console.WriteLine("Player A hold the hand");
                            Console.WriteLine("--------------------------------------------");
                            gameContinue = false;
                            break; // PlayerA chooses to hold and ends their turn
                        }
                        
                    }

                    // PlayerB's turn
                    while (playerBTotal < 21)
                    {
                        gameContinue = true;
                        // Let player press each button to hand/draw/add bet
                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine("Press 'D' to draw another card, 'B' to add to your bet, 'H' to hold: "); var key = Console.ReadKey(true).Key;
                        Console.WriteLine("--------------------------------------------");

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

                            if (additionalBet > betLimit || additionalBet > playerBCoins - playerBBet)
                            {
                                Console.WriteLine("Invalid bet. Your additional bet is set to " + Math.Min(betLimit, playerBCoins - playerBBet));
                                Console.WriteLine("--------------------------------------------");
                                additionalBet = Math.Min(betLimit, playerBCoins - playerBBet);
                            }
                            playerBBet += additionalBet;
                        }
                        // Place D to draw cards
                        if (key == ConsoleKey.D)
                        {
                            playerBTotal += DrawCard(deck, randomCards, "PlayerB");
                            Console.WriteLine("PlayerB's total is now " + playerBTotal);
                            Console.WriteLine("--------------------------------------------");
                            // End the round if player busted 
                            if (playerBTotal > 21)
                            {
                                Console.WriteLine("PlayerB busted.");
                                Console.WriteLine("--------------------------------------------");
                                gameContinue =false;
                                break;
                            }
                            if (!gameContinue)
                                break;
                        }

                        // Place H to hold the card
                        if (key == ConsoleKey.H)
                        {
                            Console.WriteLine("--------------------------------------------");
                            Console.WriteLine("Player B hold the hand");
                            Console.WriteLine("--------------------------------------------");
                            gameContinue = false;
                            break; // PlayerB chooses to hold and ends their turn
                        }
                       
                    }

                    // PlayerC's turn
                    while (playerCTotal < 21)
                    {
                        gameContinue = true;
                        // Let player press each button to hand/draw/add bet
                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine("Press 'D' to draw another card, 'B' to add to your bet, 'H' to hold: "); var key = Console.ReadKey(true).Key;
                        Console.WriteLine("--------------------------------------------");
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

                            if (additionalBet > betLimit || additionalBet > playerCCoins - playerCBet)
                            {
                                Console.WriteLine("Invalid bet. Your additional bet is set to " + Math.Min(betLimit, playerCCoins - playerCBet));
                                Console.WriteLine("--------------------------------------------");
                                additionalBet = Math.Min(betLimit, playerCCoins - playerCBet);
                            }
                            playerCBet += additionalBet;
                        }
                        // Place D to draw cards
                        if (key == ConsoleKey.D)
                        {
                            playerCTotal += DrawCard(deck, randomCards, "PlayerC");
                            Console.WriteLine("PlayerC's total is now " + playerCTotal);
                            Console.WriteLine("--------------------------------------------");
                            // End the round if player busted 
                            if (playerCTotal > 21)
                            {
                                Console.WriteLine("PlayerC busted.");
                                Console.WriteLine("--------------------------------------------");
                                gameContinue =false;
                                break;
                            }
                            if (!gameContinue)
                                break;
                        }

                        // Place H to hold the card
                        if (key == ConsoleKey.H)
                        {
                            Console.WriteLine("--------------------------------------------");
                            Console.WriteLine("Player C hold the hand");
                            Console.WriteLine("--------------------------------------------");
                            gameContinue = false;
                            break; // PlayerC chooses to hold and ends their turn
                        }

                       
                    }

                    // PlayerD's turn
                    while (playerDTotal < 21)
                    {
                        gameContinue = true;
                        // Let player press each button to hand/draw/add bet
                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine("Press 'D' to draw another card, 'B' to add to your bet, 'H' to hold: "); var key = Console.ReadKey(true).Key;
                        Console.WriteLine("--------------------------------------------");
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

                            if (additionalBet > betLimit || additionalBet > playerDCoins - playerDBet)
                            {
                                Console.WriteLine("Invalid bet. Your additional bet is set to " + Math.Min(betLimit, playerDCoins - playerDBet));
                                Console.WriteLine("--------------------------------------------");
                                additionalBet = Math.Min(betLimit, playerDCoins - playerDBet);
                            }
                            playerDBet += additionalBet;
                        }
                        // Place D to draw cards
                        if (key == ConsoleKey.D)
                        {
                            playerDTotal += DrawCard(deck, randomCards, "PlayerD");
                            Console.WriteLine("PlayerD's total is now " + playerDTotal);
                            Console.WriteLine("--------------------------------------------");
                            // End the round if player busted 
                            if (playerDTotal > 21)
                            {
                                Console.WriteLine("PlayerD busted.");
                                Console.WriteLine("--------------------------------------------");
                                gameContinue = false;
                                break;
                            }
                            if (!gameContinue)
                                break;
                        }

                        // Place H to hold the card
                        if (key == ConsoleKey.H)
                        {
                            gameContinue = false;
                            break; // PlayerD chooses to hold and ends their turn
                        }
                       
                    }
                }

                // Add score and bet coins after each round
                bool playerABusted = false;
                bool playerBBusted = false;
                bool playerCBusted = false;
                bool playerDBusted = false;
                if (playerATotal > 21)
                {
                    Console.WriteLine("PlayerA busted.");
                    Console.WriteLine("--------------------------------------------");
                    playerABusted = true;
                }

                if (playerBTotal > 21)
                {
                    Console.WriteLine("PlayerB busted.");
                    Console.WriteLine("--------------------------------------------");
                    playerBBusted = true;
                }

                if (playerCTotal > 21)
                {
                    Console.WriteLine("PlayerC busted.");
                    Console.WriteLine("--------------------------------------------");
                    playerCBusted = true;
                }

                if (playerDTotal > 21)
                {
                    Console.WriteLine("PlayerD busted.");
                    Console.WriteLine("--------------------------------------------");
                    playerDBusted = true;
                }


                int maxPoints = 0;
                    if (!playerABusted) maxPoints = Math.Max(maxPoints, playerATotal);
                    if (!playerBBusted) maxPoints = Math.Max(maxPoints, playerBTotal);
                    if (!playerCBusted) maxPoints = Math.Max(maxPoints, playerCTotal);
                    if (!playerDBusted) maxPoints = Math.Max(maxPoints, playerDTotal); 
                    List<string> winners = new List<string>();
                    if (playerATotal == maxPoints)
                    {
                        winners.Add("PlayerA");
                        playerAPoints += 10;
                    }
                    if (playerBTotal == maxPoints)
                    {
                        winners.Add("PlayerB");
                        playerBPoints += 10;
                    }
                    if (playerCTotal == maxPoints)
                    {
                        winners.Add("PlayerC");
                        playerCPoints += 10;
                    }
                    if (playerDTotal == maxPoints)
                    {
                        winners.Add("PlayerD");
                        playerDPoints += 10;
                    }

                    int totalBet = playerABet + playerBBet + playerCBet + playerDBet;

                    playerACoins -= playerABet;
                    playerBCoins -= playerBBet;
                    playerCCoins -= playerCBet;
                    playerDCoins -= playerDBet;

                    if (winners.Count == 1)
                    {
                        string winner = winners[0];
                        Console.WriteLine($"{winner} wins this round!");
                        Console.WriteLine("--------------------------------------------");
                        if (winner == "PlayerA") playerACoins += totalBet;
                        if (winner == "PlayerB") playerBCoins += totalBet;
                        if (winner == "PlayerC") playerCCoins += totalBet;
                        if (winner == "PlayerD") playerDCoins += totalBet;
                    }
                    else
                    {
                        int share = totalBet / winners.Count;
                        foreach (string winner in winners)
                        {
                            Console.WriteLine($"{winner} wins this round!");
                            Console.WriteLine("--------------------------------------------");
                            if (winner == "PlayerA") playerACoins += share;
                            if (winner == "PlayerB") playerBCoins += share;
                            if (winner == "PlayerC") playerCCoins += share;
                            if (winner == "PlayerD") playerDCoins += share;
                        }
                    }

                    Console.WriteLine($"Score: PlayerA - {playerAPoints}, PlayerB - {playerBPoints}, PlayerC - {playerCPoints}, PlayerD - {playerDPoints}");
                    Console.WriteLine($"Coins: PlayerA - {playerACoins}, PlayerB - {playerBCoins}, PlayerC - {playerCCoins}, PlayerD - {playerDCoins}");
                    Console.WriteLine("--------------------------------------------");
                


                // Win condition
                int activePlayers = 4;
                if (playerACoins <= 0) activePlayers--;
                if (playerBCoins <= 0) activePlayers--;
                if (playerCCoins <= 0) activePlayers--;
                if (playerDCoins <= 0) activePlayers--;

                if (activePlayers == 1 || playerAPoints >= 50 || playerBPoints >= 50 || playerCPoints >= 50 || playerDPoints >= 50)
                {
                    Dictionary<string, int> players = new Dictionary<string, int>()
                    {
                     {"PlayerA", playerAPoints},
                     {"PlayerB", playerBPoints},
                     {"PlayerC", playerCPoints},
                     {"PlayerD", playerDPoints}
                    };

                    var sortedPlayers = from player in players
                                        orderby player.Value descending
                                        select player;

                    int rank = 1;
                    foreach (var player in sortedPlayers)
                    {
                        if (rank == 1)
                        {
                            Console.WriteLine(player.Key + " wins the game!");
                        }
                        else
                        {
                            Console.WriteLine("Rank " + rank + ": " + player.Key + " with " + player.Value + " points");
                        }
                        rank++;
                    }
                    break;
                
            }
            }
        }

        // Reference:https://stackoverflow.com/questions/13038026/randomly-drawing-5-cards-from-a-deck-in-java
        // Drawing card from a deck
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

            int cardIndex = randomCards.Next(deck.Count);
            string card = deck[cardIndex];
            deck.RemoveAt(cardIndex);

            // Split the card into suit and rank
            string[] parts = card.Split(' ');
            string suit = parts[0];
            string rank = parts[1];

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

        // Starting deck of 52 cards, if cards empty, reuse this deck by reshuffling
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
