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

		public SubStringRope(FlatRope rope, int offset, int length)
		{
			if(length < 0 || offset < 0 || offset + length > rope.Length())
			{
				throw new IndexOutOfRangeException("Invalid substring offset (" + offset + ") and length (" + length + ")");
			}
			this.rope = rope;
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
			return new SubStringEnumerator(this.GetRope().GetEnumerator(offset + start), this.length);
		}

		public override int GetHashCode()
		{
			int output = 19;
			output = 23 * output + rope.GetHashCode();
			output = 23 * output + offset;
			output = 23 * output + length;

			return output;
		}

		public override int Length()
		{
			return this.length;
		}

		public override Rope Reverse()
		{
			return new ReverseRope(this);
		}

		public override IEnumerator<char> GetReverseEnumerator(int start)
		{
			return new SubstringRopeReverseEnumerator(this.GetRope().GetReverseEnumerator(GetRope().Length() - GetOffset() - Length() + start), this.length);
		}

		public override Rope SubSequence(int start, int end)
		{
			if(start == 0 && end == this.Length())
			{
				return this;
			}

			return new SubStringRope(this.rope, this.offset + start, end - start);
		}

		public override string ToString()
		{
			return this.rope.ToString(this.offset, this.length);
		}

		internal class SubStringEnumerator : IEnumerator<char>
		{
			private IEnumerator<char> enumerator;
			private int length;
			private int cSteps;

			internal  SubStringEnumerator(IEnumerator<char> enumerator, int length)
			{
				this.enumerator = enumerator;
				this.length = length;
				this.cSteps = 0;
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
				enumerator.Dispose();
			}

			public bool MoveNext()
			{
				if(cSteps < length)
				{
					cSteps++;
					return enumerator.MoveNext();
				}
				return false;
			}

			public void Reset()
			{
				cSteps = 0;
				enumerator.Reset();
			}
		}

		internal class SubstringRopeReverseEnumerator : IEnumerator<char>
		{
			private IEnumerator<char> enumerator;
			private int cSteps;
			private int length;

			public SubstringRopeReverseEnumerator(IEnumerator<char> enumerator, int length)
			{
				this.enumerator = enumerator;
				this.length = length;
				this.cSteps = 0;
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
				enumerator.Dispose();
			}

			public bool MoveNext()
			{
				if(cSteps < length)
				{
					cSteps++;
					return enumerator.MoveNext();
				}
				return false;
			}

			public void Reset()
			{
				enumerator.Reset();
			}
		}
	}
}