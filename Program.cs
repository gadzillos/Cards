using System;
using System.Collections.Generic;
using System.Linq;

namespace LectureHW_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            // Part 1

            var ranks = Ranks();
            ranks = ranks.Prepend(ranks.Last()).SkipLast(1);
            var startingDeck = Suits().SelectMany(suit => ranks.Select(rank => new Card((CardRanks)Enum.Parse(typeof(CardRanks), rank), (CardSuits)Enum.Parse(typeof(CardSuits), suit)))).ToList();

            Console.WriteLine(String.Join(',', startingDeck));
            startingDeck = startingDeck.Shuffle();
            Console.WriteLine(Environment.NewLine + String.Join(',', startingDeck.Take(6)));
            Console.WriteLine(Environment.NewLine + String.Join(',', startingDeck.Skip(6).Take(6)));

            // Part 2

            List<string> someCards = new List<string>{ "<Ace><spades>", "<King><Beer>", "<Leeeroy><Jenkins>", "<King><hearts>", "<Ace><spades>", null };
            Card parsedCard = new Card(CardRanks.Two, CardSuits.clubs); // training wheels

            var validCards = someCards.Where(s => Card.TryParse(s, out parsedCard)).Distinct().Select(c => new Card(parsedCard.Rank, parsedCard.Suit)).ToList();
            Console.WriteLine(Environment.NewLine + String.Join(',', validCards));

            // Part 3
            var unsortedCards = startingDeck.Shuffle();

            var heartsDeck = unsortedCards.Where(c => c.Suit == CardSuits.hearts).OrderBy(c => GetValue(c.Rank, false));
            var spadesDeck = unsortedCards.Where(c => c.Suit == CardSuits.spades).OrderBy(c => GetValue(c.Rank, true));
            Console.WriteLine(Environment.NewLine + String.Join(',', heartsDeck));
            Console.WriteLine(Environment.NewLine + String.Join(',', spadesDeck));
            int GetValue(CardRanks rank, bool isAceSuperior)
            {
                if (rank == CardRanks.Ace) 
                {
                    if (isAceSuperior) { return (int)Enum.GetValues(typeof(CardRanks)).Cast<CardRanks>().Last(); } // return (int)CardRanks.Ace - if someone changes enum this may not work
                    else { return 1; }
                }
                else { return (int)rank; } // return (int)rank
            }

            // part 4
            var firstSeries = new List<Card>
            {
                new Card(CardRanks.Nine, CardSuits.hearts),
                new Card(CardRanks.Ten, CardSuits.clubs),
                new Card(CardRanks.Jack, CardSuits.diamonds),
                new Card(CardRanks.Queen, CardSuits.spades)
            };
            var secondSeries = new List<Card>
            {
                new Card(CardRanks.King, CardSuits.hearts),
                new Card(CardRanks.Ace, CardSuits.spades),
            };
            var emptySeries = new List<Card>();
            try
            {
                var series = StackSeries(firstSeries, secondSeries);
                Console.WriteLine(Environment.NewLine + String.Join(',', series));
            }
            catch { }

            IList<Card> StackSeries(IList<Card> first, IList<Card> second)
            {
                if ((first.Count() == 0) | (second.Count() == 0))
                {
                    Console.WriteLine("Empty collection");
                    throw new InvalidOperationException();
                }
                if ((GetValue(first.Last().Rank, false) == GetValue(second.First().Rank, false) - 1) & (isRed(first.Last().Suit) != isRed(second.First().Suit)))
                {
                    var stackedSeries = first.Union(second).ToList(); ;
                    return stackedSeries;
                }
                else
                {
                    Console.WriteLine("Unstackable series");
                    throw new InvalidOperationException("Unstackable series"); 
                }
                bool isRed(CardSuits suit)
                {
                    if ((suit == CardSuits.diamonds) | (suit == CardSuits.hearts))
                    {
                        return true;
                    }
                    else { return false; }
                }
            }

            // part 6  Самому больно такой код писать, но дедлайн
            var trainingDeck = startingDeck.Shuffle();
            var extremums = trainingDeck.OrderBy(c => (GetValue(c.Rank, false)));
            var clubs = extremums.Where(c => c.Suit == CardSuits.clubs);
            var diamonds = extremums.Where(c => c.Suit == CardSuits.diamonds);
            var hearts = extremums.Where(c => c.Suit == CardSuits.hearts);
            var spades = extremums.Where(c => c.Suit == CardSuits.spades);
            clubs = new List<Card> {clubs.First(), clubs.Last() };
            clubs = clubs.Distinct();
            diamonds = new List<Card> { diamonds.First(), diamonds.Last() };
            diamonds = diamonds.Distinct();
            hearts = new List<Card> { hearts.First(), hearts.Last() };
            hearts = hearts.Distinct();
            spades = new List<Card> { spades.First(), spades.Last() };
            spades = spades.Distinct();
            Console.WriteLine(Environment.NewLine + String.Join(',', clubs) + String.Join(',', hearts) + String.Join(',', diamonds) + String.Join(',', spades));

            static IEnumerable<string> Suits()
            {
                yield return "clubs";
                yield return "diamonds";
                yield return "hearts";
                yield return "spades";
            }

            static IEnumerable<string> Ranks()
            {
                yield return "Two";
                yield return "Three";
                yield return "Four";
                yield return "Five";
                yield return "Six";
                yield return "Seven";
                yield return "Eight";
                yield return "Nine";
                yield return "Ten";
                yield return "Jack";
                yield return "Queen";
                yield return "King";
                yield return "Ace";
            }
        }
    }
}
