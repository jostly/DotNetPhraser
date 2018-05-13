using System;
using Phraser.Models;

namespace Phraser.Services
{
    public interface IPhraseService
    {
		Phrase RandomPhrase();
		Phrase AlliterativePhrase();
		Phrase PhraseStartingWith(string startWith);
    }
}
