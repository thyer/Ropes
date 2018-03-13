using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ropes.Implementations
{
	internal class SubStringRope : AbstractRope
	{
		private FlatRope rope;
		private int offset;
		private int length;

		public SubStringRope(FlatRope flatRope, int offset, int length)
		{
			if(length < 0 || offset < 0 || offset + length > rope.Length())
			{
				throw new IndexOutOfRangeException("Invalid substring offset (" + offset + ") and length (" + length + ")");
			}
			this.rope = flatRope;
			this.offset = offset;
			this.length = length;
		}

		public override char CharAt(int index)
		{
			if(index >= this.Length())
			{
				throw new IndexOutOfRangeException("Rope index out of range: " + index);
			}

			return rope.CharAt(this.offset + index);
		}

		public override byte Depth()
		{
			return RopeUtilities.INSTANCE.Depth(GetRope());
		}

		public int GetOffset()
		{
			return this.offset;
		}

		internal Rope GetRope()
		{
			return this.rope;
		}

		public override IEnumerator GetEnumerator()
		{
			return GetEnumerator(0);
		}

		public override IEnumerator<char> GetEnumerator(int start)
		{
			return new SubStringEnumerator(this.GetRope().GetEnumerator(offset + start));
		}

		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}

		public override int IndexOf(char ch)
		{
			throw new NotImplementedException();
		}

		public override int Length()
		{
			throw new NotImplementedException();
		}

		public override Rope Reverse()
		{
			throw new NotImplementedException();
		}

		public override IEnumerator ReverseEnumerator()
		{
			throw new NotImplementedException();
		}

		public override Rope SubSequence(int start, int end)
		{
			throw new NotImplementedException();
		}

		public override string ToString()
		{
			throw new NotImplementedException();
		}

		internal class SubStringEnumerator : IEnumerator<char>
		{
			private IEnumerator<char> enumerator;
			private int length;

			internal  SubStringEnumerator(IEnumerator<char> enumerator, int length)
			{
				this.enumerator = enumerator;
			}

			public char Current
			{
				get
				{
					return enumerator.Current;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return enumerator.Current;
				}
			}

			public void Dispose()
			{
				throw new NotImplementedException();
			}

			public bool MoveNext()
			{
				throw new NotImplementedException();
			}

			public void Reset()
			{
				throw new NotImplementedException();
			}
		}
	}
}