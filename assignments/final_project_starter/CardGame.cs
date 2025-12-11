using System;
using System.Collections.Generic;
using System.Linq;
using final_project_starter.Data;

namespace final_project_starter.Models
{
    public class CardGame
    {
        public string GameTitle {get; set;}
        public int numberOfPlayers {get; set;}
        public int currentPlayerTurn {get; set;}
        public int opponentTurn {get; set;}
        public int playerHealth {get; set;}
        public int opponentHealth {get; set;}
        public CardGame(string gameTitle, int numPlayers)
        {
            GameTitle = gameTitle;
            numberOfPlayers = numPlayers;
        }
        public void RulesOfCardgame()
        {
            Console.WriteLine("Before we get started, here are the rules of the game: ");
            Console.WriteLine("First, each player draws 5 cards from the deck.");
            Console.WriteLine("Second, players take turns playing cards from their hands.");
            Console.WriteLine("Third, the aim of the game is to reduce your opponent's health to zero.");
            Console.WriteLine("Players can summon creatures and equip armaments to attack their opponent or protect themselves.");
            Console.WriteLine("Players can also use special abilities of their cards to turn the tide of battle.");
            Console.WriteLine("Next, if a player's deck is empty, they shuffule from their discard pile to form a new deck.");
            Console.WriteLine("Players draw a card when their turn begins.");
            Console.WriteLine("PLayers can also play one weapon and on summon card per turn.");
            Console.WriteLine("Finally, the game ends when one player has no health left.");
        }
        public void StartGame()
        {
            Console.WriteLine($"Welcome to {GameTitle}!");
            Console.WriteLine($"Let's get started with {numberOfPlayers} players.");

            CardCatalogue.SummonCatalogue();
            CardCatalogue.WeaponCatalogue();
            CardCatalogue.AbilitiesCatalogue();
            CardCatalogue.InitializeDeck();
            Console.WriteLine("All card catalogues have been initialized.");
            // More game logic to be included here
            var drawFromDeck = CardCatalogue.drawFromDeck;
            Console.WriteLine("Let the game...BEGIN!");
        }
        public void DisplayPlayerStats()
        {
            Console.Write($"Player Health: {playerHealth} / Opponent's Health: {opponentHealth}");
        }
        public void PlayerDrawStartingHand()
        {
            var drawFromDeck = CardCatalogue.drawFromDeck;
            Console.WriteLine("Drawing your beginning hand of 5 cards...");
            for (int i = 0; i < 5; i++)
            {
                if (drawFromDeck.Count > 0)
                {
                    Card drawnCard = drawFromDeck.Dequeue();
                    Console.WriteLine($"Card {i + 1}: {drawnCard.Name}");
                }
            }
            
        }
        
        public void PlayerTurn()
        {
            Console.WriteLine("It's your turn! Make your move.");
            // Logic for the player's turn goes here
            Console.WriteLine("That is the end of your turn.");

        }
        public void PlayerDrawACard()
        {
            var drawFromDeck = CardCatalogue.DrawFromDeck;
            if (drawFromDeck.Count > 0)
            {
                Card drawnCard = drawFromDeck.Dequeue();
                Console.WriteLine($"The card you have drawn is: {drawnCard.Name}");
            }
        }
        public void OpposingPlayerTurn()
        {
            Console.WriteLine("Now it's your opponent's turn.");
            // Logic for the opponent's turn goes here
            var opponentDrawDeck = CardCatalogue.drawFromDeck;
            if (opponentDrawDeck.Count > 0)
            {
                opponentDrawDeck.Dequeue();
            }
            Console.WriteLine("And so ends your opponent's turn.");
        }
        public CardGame()
        {
            GameTitle = "Get Your Summon On!";
            numberOfPlayers = 2;
            currentPlayerTurn = 1;
            opponentTurn = 2;
            playerHealth = 30;
            opponentHealth = 30;
        }
        public void PlayerHand()
        {
            Console.WriteLine("The cards in your hand are: ");
            for (int i = 0; i < 5 && i < CardCatalogue.list.Count; i++)
            {
                Console.WriteLine($"Card {i + 1}: {CardCatalogue.list[i].Name}");
            }
        }
        public static bool PleaseMakeYourChoicePlayer(string? input)
        {
            
            if (input != null)
            {
                return;
            } else {
                Console.WriteLine("Please type in one of your card's names.");
            }
        }
        public void PlayerSummonCreature()
        {
            Console.WriteLine($"You have summoned {CardCatalogue.list[0].Name} to the fray!");
        }
        public void PlayerEquipWeapon()
        {
            Console.WriteLine($"You have armed your creature with {CardCatalogue.weaponList[0].Name}!");
        }
        public void PlayerUseAbility()
        {
            var abilities = CardCatalogue.GetAbilitiesCatalogue();
            if (abilities.Count > 0)
            {
                Console.WriteLine($"You used the ability {abilities.Keys.First()}!");
            }
        }
        public void PlayerAttack()
        {
            Console.WriteLine("You attack your opponent!");
            if  (opponentHealth > 0)
            {
                opponentHealth -= 5;
                Console.WriteLine($"Your opponent's health is now {opponentHealth}.");
            }
            else
            {
                Console.WriteLine("YOU HAVE BEATEN YOUR OPPONENT!");
            }
        }
        
        public void OpponentSummonCreature()
        {
            Console.WriteLine($"Your opponent has summoned {CardCatalogue.list[1].Name} to the fray!");
        }
        public void OpponentEquipWeapon()
        {
            Console.WriteLine($"Your opponent has equipped their creature with {CardCatalogue.weaponList[1].Name}!");
        }
        public void OpponentUseAbility()
        {
            var abilities = CardCatalogue.GetAbilitiesCatalogue();
            if (abilities.Count > 1)
            {
                Console.WriteLine($"Your opponent has used the ability {abilities.Keys.ElementAt(1)}!");
            }
        }
        public void OpponentAttack()
        {
            Console.WriteLine("You are attacked by your opponent!");
            if (playerHealth > 0)
            {
                playerHealth -= 5;
                Console.WriteLine($"Your health is now {playerHealth}.");
            }
            else
            {
                Console.WriteLine("YOU HAVE BEEN BEATEN!");
            }
        }
        public void RecoverPlayerHealth()
        {
            playerHealth += 5;
            Console.WriteLine($"Your health has been recovered all the way to {playerHealth}.");
        }
        public void RecoverOpponentHealth()
        {
            opponentHealth += 5;
            Console.WriteLine($"Your opponent's health has been boosted to {opponentHealth}.");
        }
        public void EndGame()
        {
            Console.WriteLine("Thanks for playing the game! See ya Next Time!");
        }
    }
}