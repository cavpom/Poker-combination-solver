using System.Collections.Generic;
using UnityEngine;

public class Deck
{
	private int usedCardsCount = 0;
	private List<Card> cards;

	public Deck()
	{

	}

	public void GenerateCards()
	{
		cards = new List<Card>();
		CardValue[] values = (CardValue[])System.Enum.GetValues(typeof(CardValue));
		CardSuit[] suits = (CardSuit[])System.Enum.GetValues(typeof(CardSuit));

		foreach (CardSuit suit in suits)
		{
			foreach (CardValue value in values)
			{
				cards.Add(new Card(suit, value));
			}
		}
	}

	public Card GetRandomCard()
	{
		int index = Random.Range(usedCardsCount, cards.Count);
		Card result = cards[index];
		cards[index] = cards[usedCardsCount];
		cards[usedCardsCount] = result;

		usedCardsCount++;

		return result;
	}

	public List<Card> GetCards(int cardsCount)
	{
		List<Card> result = new List<Card>();
		for (int i = 0; i < cardsCount; i++)
		{
			result.Add(GetRandomCard());
		}
		return result;
	}

	public void Reset()
	{
		usedCardsCount = 0;
	}
}