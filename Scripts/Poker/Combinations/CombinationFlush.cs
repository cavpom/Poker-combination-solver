namespace Poker.Combination
{
	/// <summary>
	/// Flush combination.
	/// 5 cards of same suit.
	/// </summary>
	public class CombinationFlush : Combination
	{
		public static string TYPE = "FLUSH";

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

			if (summary.MaximumSameSuitsCount < 5) return false;

			CombinationCards = summary.SuitsMap[summary.GreatestSameSuit].GetRange(0, 5);

			SortCards();

			return true;
		}
	}
}