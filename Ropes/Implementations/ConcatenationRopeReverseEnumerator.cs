using System;
using System.Collections;
using System.Collections.Generic;

namespace Ropes.Implementations
{
	internal class ConcatenationRopeReverseEnumerator : IEnumerator<char>
	{
		private ConcatenationRope concatenationRope;
		private int start;

		public ConcatenationRopeReverseEnumerator(ConcatenationRope concatenationRope)
		{
			this.concatenationRope = concatenationRope;
		}

		public ConcatenationRopeReverseEnumerator(ConcatenationRope concatenationRope, int start) : this(concatenationRope)
		{
			this.start = start;
		}

		public object Current
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		char IEnumerator<char>.Current
		{
			get
			{
				throw new NotImplementedException();
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