namespace Poker.Combination
{
	/// <summary>
	/// four of a kind combination.
	/// 4 cards of same values.
	/// </summary>
	public class CombinationFourOfAKind : Combination
	{
		public static string TYPE = "FOUR_OF_A_KIND";

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

			if (summary.MaximumSameValuesCount < 4) return false;

			CombinationCards = summary.ValuesMap[summary.GreatestSameValue].GetRange(0, 4);

			SortCards();

			return true;
		}
	}
}