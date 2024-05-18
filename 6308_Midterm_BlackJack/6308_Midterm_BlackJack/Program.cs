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
            // Initialize the deck of 52 cards
            List<string> suits = new List<string> { "spades", "hearts", "clubs", "diamonds" };
            List<string> ranks = new List<string> { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
            List<string> deck = new List<string>();
            foreach (string suit in suits)
            {
                foreach (string rank in ranks)
                {
                    deck.Add(suit + " " + rank);
                }
            }

            Random randomCards = new Random();
            int playerAPoints = 0, playerBPoints = 0, playerCPoints = 0,  playerDPoints=0;
            int playerACoins = 100, playerBCoins = 100, playerCCoins = 100, playerDCoins =100 ;
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


                int playerATotal = 0, playerBTotal = 0, playerCTotal = 0, playerDTotal=0;

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

               
                while (true)
                {
                    // PlayerA's Turn
                    while (playerATotal < 50)
                    {
                        if (playerATotal > 50)
                        {
                            Console.WriteLine("PlayerA's total is now " + playerATotal);
                            break;
                        }
                        // Let player press each button to hand/draw/add bet
                        Console.WriteLine("Press 'D' to draw another card, 'B' to add to your bet, 'H' to hold, or 'C' to check hands: "); var key = Console.ReadKey(true).Key;

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
                            if (playerATotal > 50)
                            {
                                Console.WriteLine("PlayerA busted.");
                                Console.WriteLine("--------------------------------------------");
                                playerACoins -= playerABet;
                                break;
                            }
                            break;
                        }

                        // Place H to hold the card
                        if (key == ConsoleKey.H)
                        {
                            break; // PlayerA chooses to hold and ends their turn
                        }
                        if (key == ConsoleKey.C)
                        {
                            Console.WriteLine("PlayerA requests to check hands. Do all players agree? Press 'Y' for yes, 'N' for no: ");
                            var checkKey = Console.ReadKey(true).Key;

                            if (checkKey == ConsoleKey.Y)
                            {
                                // Check hands and end the game
                                break;
                            }
                            else if (checkKey == ConsoleKey.N)
                            {
                                // Continue the game
                                Console.WriteLine("Not all players agree. The game continues.");
                                Console.WriteLine("--------------------------------------------");
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please try again.");
                                Console.WriteLine("--------------------------------------------");
                                continue;
                            }
                        }
                    }

                    // PlayerB's turn
                    while (playerBTotal < 50)
                    {
                        if (playerBTotal > 50)
                        {
                            Console.WriteLine("PlayerB's total is now " + playerBTotal);
                            break;
                        }
                        // Let player press each button to hand/draw/add bet
                        Console.WriteLine("Press 'D' to draw another card, 'B' to add to your bet, 'H' to hold, or 'C' to check hands: "); var key = Console.ReadKey(true).Key;

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
                            if (playerBTotal > 50)
                            {
                                Console.WriteLine("PlayerB busted.");
                                Console.WriteLine("--------------------------------------------");
                                playerBCoins -= playerBBet;
                                break;
                            }
                            break;
                                }

                        // Place H to hold the card
                        if (key == ConsoleKey.H)
                        {
                            break; // PlayerB chooses to hold and ends their turn
                        }
                        if (key == ConsoleKey.C)
                        {
                            Console.WriteLine("PlayerB requests to check hands. Do all players agree? Press 'Y' for yes, 'N' for no: ");
                            var checkKey = Console.ReadKey(true).Key;

                            if (checkKey == ConsoleKey.Y)
                            {
                                // Check hands and end the game
                                break;
                            }
                            else if (checkKey == ConsoleKey.N)
                            {
                                // Continue the game
                                Console.WriteLine("Not all players agree. The game continues.");
                                Console.WriteLine("--------------------------------------------");
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please try again.");
                                Console.WriteLine("--------------------------------------------");
                                continue;
                            }
                        }
                    }

                    // PlayerC's turn
                    while (playerCTotal < 50)
                    {
                        if (playerCTotal > 50)
                        {
                            Console.WriteLine("PlayerC's total is now " + playerCTotal);
                            break;
                        }
                        // Let player press each button to hand/draw/add bet
                        Console.WriteLine("Press 'D' to draw another card, 'B' to add to your bet, 'H' to hold, or 'C' to check hands: "); var key = Console.ReadKey(true).Key;

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
                            if (playerCTotal > 50)
                            {
                                Console.WriteLine("PlayerC busted.");
                                Console.WriteLine("--------------------------------------------");
                                playerCCoins -= playerCBet;
                                break;
                            }
                            break;
                        }

                        // Place H to hold the card
                        if (key == ConsoleKey.H)
                        {
                            break; // PlayerC chooses to hold and ends their turn
                        }

                        if (key == ConsoleKey.C)
                        {
                            Console.WriteLine("PlayerC requests to check hands. Do all players agree? Press 'Y' for yes, 'N' for no: ");
                            var checkKey = Console.ReadKey(true).Key;

                            if (checkKey == ConsoleKey.Y)
                            {
                                // Check hands and end the game
                                break;
                            }
                            else if (checkKey == ConsoleKey.N)
                            {
                                // Continue the game
                                Console.WriteLine("Not all players agree. The game continues.");
                                Console.WriteLine("--------------------------------------------");
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please try again.");
                                Console.WriteLine("--------------------------------------------");
                                continue;
                            }
                        }
                    }

                    // PlayerD's turn
                    while (playerDTotal < 50)
                    {
                        if (playerDTotal > 50)
                        {
                            Console.WriteLine("PlayerD's total is now " + playerDTotal);
                            break;
                        }
                        // Let player press each button to hand/draw/add bet
                        Console.WriteLine("Press 'D' to draw another card, 'B' to add to your bet, 'H' to hold, or 'C' to check hands: "); var key = Console.ReadKey(true).Key;

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
                            if (playerDTotal > 50)
                            {
                                Console.WriteLine("PlayerD busted.");
                                Console.WriteLine("--------------------------------------------");
                                playerDCoins -= playerDBet;
                                break;
                            }
                            break;
                        }

                        // Place H to hold the card
                        if (key == ConsoleKey.H)
                        {
                            break; // PlayerD chooses to hold and ends their turn
                        }
                        if (key == ConsoleKey.C)
                        {
                            Console.WriteLine("PlayerD requests to check hands. Do all players agree? Press 'Y' for yes, 'N' for no: ");
                            var checkKey = Console.ReadKey(true).Key;

                            if (checkKey == ConsoleKey.Y)
                            {
                                // Check hands and end the game
                                break;
                            }
                            else if (checkKey == ConsoleKey.N)
                            {
                                // Continue the game
                                Console.WriteLine("Not all players agree. The game continues.");
                                Console.WriteLine("--------------------------------------------");
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please try again.");
                                Console.WriteLine("--------------------------------------------");
                                continue;
                            }
                        }
                    }
                }
                // Add score and bet coins after each round
                if (playerATotal > 50)
                {
                    Console.WriteLine("PlayerA busted.");
                    Console.WriteLine("--------------------------------------------");
                    playerACoins -= playerABet;
                }
                else if (playerBTotal > 50)
                {
                    Console.WriteLine("PlayerB busted.");
                    Console.WriteLine("--------------------------------------------");
                    playerBCoins -= playerBBet;
                }
                else if (playerCTotal > 50)
                {
                    Console.WriteLine("PlayerC busted.");
                    Console.WriteLine("--------------------------------------------");
                    playerCCoins -= playerCBet;
                }
                else if (playerDTotal > 50)
                {
                    Console.WriteLine("PlayerD busted.");
                    Console.WriteLine("--------------------------------------------");
                    playerDCoins -= playerDBet;
                }
                else
                {
                    int maxPoints = Math.Max(Math.Max(playerATotal, playerBTotal), Math.Max(playerCTotal, playerDTotal));
                    if (playerATotal == maxPoints)
                    {
                        Console.WriteLine("PlayerA wins this round!");
                        Console.WriteLine("--------------------------------------------");
                        playerACoins += playerBBet + playerCBet + playerDBet;
                        playerAPoints += 10;
                    }
                    if (playerBTotal == maxPoints)
                    {
                        Console.WriteLine("PlayerB wins this round!");
                        Console.WriteLine("--------------------------------------------");
                        playerBCoins += playerABet + playerCBet + playerDBet;
                        playerBPoints += 10;
                    }
                    if (playerCTotal == maxPoints)
                    {
                        Console.WriteLine("PlayerC wins this round!");
                        Console.WriteLine("--------------------------------------------");
                        playerCCoins += playerABet + playerBBet + playerDBet;
                        playerCPoints += 10;
                    }
                    if (playerDTotal == maxPoints)
                    {
                        Console.WriteLine("PlayerD wins this round!");
                        Console.WriteLine("--------------------------------------------");
                        playerDCoins += playerABet + playerBBet + playerCBet;
                        playerDPoints += 10;
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

                if (activePlayers == 1 || playerAPoints >= 100 || playerBPoints >= 100 || playerCPoints >= 100 || playerDPoints >= 100)
                {
                    if (playerACoins > 0 && playerAPoints >= 100)
                    {
                        Console.WriteLine("PlayerA wins the game!");
                        break;
                    }
                    else if (playerBCoins > 0 && playerBPoints >= 100)
                    {
                        Console.WriteLine("PlayerB wins the game!");
                        break;
                    }
                    else if (playerCCoins > 0 && playerCPoints >= 100)
                    {
                        Console.WriteLine("PlayerC wins the game!");
                        break;
                    }
                    else if (playerDCoins > 0 && playerDPoints >= 100)
                    {
                        Console.WriteLine("PlayerD wins the game!");
                        break;
                    }
                }
            }
        }

        // Reference:https://stackoverflow.com/questions/13038026/randomly-drawing-5-cards-from-a-deck-in-java
        static int DrawCard(List<string> deck, Random randomCards, string player)
        {
            if (deck.Count == 0)
            {
                Console.WriteLine("Deck is empty!");
                return 0;
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

    }
}
