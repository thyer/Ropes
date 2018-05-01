using System;
using System.Collections;
using System.Collections.Generic;
using Ropes;
using Ropes.Implementations;

internal class FlatCharSequenceRope : AbstractRope, FlatRope
{
	private CharSequence sequence;

	public FlatCharSequenceRope(CharSequence sequence)
	{
		this.sequence = sequence;
	}

	public override char CharAt(int index)
	{
		return sequence.CharAt(index);
	}

	public override byte Depth()
	{
		return 0;
	}

	public override IEnumerator GetEnumerator()
	{
		return new FlatCharSequenceRopeEnumerator(this);
	}

	public override IEnumerator<char> GetEnumerator(int offset)
	{
		return new FlatCharSequenceRopeEnumerator(this, offset);
	}

	public override int GetHashCode()
	{
		int output = 23;
		output = output * 19 + sequence.GetHashCode();

		return output;
	}

	public override IEnumerator<char> GetReverseEnumerator(int offset)
	{
		return new FlatCharSequenceRopeReverseEnumerator(this, offset);
	}

	public override int Length()
	{
		return sequence.Length();
	}

	public override Rope Reverse()
	{
		return new ReverseRope(this);
	}

	public override Rope SubSequence(int start, int end)
	{
		if (start == 0 && end == this.Length())
			return this;
		if (end - start < 8)
			return new FlatCharSequenceRope(this.sequence.SubSequence(start, end));
		else
			return new SubStringRope(this, start, end - start);
	}

	public override string ToString()
	{
		return this.sequence.ToString();
	}

	public string ToString(int offset, int length)
	{
		return this.sequence.SubSequence(offset, offset + length).ToString();
	}

	private class FlatCharSequenceRopeEnumerator : IEnumerator<char>
	{
		private FlatCharSequenceRope flatCharSequenceRope;
		private int initialOffset;
		private int curPosition;

		public FlatCharSequenceRopeEnumerator(FlatCharSequenceRope flatCharSequenceRope) : this(flatCharSequenceRope, 0)
		{
			
		}

		public FlatCharSequenceRopeEnumerator(FlatCharSequenceRope flatCharSequenceRope, int offset)
		{
			this.flatCharSequenceRope = flatCharSequenceRope;
			this.curPosition = offset - 1;
			this.initialOffset = offset;
		}

		public char Current
		{
			get
			{
				return flatCharSequenceRope.CharAt(curPosition);
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return flatCharSequenceRope.CharAt(curPosition);
			}
		}

		public bool MoveNext()
		{
			if(curPosition < flatCharSequenceRope.Length() - 1)
			{
				curPosition++;
				return true;
			}
			return false;
		}

		public void Reset()
		{
			this.curPosition = initialOffset - 1;
		}

		public void Dispose()
		{
			return;
		}
	}

	private class FlatCharSequenceRopeReverseEnumerator : IEnumerator<char>
	{
		private FlatCharSequenceRope flatCharSequenceRope;
		private int initialOffset;
		private int curPosition;

		public FlatCharSequenceRopeReverseEnumerator(FlatCharSequenceRope flatCharSequenceRope) : this(flatCharSequenceRope, 0)
		{

		}

		public FlatCharSequenceRopeReverseEnumerator(FlatCharSequenceRope flatCharSequenceRope, int offset)
		{
			this.flatCharSequenceRope = flatCharSequenceRope;
			this.initialOffset = offset;
			this.curPosition = flatCharSequenceRope.Length() - offset;
		}

		public char Current
		{
			get
			{
				return flatCharSequenceRope.CharAt(curPosition);
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return flatCharSequenceRope.CharAt(curPosition);
			}
		}

		public void Dispose()
		{
			return;
		}

		public bool MoveNext()
		{
			if(curPosition == 0)
			{
				return false;
			}

			curPosition--;
			return true;
		}

		public void Reset()
		{
			this.curPosition = flatCharSequenceRope.Length() - initialOffset;
		}
	}
}