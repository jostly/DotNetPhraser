using System;
using System.IO;
using System.Collections.Generic;
namespace Phraser.Services
{
	public class WordList
	{
		public IReadOnlyList<string> Words => words;

		List<string> words = new List<string>();

		public WordList(TextReader reader)
		{
			words.AddRange(ReadWords(reader));
		}

		public string RandomWord()
		{
			return GetRandom(words);
		}

		public string WordStartingWith(string startWith)
		{
			return GetRandom(words.FindAll(s => s.StartsWith(startWith, StringComparison.Ordinal)));
		}

		T GetRandom<T>(List<T> list)
		{
			var i = new Random().Next(list.Count);
			return list[i];
		}

		static IEnumerable<string> ReadWords(TextReader reader)
		{
			string line;
			while ((line = reader.ReadLine()) != null)
			{
				var trimmed = line.Trim();
				if (trimmed.Length > 1)
				{
					yield return trimmed;
				}
			}
		}
	}
}
