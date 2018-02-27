using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ropes.Implementations
{
	/// <summary>
	/// Internal definition of a Deque for ropes
	/// </summary>
	internal class RopeDeque
	{
		private LinkedList<Rope> internalList;
		public RopeDeque()
		{
			internalList = new LinkedList<Rope>();
		}

		public int Count
		{
			get
			{
				return internalList.Count;
			}
		}

		internal void Add(Rope r)
		{
			internalList.AddLast(r);
		}

		internal Rope Pop()
		{
			Rope output = internalList.ElementAt(0);
			internalList.RemoveFirst();
			return output;
		}

		internal bool Empty()
		{
			return internalList.Count == 0;
		}
	}
}
