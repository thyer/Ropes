using System;
using System.Collections;

namespace Ropes.Implementations
{
	internal class ConcatenationRope : AbstractRope
	{
		private Rope rope1;
		private Rope rope2;

		public ConcatenationRope(Rope rope1, Rope rope2)
		{
			this.rope1 = rope1;
			this.rope2 = rope2;
		}

		public override char CharAt(int index)
		{
			throw new NotImplementedException();
		}

		public override byte Depth()
		{
			throw new NotImplementedException();
		}

		public override IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}

		public override int IndexOf(char ch)
		{
			throw new NotImplementedException();
		}

		public override int IndexOf(char ch, int fromIndex)
		{
			throw new NotImplementedException();
		}

		public override int IndexOf(string sequence, int fromIndex)
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

		internal Rope GetLeft()
		{
			throw new NotImplementedException();
		}

		internal Rope GetRight()
		{
			throw new NotImplementedException();
		}
	}
}