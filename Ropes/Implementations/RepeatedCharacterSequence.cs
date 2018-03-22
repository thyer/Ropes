using System;

namespace Ropes.Implementations
{
	internal class RepeatedCharacterSequence : CharSequence
	{
		private char padChar;
		private int toPad;

		public RepeatedCharacterSequence(char padChar, int toPad)
		{
			this.padChar = padChar;
			this.toPad = toPad;
		}

		public char CharAt(int index)
		{
			return padChar;
		}

		public int Length()
		{
			return toPad;
		}

		public CharSequence SubSequence(int start, int end)
		{
			return new RepeatedCharacterSequence(padChar, end - start);
		}

		public override string ToString()
		{
			return new string(padChar, toPad);
		}
	}
}