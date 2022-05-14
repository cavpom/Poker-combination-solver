using System.Collections.Generic;

namespace Poker.Combination
{
	/// <summary>
	/// Straight flush combination.
	/// Five same suited cards of sequential rank.
	/// In 7 cards poker needs 2 validation, cause straight and flush can contains different cards.
	/// </summary>
	public class CombinationStraightFlush : Combination
	{
		public static string TYPE = "STRAIGHT_FLUSH";

		public override string Type
		{
			get
			{
				return TYPE;
			}
		}

		public override bool Validate(CardsSummary summary)
		{
			base.Validate(summary);

			if (!summary.HasStraight) return false;
			if (summary.MaximumSameSuitsCount < 5) return false;

			// flush
			List<Card> flushCards = summary.SuitsMap[summary.GreatestSameSuit];

			CardsSummary tSummary = new CardsSummary(flushCards);

			if (!tSummary.HasStraight) return false;

			CombinationCards = new List<Card>();
			for (int i = 0; i < 5; i++)
			{
				CardValue cardValue = tSummary.StrokeRates[tSummary.HighestStraightCardIndex + i];
				CombinationCards.Add(tSummary.ValuesMap[cardValue][0]);
			}

			SortCards();

			return true;
		}
	}
}