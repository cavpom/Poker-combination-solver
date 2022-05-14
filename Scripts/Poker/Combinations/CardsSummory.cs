using System.Collections.Generic;

namespace Poker.Combination
{
	/// <summary>
	/// Summory of a card collection.
	/// Different combinations needs same calculations.
	/// Cards summory makes solving group of combinations pretty faster.
	/// </summary>
	public class CardsSummary
	{
		/// <summary>
		/// Rates of a cards values. Last element is ace for straight definition.
		/// </summary>
		public CardValue[] StrokeRates = new CardValue[]
		{
			CardValue._A,
			CardValue._K,
			CardValue._Q,
			CardValue._J,
			CardValue._X,
			CardValue._9,
			CardValue._8,
			CardValue._7,
			CardValue._6,
			CardValue._5,
			CardValue._4,
			CardValue._3,
			CardValue._2,
			CardValue._A
		};

		public List<Card> Cards
		{
			private set; get;
		}

		/// <summary>
		/// Map of the cards with same suits.
		/// </summary>
		public Dictionary<CardSuit, List<Card>> SuitsMap
		{
			private set; get;
		}
		/// <summary>
		/// Map of the cards with same values.
		/// </summary>
		public Dictionary<CardValue, List<Card>> ValuesMap
		{
			private set; get;
		}

		/// <summary>
		/// Maximum cards whith same value in collection.
		/// </summary>
		public int MaximumSameValuesCount
		{
			private set; get;
		}

		/// <summary>
		/// Most frequent card value in collection
		/// If frequency equals, stores greatest value.
		/// </summary>
		public CardValue GreatestSameValue
		{
			private set; get;
		}

		/// <summary>
		/// Maximum cards whith same suit in collection.
		/// </summary>
		public int MaximumSameSuitsCount
		{
			private set; get;
		}

		/// <summary>
		/// Most frequent card suit in collection
		/// </summary>
		public CardSuit GreatestSameSuit
		{
			private set; get;
		}

		/// <summary>
		/// Card collection has a 5 or longest stoke of a ordered card values.
		/// </summary>
		public bool HasStraight
		{
			private set; get;
		}

		/// <summary>
		/// Store index of a high card of a straight.
		/// </summary>
		public int HighestStraightCardIndex
		{
			private set; get;
		}

		/// <summary>
		/// Generate summory of a cards.
		/// </summary>
		/// <param name="cards">List of cards.</param>
		public CardsSummary(List<Card> cards)
		{
			Cards = new List<Card>(cards);
			Cards.Sort((Card a, Card b) => b.Value.CompareTo(a.Value));
			SuitsMap = new Dictionary<CardSuit, List<Card>>();
			ValuesMap = new Dictionary<CardValue, List<Card>>();
			MappingCards();
		}

		private void MappingCards()
		{
			foreach (Card card in Cards)
			{
				if (!ValuesMap.ContainsKey(card.Value)) ValuesMap[card.Value] = new List<Card>();
				ValuesMap[card.Value].Add(card);

				if (ValuesMap[card.Value].Count > MaximumSameValuesCount)
				{
					MaximumSameValuesCount = ValuesMap[card.Value].Count;
					GreatestSameValue = card.Value;
				}

				if (!SuitsMap.ContainsKey(card.Suit)) SuitsMap[card.Suit] = new List<Card>();
				SuitsMap[card.Suit].Add(card);

				if (SuitsMap[card.Suit].Count > MaximumSameSuitsCount)
				{
					MaximumSameSuitsCount = SuitsMap[card.Suit].Count;
					GreatestSameSuit = card.Suit;
				}
			}

			int strokeCount = 0;
			for (int i = 0; i < StrokeRates.Length; i++)
			{
				if (ValuesMap.ContainsKey(StrokeRates[i]))
				{
					strokeCount++;
					if (strokeCount == 5)
					{
						HasStraight = true;
						HighestStraightCardIndex = i - 4;
						break;
					}
				}
				else
				{
					if (i > StrokeRates.Length - 4) break;
					strokeCount = 0;
				}
			}
		}
	}
}