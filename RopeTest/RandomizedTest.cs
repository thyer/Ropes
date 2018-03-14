using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Linq;

namespace RopeTest
{
	[TestClass]
	public class RandomizedTest
	{
		private static int seed = 342342;
		private static Random rand = new Random(RandomizedTest.seed);
		private static int lenCC = 159486;
		private static StreamWriter log = new StreamWriter("log.txt");

		[TestMethod]
		public void TestMethod1()
		{
			// Reads in A Christmas Carol and randomly performs 100 actions on that string
			char[] output = ReadChristmasCarol();
			Rope ropeCC = RopeBuilder.BUILD(output);

			for(int i = 0; i < 100; ++i)
			{
				Array values = Enum.GetValues(typeof(Action));
				switch ((Action) values.GetValue(rand.Next(values.Length)))
				{
					case Action.Append:
						ropeCC = Append(ropeCC);
						break;
					case Action.Delete:
						ropeCC = Delete(ropeCC);
						break;
					case Action.Enumerate:
						Enumerate(ropeCC);
						break;
					case Action.Reverse:
						ropeCC = Reverse(ropeCC);
						break;
					case Action.IndexOf:
						IndexOf(ropeCC);
						break;
					case Action.Insert:
						ropeCC = Insert(ropeCC);
						break;
					case Action.TrimStart:
					case Action.TrimEnd:
						ropeCC = Trim(ropeCC);
						break;
					case Action.Subsequence:
						ropeCC = Subsequence(ropeCC);
						break;
					case Action.PadStart:
					case Action.PadEnd:
						ropeCC = Pad(ropeCC);
						break;
					case Action.StartsWith:
					case Action.EndsWith:
						StartsEndsWith(ropeCC);
						break;

				}
			}
			log.Flush();
		}

		private void StartsEndsWith(Rope ropeCC)
		{
			log.WriteLine(GetCurrentMethod());
			log.WriteLine("Starts with a? " + ropeCC.StartsWith("a"));
			log.WriteLine("Ends with a? " + ropeCC.EndsWith("a"));
		}

		private Rope Pad(Rope ropeCC)
		{
			log.WriteLine(GetCurrentMethod());
			return ropeCC.PadStart(rand.Next(5)).PadEnd(rand.Next(5));
		}

		private Rope Subsequence(Rope ropeCC)
		{
			log.WriteLine(GetCurrentMethod());
			int length = ropeCC.Length();
			int start = rand.Next(length / 2);
			int end = Math.Max(start + 1, length - rand.Next(length / 3));
			log.WriteLine(String.Format("Length:{0}, Start:{1}, End:{2}", length, start, end));
			return ropeCC.SubSequence(start, end);
		}

		private Rope Trim(Rope ropeCC)
		{
			log.WriteLine(GetCurrentMethod());
			return ropeCC.Trim();
		}

		private Rope Insert(Rope ropeCC)
		{
			log.WriteLine(GetCurrentMethod());
			int length = ropeCC.Length();
			int dstOffset = rand.Next(length - 1);
			log.WriteLine(String.Format("Length:{0}, dstOffset:{1}", length, dstOffset));
			return ropeCC.Insert(dstOffset, GetRandomString());
		}

		private void IndexOf(Rope ropeCC)
		{
			log.WriteLine(GetCurrentMethod());

			int fromIndex = rand.Next(ropeCC.Length() / 4);
			if (rand.Next() % 2 == 0) // char
			{
				log.WriteLine(String.Format("Index of next \'e\' from index {0}: {1}", fromIndex, ropeCC.IndexOf('e', fromIndex)));
			}
			else // string
			{
				log.WriteLine(String.Format("Index of next occurrence of \"you\" from index {0}: {1}", fromIndex, ropeCC.IndexOf("you", fromIndex)));
			}
		}

		private Rope Reverse(Rope ropeCC)
		{
			log.WriteLine(GetCurrentMethod());
			// TODO: implement reverse
			return ropeCC;
		}

		private void Enumerate(Rope ropeCC)
		{
			log.WriteLine(GetCurrentMethod());
			int cH = 0;
			foreach(char c in ropeCC)
			{
				if (c.Equals('h')){
					cH++;
				}
			}

			log.WriteLine("Counted all \'h\'s in the text and found: " + cH);
		}

		private Rope Delete(Rope ropeCC)
		{
			log.WriteLine(GetCurrentMethod());

			int length = ropeCC.Length();
			int start = rand.Next(length / 2);
			int end = Math.Max(start + 1, length - rand.Next(length / 3));
			log.WriteLine(String.Format("Length:{0}, Start:{1}, End:{2}", length, start, end));
			return ropeCC;
		}

		private Rope Append(Rope ropeCC)
		{
			log.WriteLine(GetCurrentMethod());

			if(rand.Next() % 2 == 0) // append a new rope
			{
				return ropeCC.Append(RopeBuilder.BUILD(GetRandomString()));
			}
			return ropeCC.Append(GetRandomString());
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private string GetCurrentMethod()
		{
			StackTrace st = new StackTrace();
			StackFrame sf = st.GetFrame(1);

			return "Method: " + sf.GetMethod().Name;
		}

		/// <summary>
		/// Returns a random alphanumeric string between 1 and 10 characters
		/// </summary>
		/// <returns>a random string</returns>
		private string GetRandomString()
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, rand.Next(10))
			  .Select(s => s[rand.Next(s.Length)]).ToArray());
		}

		private static char[] ReadChristmasCarol()
		{
			char[] output = new char[lenCC];
			StreamReader reader = new StreamReader("TestRepo/AChristmasCarol.txt");
			reader.Read(output, 0, lenCC);

			return output;
		}

		internal enum Action : int
		{
			Append = 1,
			Delete = 2,
			Enumerate = 3,
			Reverse = 4,
			IndexOf = 5,
			Insert = 6,
			TrimStart = 7,
			TrimEnd = 8,
			Subsequence = 9,
			PadStart = 10,
			PadEnd = 11,
			StartsWith = 12,
			EndsWith = 13
		}
	}
}
