using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;

namespace RopeTest
{
    [TestClass]
    public class PerformanceTest
    {
        private static readonly StreamWriter writer = new StreamWriter("output.txt");
        private static readonly Stopwatch sw = new Stopwatch();

        [TestInitialize]
        public void Setup()
        {
            sw.Reset();
        }

        [TestMethod]
        public void ChristmasCarolPerf_Read()
        {
            string strCC = ReadChristmasCarol();

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
            int length = strCC.Length;

            sw.Start();
            strCC = strCC.Remove(length - 550, 550);
            sw.Stop();
            Report("Substring time for ChristmasCarol string", sw.Elapsed);

            sw.Restart();
            ropeCC = ropeCC.SubSequence(length - 550, length);
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

        private static string ReadChristmasCarol()
        {
            return RopesTest.TestStrings.AChristmasCarol;
        }

        private void Report(string message, TimeSpan span)
        {
            writer.WriteLine(message + ", " + span);
            writer.Flush();
        }
    }
}
