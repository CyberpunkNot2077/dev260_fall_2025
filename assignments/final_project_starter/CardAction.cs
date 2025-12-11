using System;
using System.Collections.Generic;

namespace final_project_starter.Models
{
    public class CardAction
    {
        Queue<Card> drawFromDeck;
        List<Card> discardPile;
        public Queue<Card> DrawFromDeck { get { return drawFromDeck; } }
        public List<Card> DiscardPile { get { return discardPile; } }
        public CardAction()
        {
            drawFromDeck = CardCatalogue.drawFromDeck;
            discardPile = CardCatalogue.discardPile;
        }
        public void ShuffleDeck()
        {
            var deckList = new List<Card>(drawFromDeck);
            var random = new Random();
            int n = deckList.Count;
            while (n > 1)
            {
                int a = random.Next(n--);;
                Card temp = deckList[n];
                deckList[n] = deckList[a];
                deckList[a] = temp;
            }
            drawFromDeck = new Queue<Card>(deckList);
            CardCatalogue.drawFromDeck = drawFromDeck;
            
        }
    }
}