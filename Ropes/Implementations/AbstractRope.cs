using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ropes.Implementations
{
	public abstract class AbstractRope : Rope
	{
		public Rope Append(string suffix)
		{
			throw new NotImplementedException();
		}

		public Rope Append(char c)
		{
			throw new NotImplementedException();
		}

		public Rope Append(string csq, int start, int end)
		{
			throw new NotImplementedException();
		}

		public Rope Delete(int start, int end)
		{
			throw new NotImplementedException();
		}

		public int CompareTo(object obj)
		{
			throw new NotImplementedException();
		}

		public abstract byte Depth();

		public override bool Equals(Object obj)
		{
			throw new NotImplementedException();
		}

		public bool EndsWith(string suffix)
		{
			throw new NotImplementedException();
		}

		public bool EndsWith(string suffix, int offset)
		{
			throw new NotImplementedException();
		}

		public bool FEmpty()
		{
			throw new NotImplementedException();
		}

		public bool FMatches(string regex)
		{
			throw new NotImplementedException();
		}

		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		public int IndexOf(string sequence)
		{
			throw new NotImplementedException();
		}

		public abstract int IndexOf(char ch);

		public int IndexOf(string sequence, int fromIndex)
		{
			throw new NotImplementedException();
		}

		public abstract int IndexOf(char ch, int fromIndex);

		public Rope Insert(int dstOffset, string s)
		{
			throw new NotImplementedException();
		}

		public IEnumerator Iterator(int start)
		{
			throw new NotImplementedException();
		}

		public Rope PadEnd(int toLength)
		{
			throw new NotImplementedException();
		}

		public Rope PadEnd(int toLength, char padChar)
		{
			throw new NotImplementedException();
		}

		public Rope PadStart(int toLength)
		{
			throw new NotImplementedException();
		}

		public Rope PadStart(int toLength, char padChar)
		{
			throw new NotImplementedException();
		}

		public Rope Rebalance()
		{
			throw new NotImplementedException();
		}

		public Rope Reverse()
		{
			throw new NotImplementedException();
		}

		public IEnumerator ReverseEnumerator()
		{
			throw new NotImplementedException();
		}

		public IEnumerator ReverseIterator(int start)
		{
			throw new NotImplementedException();
		}

		public bool StartsWith(string prefix)
		{
			throw new NotImplementedException();
		}

		public bool StartsWith(string prefix, int offset)
		{
			throw new NotImplementedException();
		}

		public Rope SubSequence(int start, int end)
		{
			throw new NotImplementedException();
		}

		public Rope Trim()
		{
			throw new NotImplementedException();
		}

		public Rope TrimEnd()
		{
			throw new NotImplementedException();
		}

		public Rope TrimStart()
		{
			throw new NotImplementedException();
		}
	}
}
