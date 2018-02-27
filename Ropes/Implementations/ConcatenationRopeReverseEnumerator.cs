using System;
using System.Collections;

namespace Ropes.Implementations
{
	internal class ConcatenationRopeReverseEnumerator : IEnumerator
	{
		private ConcatenationRope concatenationRope;

		public ConcatenationRopeReverseEnumerator(ConcatenationRope concatenationRope)
		{
			this.concatenationRope = concatenationRope;
		}

		public object Current
		{
			get
			{
				throw new NotImplementedException();
			}
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