using Ropes.Implementations;
using System;

public sealed class RopeBuilder
{

	/// <summary>
	/// Makes a rope from a string
	/// </summary>
	/// <param name="sequence">the string to make a rope</param>
	/// <returns>a constructed Rope</returns>
	static public Rope BUILD(String sequence)
	{
		return new FlatCharArrayRope(sequence.ToCharArray());
	}

	internal static Rope BUILD(RepeatedCharacterSequence repeatedCharacterSequence)
	{
		throw new NotImplementedException();
	}
}
