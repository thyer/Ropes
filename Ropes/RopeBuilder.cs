using Ropes;
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

	/// <summary>
	/// Makes a rope from a char array
	/// </summary>
	/// <param name="sequence">the character array to make into a rope</param>
	/// <returns>a constructed Rope</returns>
	static public Rope BUILD(char[] sequence)
	{
		return new FlatCharArrayRope(sequence);
	}

	static public Rope BUILD(CharSequence sequence)
	{
		return new FlatCharSequenceRope(sequence);
	}
}
