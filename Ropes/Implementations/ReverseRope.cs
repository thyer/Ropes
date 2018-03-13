using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ropes.Implementations
{
	internal class ReverseRope : AbstractRope
	{
		private FlatCharArrayRope flatCharArrayRope;

		public ReverseRope(FlatCharArrayRope flatCharArrayRope)
		{
			this.flatCharArrayRope = flatCharArrayRope;
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

		public override IEnumerator<char> GetEnumerator(int offset)
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
	}
}