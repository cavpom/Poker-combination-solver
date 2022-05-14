using UnityEngine;
using Poker.Combination.Solver;
using Poker.Combination;
using System.Collections.Generic;

public class TexasHoldemTest : MonoBehaviour
{
	public int Itterations = 10;
	private PokerCombinationSolver solver;
	private Deck deck;

	[Inspector.Button]
	private void RunBruteForceTest()
	{
		solver = new PokerCombinationSolver();

		solver.RegisterCombination(new CombinationRoyalFlush()		.SetRank(9) );
		solver.RegisterCombination(new CombinationStraightFlush()	.SetRank(8) );
		solver.RegisterCombination(new CombinationFourOfAKind()		.SetRank(7) );
		solver.RegisterCombination(new CombinationFullHouse()		.SetRank(6) );
		solver.RegisterCombination(new CombinationFlush()		.SetRank(5) );
		solver.RegisterCombination(new CombinationStraight()		.SetRank(4) );
		solver.RegisterCombination(new CombinationThreeOfAKind()	.SetRank(3) );
		solver.RegisterCombination(new CombinationTwoPairs()		.SetRank(2) );
		solver.RegisterCombination(new CombinationPair()		.SetRank(1) );
		solver.RegisterCombination(new CombinationHighCard()		.SetRank(0) );

		deck = new Deck();
		deck.GenerateCards();

		for (int i = 1; i <= Itterations; i++)
		{
			Debug.Log("test #" + i);
			RunTestItteration();
		}
	}

	private void RunTestItteration()
	{
		List<Card> table = deck.GetCards(5);
		List<Card> hand_a = deck.GetCards(2);
		List<Card> hand_b = deck.GetCards(2);
		
		Debug.Log(string.Join(",", new List<Card>(table).ConvertAll<string>(e => e.ToString()).ToArray()));
		Debug.Log("A: " + string.Join(",", new List<Card>(hand_a).ConvertAll<string>(e => e.ToString()).ToArray()));
		Debug.Log("B: " + string.Join(",", new List<Card>(hand_b).ConvertAll<string>(e => e.ToString()).ToArray()));

		hand_a.AddRange(table);
		hand_b.AddRange(table);

		Combination combination_a = solver.SolveCombination(hand_a);
		Combination combination_b = solver.SolveCombination(hand_b);

		Debug.Log(combination_a);
		Debug.Log(combination_b);

		if (combination_a < combination_b)
		{
			Debug.Log("B win");
		}
		else
		{
			if (combination_b < combination_a)
			{
				Debug.Log("A win");
			}
			else
			{
				Debug.Log("draw");
			}
		}
		deck.Reset();
	}
}
