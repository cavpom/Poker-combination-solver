
public class Card
{
	public CardSuit Suit
	{
		private set; get;
	}

	public CardValue Value
	{
		private set; get;
	}

	public Card(CardSuit suit, CardValue value)
	{
		Suit = suit;
		Value = value;
	}

	public override string ToString()
	{
		return Value.ToString().Substring(1, 1) + Suit.ToString().Substring(0, 1);
	}
}

public enum CardSuit
{
	Spades,
	Clubs,
	Diamonds,
	Hearts
}
public enum CardValue
{
	_2,
	_3,
	_4,
	_5,
	_6,
	_7,
	_8,
	_9,
	_X,
	_J,
	_Q,
	_K,
	_A
}
