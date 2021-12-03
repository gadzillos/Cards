using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LectureHW_3
{
    public static class Extensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> deck)
        {
            var shuffledDeck = deck.OrderBy(a => Guid.NewGuid());
            var deckIter = shuffledDeck.GetEnumerator();
            while (deckIter.MoveNext())
            {
                yield return deckIter.Current;
            }
        }

        public static List<T> Shuffle<T>(this List<T> deck)
        {
            var shuffledDeck = deck.OrderBy(a => Guid.NewGuid()).ToList();
            return shuffledDeck;
        }
    }
}
