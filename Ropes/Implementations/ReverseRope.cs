using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ropes.Implementations
{
	internal class ReverseRope : AbstractRope
	{
		private AbstractRope internalRope;

		public ReverseRope(AbstractRope rope)
		{
			this.internalRope = rope;
		}

		public override char CharAt(int index)
		{
			return internalRope.CharAt(internalRope.Length() - 1 - index);
		}

		public override byte Depth()
		{
			return internalRope.Depth();
		}

		public override IEnumerator GetEnumerator()
		{
			return internalRope.GetReverseEnumerator(0);
		}

		public override IEnumerator<char> GetEnumerator(int offset)
		{
			return internalRope.GetReverseEnumerator(offset);
		}

		public override int GetHashCode()
		{
			int output = internalRope.GetHashCode();
			output = 23 * output + 19;
			return output;
		}

		public override int IndexOf(char ch)
		{
			int index = -1;
			IEnumerator<char> reverseEnumerator = (IEnumerator<char>)GetEnumerator();
			while (reverseEnumerator.MoveNext())
			{
				++index;
				if(reverseEnumerator.Current == ch)
					return index;
			}

			return -1;
		}

		public override int Length()
		{
			return internalRope.Length();
		}

		public override Rope Reverse()
		{
			return internalRope;
		}

		public override IEnumerator<char> GetReverseEnumerator(int start)
		{
			return internalRope.GetEnumerator(start);
		}

		public override Rope SubSequence(int start, int end)
		{
			int length = internalRope.Length();
			return internalRope.SubSequence(length - end, length - start).Reverse();
		}

		public override string ToString()
		{
			char[] output = new char[Length()];
			int i = 0;
			IEnumerator<char> reverseEnumerator = (IEnumerator<char>)GetEnumerator();
			while (reverseEnumerator.MoveNext())
			{
				output[i] = reverseEnumerator.Current;
				i++;
			}

			return new string(output);
		}
	}
}