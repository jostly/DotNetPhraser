namespace Phraser.Models
{
	public class Phrase
	{
		public string Adjective { get; set; }
		public string Animal { get; set; }

		public static Phrase Create(string adjective, string animal)
		{
			return new Phrase
			{
				Adjective = adjective,
				Animal = animal
			};
		}

		public override string ToString()
		{
			return Adjective + " " + Animal;
		}
	}
}
