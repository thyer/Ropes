using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ropes.Implementations
{
	public sealed class FlatCharArrayRope : AbstractRope
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

		public char CharAt(int index)
		{
			return this.sequence[index];
		}

		public override byte Depth()
		{
			return 0;
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

		public override int IndexOf(char ch, int fromIndex)
		{
			throw new NotImplementedException();
		}
	}
}
