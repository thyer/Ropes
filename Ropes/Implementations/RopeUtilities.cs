using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ropes.Implementations
{
	internal class RopeUtilities
	{
		private static readonly long[] FIBONACCI = { 0L, 1L, 1L, 2L, 3L, 5L, 8L, 13L, 21L, 34L, 55L, 89L, 144L, 233L, 377L, 610L, 987L, 1597L, 2584L, 4181L, 6765L, 10946L, 17711L, 28657L, 46368L, 75025L, 121393L, 196418L, 317811L, 514229L, 832040L, 1346269L, 2178309L, 3524578L, 5702887L, 9227465L, 14930352L, 24157817L, 39088169L, 63245986L, 102334155L, 165580141L, 267914296L, 433494437L, 701408733L, 1134903170L, 1836311903L, 2971215073L, 4807526976L, 7778742049L, 12586269025L, 20365011074L, 32951280099L, 53316291173L, 86267571272L, 139583862445L, 225851433717L, 365435296162L, 591286729879L, 956722026041L, 1548008755920L, 2504730781961L, 4052739537881L, 6557470319842L, 10610209857723L, 17167680177565L, 27777890035288L, 44945570212853L, 72723460248141L, 117669030460994L, 190392490709135L, 308061521170129L, 498454011879264L, 806515533049393L, 1304969544928657L, 2111485077978050L, 3416454622906707L, 5527939700884757L, 8944394323791464L, 14472334024676221L, 23416728348467685L, 37889062373143906L, 61305790721611591L, 99194853094755497L, 160500643816367088L, 259695496911122585L, 420196140727489673L, 679891637638612258L, 1100087778366101931L, 1779979416004714189L, 2880067194370816120L, 4660046610375530309L, 7540113804746346429L};
		private static readonly short MAX_ROPE_DEPTH = 96;
		private static readonly int COMBINE_LENGTH = 17;
		private static readonly String SPACES = "                                                                                                                                                                                                        ";

		private static RopeUtilities instance;
		private RopeUtilities() { }
		public static RopeUtilities INSTANCE
		{
			get
			{
				if(instance == null)
				{
					instance = new RopeUtilities();
				}
				return instance;
			}
		}

		/// <summary>
		/// Rebalances a rope if its depth has exceeded the maximum allowed depth. Else do nothing
		/// </summary>
		/// <param name="r">the rope to rebalance</param>
		/// <returns>a rebalanced rope</returns>
		public Rope AutoRebalance(Rope r)
		{
			if (r is AbstractRope && ((AbstractRope)r).Depth() > RopeUtilities.MAX_ROPE_DEPTH)
			{
				return this.Rebalance(r);
			}
			else
			{
				return r;
			}
		}

		/// <summary>
		/// Concatenates two ropes together using the most efficient implementation
		/// </summary>
		/// <param name="left">the rope to come first in the new rope</param>
		/// <param name="right">te rope to come last in the new rope</param>
		/// <returns>the concatenated rope</returns>
		public Rope Concatenate(Rope left, Rope right)
		{
			if (left.Length() == 0)
				return right;
			if (right.Length() == 0)
				return left;

			if ((long)left.Length() + right.Length() > int.MaxValue)
				throw new ArgumentOutOfRangeException("Concatenation would overflow field length");

			if(left.Length() + right.Length() < COMBINE_LENGTH)
			{
				return new FlatCharArrayRope(left.ToString(), right.ToString());
			}
			else if (!(left is ConcatenationRope))
			{
				if (right is ConcatenationRope)
				{
					ConcatenationRope cRight = (ConcatenationRope)right;
					if (left.Length() + cRight.GetLeft().Length() < COMBINE_LENGTH)
						return this.AutoRebalance(new ConcatenationRope(new FlatCharArrayRope(left.ToString(), cRight.GetLeft().ToString()), cRight.GetRight()));
				}
			}
			else if (!(right is ConcatenationRope))
			{
				if (left is ConcatenationRope)
				{
					ConcatenationRope cLeft = (ConcatenationRope)left;
					if (right.Length() + cLeft.GetRight().Length() < COMBINE_LENGTH)
						return this.AutoRebalance(new ConcatenationRope(cLeft.GetLeft(), new FlatCharArrayRope(cLeft.GetRight().ToString(), right.ToString())));
				}
			}

			return this.AutoRebalance(new ConcatenationRope(left, right));
		}

		public byte Depth(Rope r)
		{
			if (r is AbstractRope)
			{
				return ((AbstractRope)r).Depth();
			}
			else
			{
				throw new ArgumentException("New implementation?");
			}
		}

		public bool IsBalanced(Rope r)
		{
			byte depth = this.Depth(r);
			if (depth >= FIBONACCI[depth + 2] - r.Length()) 
			{
				return false;
			}

			return (FIBONACCI[depth + 2] <= r.Length()); // TODO: not necessarily valid w/e.g. padding char sequences.
		}

		public Rope Rebalance(Rope r)
		{
			List<Rope> leafNodes = new List<Rope>();
			RopeDeque toExamine = new RopeDeque();

			// begin a depth first loop
			toExamine.Add(r);
			while (toExamine.Count > 0)
			{
				Rope rExamine = toExamine.Pop();
				if (rExamine is ConcatenationRope)
				{
					toExamine.Add(((ConcatenationRope)rExamine).GetRight());
					toExamine.Add(((ConcatenationRope)rExamine).GetLeft());
					continue;
				}
				else
				{
					leafNodes.Add(rExamine);
				}
			}
			Rope result = Merge(leafNodes, 0, leafNodes.Count);
			return result;
		}

		private Rope Merge(List<Rope> leafNodes, int start, int end)
		{
			int range = end - start;
			switch (range)
			{
				case 1:
					return leafNodes.ElementAt(start);
				case 2:
					return new ConcatenationRope(leafNodes.ElementAt(start), leafNodes.ElementAt(start + 1));
				default:
					int middle = start + (range / 2);
					return new ConcatenationRope(Merge(leafNodes, start, middle), Merge(leafNodes, middle, end));
			}
		}

		/// <summary>
		/// Visualizes a rope
		/// </summary>
		/// <param name="r">The rope to visualize</param>
		/// <param name="depth">the depth of the rope</param>
		void Visualize(Rope r, int depth)
		{
			if (r is FlatRope)
			{
				Console.Write(SPACES.Substring(0, depth * 2));
				Console.WriteLine("\"" + r + "\"");
				Console.WriteLine(r.Length());
			}
			if (r is SubStringRope)
			{
				Console.Write(SPACES.Substring(0, depth * 2));
				Console.WriteLine("substring " + r.Length() + " \"" + r + "\"");
				this.Visualize(((SubStringRope)r).GetRope(), depth + 1);
			}
			if (r is ConcatenationRope)
			{
				Console.Write(SPACES.Substring(0, depth * 2));
				Console.WriteLine("concat[left]");
				this.Visualize(((ConcatenationRope)r).GetLeft(), depth + 1);
				Console.Write(SPACES.Substring(0, depth * 2));
				Console.WriteLine("concat[right]");
				this.Visualize(((ConcatenationRope)r).GetRight(), depth + 1);
			}
		}
	}
}
