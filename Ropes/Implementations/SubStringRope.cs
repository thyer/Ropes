﻿using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Ropes.Implementations
{
	internal class SubStringRope : AbstractRope
	{
		private FlatCharArrayRope flatCharArrayRope;
		private int start;
		private int v;

		public SubStringRope(FlatCharArrayRope flatCharArrayRope, int start, int v)
		{
			this.flatCharArrayRope = flatCharArrayRope;
			this.start = start;
			this.v = v;
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

		internal Rope GetRope()
		{
			throw new NotImplementedException();
		}
	}
}