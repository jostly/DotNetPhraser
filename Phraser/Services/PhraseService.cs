using System;
using Phraser.Models;

namespace Phraser.Services
{
	public class PhraseService : IPhraseService
    {
		private readonly WordList adjectives, animals;

        public PhraseService(WordList adjectives, WordList animals)
        {
			this.adjectives = adjectives;
			this.animals = animals;
        }

		public Phrase AlliterativePhrase()
		{
			var adjective = adjectives.RandomWord();
			var animal = animals.WordStartingWith(adjective.Substring(0, 1));
			return Phrase.Create(adjective, animal);
		}

		public Phrase PhraseStartingWith(string startWith)
		{
			var adjective = adjectives.WordStartingWith(startWith);
			var animal = animals.WordStartingWith(startWith);
			return Phrase.Create(adjective, animal);
		}
        
		public Phrase RandomPhrase()
		{
			var adjective = adjectives.RandomWord();
			var animal = animals.RandomWord();
			return Phrase.Create(adjective, animal);
		}
	}
}
