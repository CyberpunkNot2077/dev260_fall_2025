using System;
using System.Collections.Generic;
using System.Linq;
using final_project_starter.Models;

namespace final_project_starter.Data
{
    class CardCatalogue
    {
        public static List<SummonCard> list = new List<SummonCard>();
        public static List<WeaponCard> weaponList = new List<WeaponCard>();
        public static Queue <Card> drawFromDeck = new Queue<Card>();
        public static List<Card> discardPile = new List<Card>();
        public static Dictionary<string, int> cardAbilities = new Dictionary<string, int>();
        public static Queue<Card> DrawFromDeck = new Queue<Card>();

        public static List<SummonCard> GetCatalogue()

        {
            return list;
        }
        public static List<WeaponCard> GetWeaponCatalogue()
        {
            return weaponList;
        }
        public static Dictionary<string, int> GetAbilitiesCatalogue()
        {
            return cardAbilities;
        }
        public static void ClearCatalogues()
        {
            list.Clear();
            weaponList.Clear();
        }
        public static Queue<Card> GetDrawFromDeck()
        {
            return new Queue<Card>();
        }
        public static List<Card> GetDiscardPile()
        {
            return new List<Card>();
        }
        public static void SummonCatalogue()
        {
            list.Add(new SummonCard("Flame Dragon", "A mencacing dragon engulfed in flames.", 8, 5));
            list.Add(new SummonCard("Thalassic Serpent", "A gargantuan sea serpent that reigns over the oceans.", 7, 6));
            list.Add(new SummonCard("Terra Firma Golem", "A colossal golem constructed of rock and earth.", 6, 7));
            list.Add(new SummonCard("The Immortal Phoenix", "A legendary firebird that is reborn from its ashes.", 9, 4));
            list.Add(new SummonCard("Assassin of the Darkness", "A stealthy infiltrator who strikes from the shadows.", 5, 8));
            list.Add(new SummonCard("Celestial Goddess", "A divine being who commands the power of the heavens.", 10, 3));
            list.Add(new SummonCard("Paladin of the Forest", "A noble warrior who shields the ancient woods.", 6, 6));
            list.Add(new SummonCard("Forgotten Thief", "A clever criminal who excels in stealth and cunning.", 4, 7));
            list.Add(new SummonCard("Dark Sorcerer", "A master of dark magic who bears great power.", 8, 5));
            list.Add(new SummonCard("Odin, the AllFather", "The leading God of Norse Mythology.", 9, 6));
            list.Add(new SummonCard("Zeus, The King of the Gods", "The leading God of Greek Mythology.", 9, 6));
            list.Add(new SummonCard("Ra, God of the Sun", "The God of the Egyptian Sun.", 8, 5));
            list.Add(new SummonCard("Rhythmic Bard", "A melodius musician whose tunes can heal allies or confuse foes.", 5, 7));
        }
        public static void WeaponCatalogue()
        {
            weaponList.Add(new WeaponCard("Caladbolg", "A legendary sword said to cut through anything.", 7, 3));
            weaponList.Add(new WeaponCard("Gungnir", "Odin's spear that never misses its mark.", 6, 4));
            weaponList.Add(new WeaponCard("Keraunos", "Zeus's thunderbolt, a weapon of incredible power.", 8, 2));
            weaponList.Add(new WeaponCard("Caduceus", "Hermes's staff capable of inducing sleep and resurrecting the dead.", 5, 5));
            weaponList.Add(new WeaponCard("The Ankh of Life", "An ancient Egyptian symbol that grants eternal life.", 4, 6));
            weaponList.Add(new WeaponCard("Dagger of Time", "A magical dagger that can manipulate time itself.", 6, 3));
            weaponList.Add(new WeaponCard("Crest of the Lion", "A shield that houses the spirit of a mighty lion.", 5, 4));
            weaponList.Add(new WeaponCard("Greatbow of Artemis", "A bow that does not miss its target.", 7, 2));
            weaponList.Add(new WeaponCard("Lute of Healing", "A melodius lute that can heal wounds when played.", 4, 5));
            weaponList.Add(new WeaponCard("Staff of the Magi", "A powerful staff that enhances magical abilities.", 8, 3));
            weaponList.Add(new WeaponCard("Deathscythe", "A fear-inducing scythe that can reap the souls of the living.", 9, 1));

        }
        public static void AbilitiesCatalogue()
        {
            cardAbilities.Add("Fireball", 4);
            cardAbilities.Add("Tsunami", 5);
            cardAbilities.Add("Aftershock", 3);
            cardAbilities.Add("Rebirth", 6);
            cardAbilities.Add("Invisibility", 2);
            cardAbilities.Add("Mirror Image", 3);
            cardAbilities.Add("Lightning Storm", 5);
            cardAbilities.Add("Revitalizing Light", 4);
            cardAbilities.Add("Blizzard", 4);
            cardAbilities.Add("Tornado", 5);
            cardAbilities.Add("Meteor Storm", 6);
            cardAbilities.Add("Earthquake", 5);
            cardAbilities.Add("Luminous Lance", 4);
            cardAbilities.Add("Strike of the Darkness", 3);
            cardAbilities.Add("Healing Aria", 4);
            cardAbilities.Add("Petrify", 3);
            cardAbilities.Add("Time Stop", 5);
            cardAbilities.Add("Soul Steal", 4);
            cardAbilities.Add("Sacred Shield", 3);
        }
        private static Random randomize = new Random();
        public static void InitializeDeck()
        {
            List<Card> GetEveryCard = new List<Card>();
            GetEveryCard.AddRange(list);
            GetEveryCard.AddRange(weaponList);
            foreach (var ability in cardAbilities)
            {
                GetEveryCard.Add(new AbilityCard(ability.Key, "A ridiculously strong ability.", ability.Value));
            }
            GetEveryCard = GetEveryCard.OrderBy(card => Guid.NewGuid()).ToList();
            drawFromDeck = new Queue<Card>(GetEveryCard);
        }
        public List<Card> ShuffleList(List<Card> deckToShuffle)
        {
            for (int i = deckToShuffle.Count - 1; i > 0; i--)
            {
                var b = randomize.Next(i + 1);
                var cardPlay = deckToShuffle[b];
                deckToShuffle[b] = deckToShuffle[i];
                deckToShuffle[i] = cardPlay;
            }
            return deckToShuffle;
        }
    }
}