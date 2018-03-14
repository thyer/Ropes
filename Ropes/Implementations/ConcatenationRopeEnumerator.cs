using System;
using System.Collections;
using System.Collections.Generic;

namespace Ropes.Implementations
{
	internal class ConcatenationRopeEnumerator : IEnumerator<char>
	{
		private Rope initialRope;
		private RopeDeque toTraverse;
		private Rope currentRope;
		private int currentRopePos;
		private int initStart; // the index position where we began the enumerator
		private int currentAbsolutePos;

		public ConcatenationRopeEnumerator(ConcatenationRope concatenationRope) : this(concatenationRope, 0)
		{

		}

		public ConcatenationRopeEnumerator(ConcatenationRope concatenationRope, int start)
		{
			this.initialRope = concatenationRope;
			this.toTraverse = new RopeDeque();
			this.toTraverse.Add(concatenationRope);
			this.currentRope = null;
			this.initStart = start;
			this.Init();

			if(start < 0 || start > concatenationRope.Length())
			{
				throw new ArgumentOutOfRangeException("Rope index out of range: " + start);
			}

			this.MoveNext(start);
		}

		public bool CanMoveBackward(int amount)
		{
			return (this.currentRopePos - amount) >= -1;
		}

		public object Current
		{
			get
			{
				return currentRope.CharAt(this.currentRopePos);
			}
		}

		char IEnumerator<char>.Current
		{
			get
			{
				return currentRope.CharAt(this.currentRopePos);
			}
		}

		public int GetPos()
		{
			return this.currentAbsolutePos;
		}

		public void Init()
		{
			while (!this.toTraverse.Empty())
			{
				this.currentRope = this.toTraverse.Pop();
				if(this.currentRope is ConcatenationRope){
					this.toTraverse.Add(((ConcatenationRope)this.currentRope).GetRight());
					this.toTraverse.Add(((ConcatenationRope)this.currentRope).GetLeft());
				}
				else
				{
					break;
				}
				if (this.currentRope == null)
					throw new ArgumentNullException("No terminal ropes present");

				this.currentRopePos = -1;
				this.currentAbsolutePos = -1;
			}
		}

		public bool MoveNext()
		{
			return MoveNext(0);
		}

		public bool MoveNext(int amount)
		{
			throw new NotImplementedException();
		}

		public bool MovePrev()
		{
			return MovePrev(0);
		}

		public bool MovePrev(int amount)
		{
			if (!this.CanMoveBackward(amount))
			{
				return false;
			}

			this.currentRopePos -= amount;
			this.currentAbsolutePos -= amount;
			return true;
		}

		public void Reset()
		{
			this.toTraverse = new RopeDeque();
			this.toTraverse.Add(initialRope);
			this.currentRope = null;
			this.Init();
		}

		public void Dispose()
		{
			// nothing to dispose
		}
	}
}