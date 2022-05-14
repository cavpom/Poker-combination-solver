using System.Collections.Generic;

namespace Poker.Combination.Solver
{
	public class PokerCombinationSolver
	{
		public List<Combination> Combinations = new List<Combination>();

		/// <summary>
		/// Register poker combination.
		/// </summary>
		/// <param name="combination">combination.</param>
		public void RegisterCombination(Combination combination)
		{
			Combinations.Add(combination);
		}

		/// <summary>
		/// What the best combination represents by this cards.
		/// </summary>
		/// <param name="combination">cards collection.</param>
		/// <returns>Best combination of this cards.</returns>
		public Combination SolveCombination(List<Card> cards)
		{
			CardsSummary summary = new CardsSummary(cards);
			foreach(Combination combination in Combinations)
			{
				if (combination.Validate(summary))
				{
					return combination.Clone();
				}
			}
			return null;
		}
	}
}