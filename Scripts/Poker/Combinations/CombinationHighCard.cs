namespace Poker.Combination
{
	/// <summary>
	/// High card combination.
	/// Any combination of high rate cards.
	/// </summary>
	public class CombinationHighCard : Combination
	{
		public static string TYPE = "HIGH_CARD";

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

			CombinationCards = summary.Cards.Count > 5 ? summary.Cards.GetRange(0, 5) : summary.Cards;

			return true;
		}
	}
}