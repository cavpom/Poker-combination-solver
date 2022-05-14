using System.Collections.Generic;

namespace Poker.Combination
{
	/// <summary>
	/// Straight combination.
	/// Five cards of sequential rank.
	/// </summary>
	public class CombinationStraight : Combination
	{
		public static string TYPE = "STRAIGHT";

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

			CombinationCards = new List<Card>();
			for (int i = 0; i < 5; i++)
			{
				CardValue cardValue = summary.StrokeRates[summary.HighestStraightCardIndex + i];
				CombinationCards.Add(summary.ValuesMap[cardValue][0]);
			}

			SortCards();

			return true;
		}
	}
}