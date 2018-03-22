using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ropes.Implementations
{
	internal abstract class AbstractRope : Rope
	{
		protected int hashCode = 0;

		public Rope Append(string suffix)
		{
			return RopeUtilities.INSTANCE.Concatenate(this, RopeBuilder.BUILD(suffix));
		}

		public Rope Append(char c)
		{
			return RopeUtilities.INSTANCE.Concatenate(this, RopeBuilder.BUILD(c.ToString()));
		}

		public Rope Append(string suffix, int start, int end)
		{
			return RopeUtilities.INSTANCE.Concatenate(this, RopeBuilder.BUILD(suffix).SubSequence(start, end));
		}

		public Rope Append(Rope rope)
		{
			return RopeUtilities.INSTANCE.Concatenate(this, rope);
		}

		public int CompareTo(object obj)
		{
			if (!(obj is string))
				return -1;
			string sequence = (string)obj;
			int compareUntil = Math.Min(sequence.Length, this.Length());
			int i = 0;
			foreach(char x in this)
			{
				char y = sequence[i];
				if (x != y)
					return x - y;
				i++;
			}

			return this.Length() - sequence.Length;
		}

		public Rope Delete(int start, int end)
		{
			if (start == end)
			{
				return this;
			}
			else if(start > end)
			{
				throw new ArgumentException(System.String.Format("start ({0}) less than end ({1})", start.ToString(), end.ToString()));
			}
			return this.SubSequence(0, start).Append(this.SubSequence(end, this.Length()).ToString());
		}

		/// <summary>
		/// The current depth of the rope
		/// </summary>
		/// <returns>the rope's depth</returns>
		public abstract byte Depth();

		public override bool Equals(Object obj)
		{
			if(obj is Rope)
			{
				Rope rope = (Rope)obj;
				if (rope.GetHashCode() != this.GetHashCode() || rope.Length() != this.Length())
					return false;

				return this.CompareTo(rope) == 0;
			}
			return false;
		}

		public bool EndsWith(string suffix)
		{
			return EndsWith(suffix, 0);
		}

		public bool EndsWith(string suffix, int offset)
		{
			return StartsWith(suffix, Length() - suffix.Length - offset);
		}

		public bool Empty()
		{
			return Length() == 0;
		}

		public abstract IEnumerator GetEnumerator();

		public abstract IEnumerator<char> GetEnumerator(int start);

		public override abstract int GetHashCode();

		public int IndexOf(string sequence)
		{
			return this.IndexOf(sequence, 0);
		}

		public virtual int IndexOf(char ch)
		{
			int index = -1;
			foreach(char c in this)
			{
				++index;
				if (c == ch)
					return index;
			}

			return -1;
		}

		public virtual int IndexOf(string sequence, int fromIndex)
		{
			// Implementation of Boyer-Moore-Horspool algorith with special unicode support

			// Basic case
			int length = sequence.Length;
			if (length == 0)
				return -1;
			else if (length == 1)
				return this.IndexOf(sequence[0], fromIndex);

			int[] bcs = new int[256]; // bad character shift
			for (int i = 0; i < bcs.Length; ++i)
			{
				bcs[i] = length;
			}

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
					if (sequence[y] != this.CharAt(x))
					{
						i += bcs[(this.CharAt(x) & 0xFF)];
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

		public virtual int IndexOf(char ch, int fromIndex)
		{
			if (fromIndex < 0 || fromIndex >= this.Length())
				throw new IndexOutOfRangeException("Rope index out of range: " + fromIndex);
			int i = fromIndex;
			foreach (char c in this)
			{
				if(fromIndex > 0)
				{
					fromIndex--;
					continue;
				}
				if(ch == c)
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		public Rope Insert(int dstOffset, string s)
		{
			Rope r = (s == null) ? RopeBuilder.BUILD("null") : RopeBuilder.BUILD(s);
			if (dstOffset == 0)
				return r.Append(this);
			else if (dstOffset == this.Length())
				return this.Append(r);
			else if (dstOffset < 0 || dstOffset > this.Length())
				throw new IndexOutOfRangeException(dstOffset + " is out of range [" + 0 + ":" + this.Length() + "]");
			return this.SubSequence(0, dstOffset).Append(r).Append(this.SubSequence(dstOffset, this.Length()));
		}

		public abstract int Length();

		public Rope PadEnd(int toLength)
		{
			return PadEnd(toLength, ' ');
		}

		public Rope PadEnd(int toLength, char padChar)
		{
			int toPad = toLength - this.Length();
			if (toPad < 1)
				return this;
			return RopeUtilities.INSTANCE.Concatenate(this, RopeBuilder.BUILD(new RepeatedCharacterSequence(padChar, toPad)));
		}

		public Rope PadStart(int toLength)
		{
			return PadStart(toLength, ' ');
		}

		public Rope PadStart(int toLength, char padChar)
		{
			int toPad = toLength - this.Length();
			if (toPad < 1)
				return this;
			return RopeUtilities.INSTANCE.Concatenate(RopeBuilder.BUILD(new RepeatedCharacterSequence(padChar, toPad)), this);
		}

		public Rope Rebalance()
		{
			return this;
		}

		public abstract Rope Reverse();

		public abstract IEnumerator<char> GetReverseEnumerator(int start);

		public bool StartsWith(string prefix)
		{
			return StartsWith(prefix, 0);
		}

		public bool StartsWith(string prefix, int offset)
		{
			if (offset < 0 || offset > this.Length())
			{
				throw new IndexOutOfRangeException("Index out of range: " + offset);
			}
			else if (offset + prefix.Length > this.Length())
			{
				return false;
			}

			int x = 0;
			foreach (char c in this)
			{
				if (x >= prefix.Length || c != prefix[x])
					return false;

				x++;
			}

			return true;
		}


		CharSequence CharSequence.SubSequence(int start, int end)
		{
			return SubSequence(start, end);
		}

		public abstract Rope SubSequence(int start, int end);

		public Rope Trim()
		{
			return this.TrimStart().TrimEnd();
		}

		public Rope TrimEnd()
		{
			int index = this.Length() + 1;
			foreach (char c in this)
			{
				--index;
				if (c > 0x20 && !char.IsWhiteSpace(c))
					break;
			}
			if (index > this.Length())
				return this;
			else
				return this.SubSequence(0, index);
		}

		public Rope TrimStart()
		{
			int index = -1;
			foreach(char c in this)
			{
				++index;
				if (c > 0x20 && !char.IsWhiteSpace(c))
					break;
			}
			if (index <= 0)
				return this;
			else
				return this.SubSequence(index, this.Length());
		}

		public abstract override string ToString();

		public abstract char CharAt(int index);
	}
}
