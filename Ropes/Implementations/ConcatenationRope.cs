using System;
using System.Collections;

namespace Ropes.Implementations
{
	internal class ConcatenationRope : AbstractRope
	{
		private Rope left;
		private Rope right;
		private byte depth;
		private int length;

		public ConcatenationRope(Rope left, Rope right)
		{
			this.left = left;
			this.right = right;
			this.depth = (byte) (Math.Max(RopeUtilities.INSTANCE.Depth(left), RopeUtilities.INSTANCE.Depth(right)) + 1);
			this.length = left.Length() + right.Length();
		}

		public override char CharAt(int index)
		{
			if (index >= this.Length())
				throw new IndexOutOfRangeException("Rope index out of range: " + index);

			if(index < this.left.Length())
			{
				return this.left.CharAt(index);
			}
			else
			{
				return this.right.CharAt(index - this.left.Length());
			}
		}

		public override byte Depth()
		{
			return this.depth;
		}

		public override IEnumerator GetEnumerator()
		{
			return new ConcatenationRopeEnumerator(this);
		}

		public override int GetHashCode()
		{
			int output = 19;
			output = output * 23 + left.GetHashCode();
			output = output * 23 + right.GetHashCode();
			output = output * 23 + depth;
			output = output * 23 + length;

			return output;
		}

		public override int Length()
		{
			return this.length;
		}

		public override Rope Reverse()
		{
			return RopeUtilities.INSTANCE.Concatenate(this.GetRight().Reverse(), this.GetLeft().Reverse());
		}

		public override IEnumerator ReverseEnumerator()
		{
			return new ConcatenationRopeReverseEnumerator(this);
		}

		public override Rope SubSequence(int start, int end)
		{
			if (start < 0 || end > this.length)
				throw new ArgumentException("Illegal subsequence (" + start + "," + end + ")");
			if (start == 0 && end == this.length)
				return this;
			int l = this.left.Length();
			if (end <= l)
				return this.left.SubSequence(start, end);
			if (start >= l)
				return this.right.SubSequence(start - l, end - l);
			return RopeUtilities.INSTANCE.Concatenate(
				this.left.SubSequence(start, l),
				this.right.SubSequence(0, end - l));
		}

		public override string ToString()
		{
			return left.ToString() + right.ToString();
		}

		internal Rope GetLeft()
		{
			return left;
		}

		internal Rope GetRight()
		{
			return right;
		}
	}
}