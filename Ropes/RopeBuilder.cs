using Ropes.Implementations;
using System;

public sealed class RopeBuilder
{

	/// <summary>
	/// Makes a rope from a string
	/// </summary>
	/// <param name="sequence">the string to make a rope</param>
	/// <returns>a constructed Rope</returns>
	static public Rope build(String sequence)
	{
		return new FlatCharArrayRope(sequence.ToCharArray());
	}
}
