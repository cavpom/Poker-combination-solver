namespace Poker.Combination
{
	/// <summary>
	/// Pair combination.
	/// 2 cards of same values.
	/// </summary>
	public class CombinationPair : Combination
	{
		public static string TYPE = "PAIR";

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

			if (summary.MaximumSameValuesCount < 2) return false;

			CombinationCards = summary.ValuesMap[summary.GreatestSameValue].GetRange(0, 2);

			SortCards();

			return true;
		}
	}
}