using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using final_project_starter.Models;
using final_project_starter.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace final_project_starter
{
    class Program
    {
        static void Main(string[] args)
        {
            var GetYourCardOn = new CardGame();
            GetYourCardOn.GameTitle = "Get Your Summon On!";
            GetYourCardOn.numberOfPlayers = 2;
            GetYourCardOn.StartGame();
            GetYourCardOn.ShuffleDeck();
            GetYourCardOn.DisplayPlayerStats();
            GetYourCardOn.PlayerDrawStartingHand();
            GetYourCardOn.OpponentDrawStartingHand();
            GetYourCardOn.RulesOfCardgame();
            GetYourCardOn.PlayerTurn();
            GetYourCardOn.PlayerHand();
            GetYourCardOn.PlayerSummonCreature();
            GetYourCardOn.PlayerEquipWeapon();
            GetYourCardOn.PlayerUseAbility();
            GetYourCardOn.PlayerAttack();
            GetYourCardOn.OpposingPlayerTurn();
            GetYourCardOn.OpponentSummonCreature();
            GetYourCardOn.OpponentEquipWeapon();
            GetYourCardOn.OpponentUseAbility();
            GetYourCardOn.OpponentAttack();            
            GetYourCardOn.EndGame();

            Console.WriteLine("Press any key to leave the game...");
            Console.ReadKey();
        }

    }
}