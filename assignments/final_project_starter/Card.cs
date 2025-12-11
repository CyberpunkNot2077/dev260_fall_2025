using System;
using System.Collections.Generic;

namespace final_project_starter.Models
{
    public abstract class Card
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Card(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }

    public class SummonCard : Card
    {
        public int Attack { get; set; }
        public int Defense { get; set; }

        public SummonCard(string name, string description, int attack, int defense) 
            : base(name, description)
        {
            Attack = attack;
            Defense = defense;
        }
    }

    public class WeaponCard : Card
    {
        public int Strength { get; set; }
        public int Price { get; set; }

        public WeaponCard(string name, string description, int power, int cost) 
            : base(name, description)
        {
            Strength = power;
            Pirce = cost;
        }
    }

    public class AbilityCard : Card
    {
        public int Cost { get; set; }

        public AbilityCard(string name, string description, int cost) 
            : base(name, description)
        {
            Cost = cost;
        }
    }
}
