namespace Poker.Combination
{
	/// <summary>
	/// Two pairs combination.
	/// 2 cards + 2 cards of same values.
	/// Sometimes 2 + 2 + 2 in 7 cards poker. But include only 2 groups of greatest pairs. 
	/// </summary>
	public class CombinationTwoPairs : Combination
	{
		public static string TYPE = "TWO_PAIRS";

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

			//exept last ace
			for (int i = 0; i < summary.StrokeRates.Length - 1; i++)
			{
				CardValue value = summary.StrokeRates[i];
				if (value != summary.GreatestSameValue && summary.ValuesMap.ContainsKey(value) && summary.ValuesMap[value].Count >= 2)
				{
					CombinationCards = summary.ValuesMap[summary.GreatestSameValue].GetRange(0, 2);
					CombinationCards.AddRange(summary.ValuesMap[value].GetRange(0, 2));
					SortCards();
					return true;
				}
			}

			return false;
		}
	}
}