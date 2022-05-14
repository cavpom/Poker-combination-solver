using System;
using System.Collections.Generic;
using UnityEngine;

namespace Poker.Combination
{
	/// <summary>
	/// Poker card combination
	/// </summary>
	public class Combination : IComparable<Combination>, IEquatable<Combination>
	{
		/// <summary>
		/// Name of the combination
		/// </summary>
		public virtual string Type
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// Rank of the combination.
		/// </summary>
		public int Rank
		{
			private set; get;
		}

		public Combination()
		{
		}

		/// <summary>
		/// Set rank of the combination. Combination with highest rank always beat combination with lower rank.
		/// </summary>
		/// <param name="value">Combination value.</param>
        /// <returns>This combination</returns>
		public Combination SetRank(int value)
		{
			Rank = value;
			return this;
		}

		/// <summary>
		/// Meaningful cards of combination.
		/// </summary>
		public List<Card> CombinationCards
		{
			protected set; get;
		}

		/// <summary>
		/// All cards of combination.
		/// </summary>
		public List<Card> AllCards
		{
			protected set; get;
		}

		/// <summary>
		/// Validate if combination wins.
		/// </summary>
		/// <param name="summary">Cards summory.</param>
        /// <returns>Is validation success</returns>
		public virtual bool Validate(CardsSummary summary)
		{
			CombinationCards = null;
			AllCards = summary.Cards;
			return false;
		}

		/// <summary>
		/// In bring to front meaningful cards.
		/// </summary>
		protected void SortCards()
		{
			for (int i = CombinationCards.Count - 1; i >= 0; i--)
			{
				int index = AllCards.IndexOf(CombinationCards[i]);
				if (index > 0)
				{
					AllCards.RemoveAt(index);
					AllCards.Insert(0, CombinationCards[i]);
				}
			}
		}


		private int combinationRate = -1;
		/// <summary>
		/// Get rate of the combination.
		/// Any combination with highest rate beats combination with lower rate.
		/// If rates equals, combinations equals too.
		/// Two combinations with different meaningful cards always gives different rate.
		/// </summary>
        /// <returns>Unique rate of combination</returns>
		public virtual int GetCombinationRate()
		{
			if (combinationRate == -1 && AllCards != null)
			{
				combinationRate = Rank;
				for (int i = 0; i < Mathf.Min(AllCards.Count, 5); i++)
				{
					combinationRate *= 15;
					combinationRate += (int)AllCards[i].Value;
				}
			}
			return combinationRate;
		}

		/// <summary>
		/// Clone combination.
		/// </summary>
        /// <returns>New instance of this combination</returns>
		public Combination Clone()
		{
			Combination result = Activator.CreateInstance(this.GetType()) as Combination;

			if (result != null)
			{
				result.AllCards = AllCards.GetRange(0, Mathf.Min(AllCards.Count, 5));
				result.CombinationCards = new List<Card>(CombinationCards);
				result.Rank = Rank;
			}
			
			return result;
		}

		public override string ToString()
		{
			string allcards = AllCards != null ? string.Join(",", new List<Card>(AllCards).ConvertAll<string>(e => e.ToString()).ToArray()) : string.Empty;
			string wincards = AllCards != null ? string.Join(",", new List<Card>(CombinationCards).ConvertAll<string>(e => e.ToString()).ToArray()) : string.Empty;

			return Type + " \t" + wincards + " \t" + allcards + " \t" + GetCombinationRate();
		}

		#region IComparable
		public int CompareTo(Combination other)
		{
			int result = this.Rank.CompareTo(other.Rank);
			if (result == 0) result = this.GetCombinationRate().CompareTo(other.GetCombinationRate());
			return result;
		}
		#endregion

		#region IEquatable
		public bool Equals(Combination other)
		{
			if (object.ReferenceEquals(other, null)) return false;
			return CompareTo(other) == 0;
		}

		public override bool Equals(object other)
		{
			return Equals(other as Combination);
		}

		public override int GetHashCode()
		{
			return GetCombinationRate();
		}
		#endregion

		#region Comparable operators
		public static bool operator > (Combination lhs, Combination rhs)
		{
			return lhs.CompareTo(rhs) > 0;
		}

		public static bool operator >= (Combination lhs, Combination rhs)
		{
			return lhs.CompareTo(rhs) >= 0;
		}

		public static bool operator < (Combination lhs, Combination rhs)
		{
			return lhs.CompareTo(rhs) < 0;
		}

		public static bool operator <= (Combination lhs, Combination rhs)
		{
			return lhs.CompareTo(rhs) <= 0;
		}

		public static bool operator == (Combination lhs, Combination rhs)
		{
			if (object.ReferenceEquals(lhs, null))
			{
				return object.ReferenceEquals(rhs, null);
			}
			return lhs.Equals(rhs);
		}

		public static bool operator != (Combination lhs, Combination rhs)
		{
			if (object.ReferenceEquals(lhs, null))
			{
				return !object.ReferenceEquals(rhs, null);
			}

			return !lhs.Equals(rhs);
		}
		#endregion
	}
}