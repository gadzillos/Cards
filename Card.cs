using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LectureHW_3
{
    public enum CardRanks
    {
        Two = 2,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }
    public enum CardSuits
    {
        clubs,
        diamonds,
        hearts,
        spades
    }

    public class Card
    {
        public CardRanks Rank { get; set; }
        public CardSuits Suit { get; set; }

        public Card(CardRanks rank, CardSuits suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public override string ToString()
        {
            string s = "";
            if (Rank < CardRanks.Jack)
            {
                s += (int) Rank;
            }
            else
            {
                s += Rank.ToString()[0];
            }
            switch (Suit)
            {
                case CardSuits.clubs:
                    s += '♣';
                    break;
                case CardSuits.diamonds:
                    s += '♦';
                    break;
                case CardSuits.hearts:
                    s += '♥';
                    break;
                case CardSuits.spades:
                    s += '♠';
                    break;
            }
            return s;
        }

        public static bool TryParse(string? s, out Card? result)
        {
            if (s == null)
            {
                result = null;
                return false;
            }
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            s = s.Trim();
            var rankAndSuit = s.Split("><");
            rankAndSuit[0] = rankAndSuit[0].Replace("<", null);
            rankAndSuit[0] = rankAndSuit[0].Replace(">", null);
            rankAndSuit[1] = rankAndSuit[1].Replace("<", null);
            rankAndSuit[1] = rankAndSuit[1].Replace(">", null);
            rankAndSuit[0] = textInfo.ToLower(rankAndSuit[0]);
            rankAndSuit[0] = textInfo.ToTitleCase(rankAndSuit[0]);
            rankAndSuit[1] = textInfo.ToLower(rankAndSuit[1]);
            try
            {
                result = new Card((CardRanks)Enum.Parse(typeof(CardRanks), rankAndSuit[0]), (CardSuits)Enum.Parse(typeof(CardSuits), rankAndSuit[1]));
                return true;
            }
            catch { }
            result = null;
            return false;
        }
    }

    
}
