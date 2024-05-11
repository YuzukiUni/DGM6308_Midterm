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
            // Shuffle and List all monotype cards
            Random randomCards = new Random();
            List<string> deck = new List<string> { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
            // Intialize the Starting points and coins for player and computer
            int playerPoints = 0, computerPoints = 0;
            int playerCoins = 100, computerCoins = 100;
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

                // Starting a bet for computer and player
                int playerBet = betLimit;
                int computerBet = betLimit;

                // If player and computer have not enough coins to bet, do "all in " process
                if (playerBet > playerCoins)
                {
                    Console.WriteLine("You don't have enough coins. Your bet is set to " + playerCoins);
                    playerBet = playerCoins;
                }
                if (computerBet > computerCoins)
                {
                    Console.WriteLine("Computer doesn't have enough coins. Its bet is set to " + computerCoins);
                    computerBet = computerCoins;
                }

                // Write down the starting bet for each one
                Console.WriteLine("Your bet is " + playerBet);
                Console.WriteLine("Computer's bet is " + computerBet);
                // Intialize the total points of each player before draw card
                int playerTotal = 0, computerTotal = 0;
                // Reference:https://raisanenmarkus.github.io/csharp/part5/1/
                // Player and computer draw cards
                playerTotal += DrawCard(deck, randomCards, "You");
                computerTotal += DrawCard(deck, randomCards, "Computer");
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("Your total is " + playerTotal);
                Console.WriteLine("Computer total is " + computerTotal);
                Console.WriteLine("--------------------------------------------");

                // In game progress, I change some special rules for this game
                while (playerTotal < 50)
                {
                    // If player total >50, end the round
                    if (playerTotal > 50)
                    {
                        Console.WriteLine("Your total is now " + playerTotal);
                        break;
                    }
                    // Let player press each button to hand/draw/add bet
                    Console.WriteLine("Press 'D' to draw another card, 'B' to add to your bet, or 'H' to hold: ");
                    var key = Console.ReadKey(true).Key;

                    // Invalid key beside D,B,and H
                    if (key != ConsoleKey.D && key != ConsoleKey.B && key != ConsoleKey.H)
                    {
                        Console.WriteLine("Invalid input. Please try again.");
                        Console.WriteLine("--------------------------------------------");
                        continue;
                    }

                    // Add bet by input number
                    if (key == ConsoleKey.B)
                    {
                        Console.Write("Enter your additional bet (limit " + betLimit + "): ");
                        int additionalBet = int.Parse(Console.ReadLine());

                        // Bet regualtion, restrict player to add bet within betLimit
                        if (additionalBet > betLimit || additionalBet > playerCoins - playerBet)
                        {
                            // Reference:https://learn.microsoft.com/en-us/dotnet/api/system.math.min?view=net-8.0
                            Console.WriteLine("Invalid bet. Your additional bet is set to " + Math.Min(betLimit, playerCoins - playerBet));
                            Console.WriteLine("--------------------------------------------");
                            additionalBet = Math.Min(betLimit, playerCoins - playerBet);
                        }
                        playerBet += additionalBet;
                        // After Player add bet, computer also add bet by random number
                        Random comBet = new Random();
                        int computerAdditionalBet = comBet.Next(0, betLimit + 1);
                        computerBet += computerAdditionalBet;
                        Console.WriteLine("Computer's additional bet is " + computerAdditionalBet);
                    }
                    // Place D to draw cards
                    if (key == ConsoleKey.D)
                    {
                        playerTotal += DrawCard(deck, randomCards, "You");
                        Console.WriteLine("Your total is now " + playerTotal);
                        Console.WriteLine("--------------------------------------------");
                        // End the round if player busted 
                        if (playerTotal > 50)
                        {
                            Console.WriteLine("You busted. Computer wins!");
                            Console.WriteLine("--------------------------------------------");
                            playerCoins -= playerBet;
                            computerCoins += playerBet;
                            computerPoints += 10;
                            break;
                        }
                        computerTotal += DrawCard(deck, randomCards, "Computer");
                        Console.WriteLine("The computers's total is now " + computerTotal);
                        Console.WriteLine("--------------------------------------------");
                        // End the round if computer busted 
                        if (computerTotal > 50)
                        {
                            Console.WriteLine("Computer busted. You wins!");
                            Console.WriteLine("--------------------------------------------");
                            playerCoins += computerBet;
                            computerCoins -= computerBet;
                            playerPoints += 10;
                            break;
                        }
                    }

                    // Place H to hold the card
                    else if (key == ConsoleKey.H)
                    {

                        if (playerTotal < computerTotal)
                        {
                            playerTotal += DrawCard(deck, randomCards, "You");
                        }
                        // If computer hand is less, it need to draw one more card 
                        if (computerTotal < playerTotal)
                        {
                            computerTotal += DrawCard(deck, randomCards, "Computer");
                        }
                        break;
                    }
                }

                // Add score and bet coins after each round
                // Check for busts after all drawing is done
                if (playerTotal > 50)   // Special Rule Player busted (for final result)
                {
                    Console.WriteLine("You busted. Computer gets the bet.");
                    Console.WriteLine("--------------------------------------------");
                    playerCoins -= playerBet;
                    computerCoins += playerBet;
                    computerPoints += 10;
                }
                else if (computerTotal > 50)
                {
                    Console.WriteLine("Computer busted. You wins!");
                    Console.WriteLine("--------------------------------------------");
                    playerCoins += computerBet;
                    computerCoins -= computerBet;
                    playerPoints += 10;
                }
                // Player win the round and get coins and scores
                else if (playerTotal > computerTotal)
                {
                    Console.WriteLine("You win this round! ");
                    Console.WriteLine("--------------------------------------------");
                    playerCoins += computerBet;
                    computerCoins -= computerBet;
                    playerPoints += 10;
                }
                // Computer win the round and get coins and scores
                else if (playerTotal < computerTotal)
                {
                    Console.WriteLine("Computer wins this round!");
                    Console.WriteLine("--------------------------------------------");
                    playerCoins -= playerBet;
                    computerCoins += playerBet;
                    computerPoints += 10;
                }
                else
                {
                    Console.WriteLine("It's a draw! No one wins the bet.");
                    Console.WriteLine("--------------------------------------------");
                }
                // Reference:https://blog.csdn.net/weixin_43328198/article/details/85311232
                Console.WriteLine($"Score: You - {playerPoints}, Computer - {computerPoints}");
                Console.WriteLine($"Coins: You - {playerCoins}, Computer - {computerCoins}");
                Console.WriteLine("--------------------------------------------");

                // Win condition
                if (playerPoints >= 100 || computerCoins < 0)
                {
                    Console.WriteLine("You win the game!");
                    break;
                }
                else if (computerPoints >= 100 || playerCoins < 0)
                {
                    Console.WriteLine("Computer wins the game!");
                    break;
                }
            }
        }

        // Reference:https://stackoverflow.com/questions/13038026/randomly-drawing-5-cards-from-a-deck-in-java
        static int DrawCard(List<string> deck, Random randomCards, string player)
        {
            string card = deck[randomCards.Next(deck.Count)];

            Console.WriteLine(player + " drew a " + card);
            // Ace should be a value of 11
            if (card == "A")
            {
                return 11;
            }
            else if (card == "J" || card == "Q" || card == "K")
            {
                return 10;
            }
            else
            {
                return int.Parse(card);
            }
        }
    }
}
