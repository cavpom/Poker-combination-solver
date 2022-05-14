using UnityEngine;

namespace Poker.Combination
{
	/// <summary>
	/// Three of a kind combination.
	/// 3 cards of same values.
	/// </summary>
	public class CombinationThreeOfAKind : Combination
	{
		public static string TYPE = "THREE_OF_A_KIND";

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

			if (summary.MaximumSameValuesCount < 3) return false;

			CombinationCards = summary.ValuesMap[summary.GreatestSameValue].GetRange(0, 3);

			SortCards();

			return true;
		}
	}
}