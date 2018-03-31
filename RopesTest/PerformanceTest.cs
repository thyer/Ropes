using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;

namespace RopeTest
{
	[TestClass]
	public class PerformanceTest
	{
		private static int seed = 342342;
		private static Random rand = new Random(PerformanceTest.seed);
		private static int lenCC = 159486;

		private const int ITERATION_COUNT = 7;
		private const int PLAN_LENGTH = 500;

		private static String complexString = null;
		private static Rope complexRope = null;

		private static StreamWriter writer = new StreamWriter("output.txt");
		private static Stopwatch sw = new Stopwatch();

		[TestInitialize]
		public void Setup()
		{
			sw.Reset();
		}

		[TestMethod]
		public void ChristmasCarolPerf_Read()
		{
			char[] output = ReadChristmasCarol();
			string strCC = new string(output);

			sw.Start();
			Rope ropeCC = RopeBuilder.BUILD(strCC);
			sw.Stop();

			Report("Constructed rope from ChristmasCarol string", sw.Elapsed);
		}

		[TestMethod]
		public void ChristmasCarolPerf_DeleteFront()
		{
			string strCC = new string(ReadChristmasCarol());
			Rope ropeCC = RopeBuilder.BUILD(strCC);

			sw.Start();
			strCC = strCC.Remove(5, 550);
			sw.Stop();
			Report("Substring time for ChristmasCarol string", sw.Elapsed);

			sw.Restart();
			ropeCC = ropeCC.SubSequence(5, 555);
			sw.Stop();
			Report("Substring time for ChristmasCarol Rope", sw.Elapsed);
		}

		[TestMethod]
		public void ChristmasCarolPerf_DeleteEnd()
		{
			string strCC = new string(ReadChristmasCarol());
			Rope ropeCC = RopeBuilder.BUILD(strCC);

			sw.Start();
			strCC = strCC.Remove(lenCC - 550, 550);
			sw.Stop();
			Report("Substring time for ChristmasCarol string", sw.Elapsed);

			sw.Restart();
			ropeCC = ropeCC.SubSequence(lenCC - 550, lenCC);
			sw.Stop();
			Report("Substring time for ChristmasCarol Rope", sw.Elapsed);
		}

		[TestMethod]
		public void ChristmasCarolPerf_InsertFront()
		{
			string strCC = new string(ReadChristmasCarol());
			Rope ropeCC = RopeBuilder.BUILD(strCC);

			sw.Start();
			strCC = strCC.Insert(0, "hello!");
			sw.Stop();
			Report("Insertion time for ChristmasCarol string", sw.Elapsed);

			sw.Restart();
			ropeCC = ropeCC.Insert(0, "hello!");
			sw.Stop();
			Report("Insertion time for ChristmasCarol Rope", sw.Elapsed);
		}

		[TestMethod]
		public void ChristmasCarolPerf_InsertBack()
		{
			string strCC = new string(ReadChristmasCarol());
			Rope ropeCC = RopeBuilder.BUILD(strCC);

			sw.Start();
			strCC = strCC.Insert(strCC.Length - 1, "hello!");
			sw.Stop();
			Report("Insertion time for ChristmasCarol string", sw.Elapsed);

			sw.Restart();
			ropeCC = ropeCC.Insert(ropeCC.Length() - 1, "hello!");
			sw.Stop();
			Report("Insertion time for ChristmasCarol Rope", sw.Elapsed);
		}

		private static char[] ReadChristmasCarol()
		{
			char[] output = new char[lenCC];
			StreamReader reader = new StreamReader("TestRepo/AChristmasCarol.txt");
			reader.Read(output, 0, lenCC);

			return output;
		}

		private void Report(string message, TimeSpan span)
		{
			writer.WriteLine(message + ", " + span);
			writer.Flush();
		}
	}
}
