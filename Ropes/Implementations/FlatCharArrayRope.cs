using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ropes.Implementations
{
	internal sealed class FlatCharArrayRope : AbstractRope
	{
		private readonly char[] sequence;

		public FlatCharArrayRope(char[] sequence) : this(sequence, 0, sequence.Length)
		{

		}

		public FlatCharArrayRope(char[] sequence, int offset, int length)
		{
			if(length > sequence.Length)
			{
				throw new ArgumentException("Length must be less than sequence length");
			}
			this.sequence = new char[length];
			System.Array.Copy(sequence, offset, this.sequence, 0, length);
		}

		public FlatCharArrayRope(string sequence1, string sequence2) : this(sequence1.ToCharArray(), sequence2.ToCharArray())
		{

		}

		public FlatCharArrayRope(char[] sequence1, char[] sequence2)
		{
			this.sequence = new char[sequence1.Length + sequence2.Length];
			sequence1.CopyTo(this.sequence, 0);
			sequence2.CopyTo(this.sequence, sequence1.Length);
		}

		public override char CharAt(int index)
		{
			return this.sequence[index];
		}

		public override byte Depth()
		{
			return 0;
		}

		public override int GetHashCode()
		{
			return sequence.GetHashCode();
		}

		// Note: this is a reproduction of the AbstractRope::IndexOf implementation. Calls
		// to CharAt have been replaced with direct array access to optimize speed
		public override int IndexOf(char ch)
		{
			for(int i = 0; i < this.sequence.Length; ++i)
			{
				if(this.sequence[i] == ch)
				{
					return i;
				}
			}

			return -1;
		}

		// Note: this is a reproduction of the AbstractRope::IndexOf implementation. Calls
		// to CharAt have been replaced with direct array access to optimize speed
		public override int IndexOf(char ch, int fromIndex)
		{
			if (fromIndex < 0 || fromIndex >= this.Length())
				throw new IndexOutOfRangeException("Rope index out of range: " + fromIndex);
			for (int j = fromIndex; j < this.sequence.Length; ++j)
				if (this.sequence[j] == ch)
					return j;
			return -1;
		}

		// Note: this is a reproduction of the AbstractRope::IndexOf implementation. Calls
		// to CharAt have been replaced with direct array access to optimize speed
		public override int IndexOf(String sequence, int fromIndex)
		{
			// Implementation of Boyer-Moore-Horspool algorith with special unicode support

			// Basic case
			int length = sequence.Length;
			if (length == 0)
				return -1;
			else if (length == 1)
				return this.IndexOf(sequence[0], fromIndex);

			int[] bcs = new int[256]; // bad character shift

			// Preprocess the sequence text
			for (int i = 0; i < length - 1; ++i)
			{
				char c = sequence[i];
				int l = (c & 0xFF);
				bcs[l] = Math.Min(length - i - 1, bcs[l]);
			}

			// Search this rope for the sequence
			for (int i = fromIndex + length - 1; i < this.Length();)
			{
				int x = i;
				int y = length - 1;
				while (true)
				{
					if (sequence[y] != this.sequence[x])
					{
						i += bcs[(this.sequence[x] & 0xFF)];
						break;
					}
					if (y == 0)
					{
						return x;
					}

					--x;
					--y;
				}
			}

			return -1;
		}

		public override int Length()
		{
			return this.sequence.Length;
		}

		public override Rope Reverse()
		{
			return new ReverseRope(this);
		}

		public override Rope SubSequence (int start, int end)
		{
			if (start == 0 && end == this.Length())
				return this;
			else if (end - start < 16)
				return new FlatCharArrayRope(this.sequence, start, end - start);
			else
				return new SubStringRope(this, start, end - start);
		}

		public override string ToString()
		{
			return new string(sequence);
		}

		public String ToString(int offset, int length)
		{
			return new string(this.sequence, offset, length);
		}

		public override IEnumerator GetEnumerator()
		{
			return this.sequence.GetEnumerator();
		}

		public override IEnumerator ReverseEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}
