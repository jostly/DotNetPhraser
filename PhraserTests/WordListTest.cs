using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Xunit;
using Phraser.Services;

namespace PhraserTests
{
	public class WordListTest
	{
		[Theory]
		[InlineData("foo\nbar\nbaz", new string[] { "foo", "bar", "baz" })]
		[InlineData("A\nfoo\nbar", new string[] { "foo", "bar" })]
		[InlineData("foo\n\nbar", new string[] { "foo", "bar" })]
		[InlineData("   \nfoo\nbar", new string[] { "foo", "bar" })]
        public void ParseInputData(string source, string[] expected)
        {
			var reader = new StringReader(source);
			var wordList = new WordList(reader);
			Assert.Equal(expected.ToList(), wordList.Words);
        }
    }
}
