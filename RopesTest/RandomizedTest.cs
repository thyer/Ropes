using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RopeTest
{
    [TestClass]
    public class RandomizedTest
    {
        private const int seed = 3141592;
        private static readonly Random rand = new Random(RandomizedTest.seed);
        private static readonly StreamWriter log = new StreamWriter("log.txt");

        [TestMethod]
        public void Random100Actions()
        {
            // Reads in A Christmas Carol and randomly performs 100 actions on that rope
            Rope ropeCC = RopeBuilder.BUILD(ReadChristmasCarol());

            for (int i = 0; i < 100; ++i)
            {
                Array values = Enum.GetValues(typeof(Action));
                switch ((Action)values.GetValue(rand.Next(values.Length)))
                {
                    case Action.Append:
                        ropeCC = AppendRandom(ropeCC);
                        break;
                    case Action.Delete:
                        ropeCC = DeleteRandom(ropeCC);
                        break;
                    case Action.Enumerate:
                        Enumerate(ropeCC);
                        break;
                    case Action.Reverse:
                        ropeCC = Reverse(ropeCC);
                        break;
                    case Action.IndexOf:
                        IndexOfRandom(ropeCC);
                        break;
                    case Action.Insert:
                        ropeCC = InsertRandom(ropeCC);
                        break;
                    case Action.TrimStart:
                    case Action.TrimEnd:
                        ropeCC = Trim(ropeCC);
                        break;
                    case Action.Subsequence:
                        ropeCC = SubsequenceRandom(ropeCC);
                        break;
                    case Action.PadStart:
                    case Action.PadEnd:
                        ropeCC = PadRandom(ropeCC);
                        break;
                    case Action.StartsWith:
                    case Action.EndsWith:
                        StartsEndsWith(ropeCC);
                        break;

                }
            }
            log.Flush();
        }

        [TestMethod]
        public void RandomActionsCompareToString()
        {
            // Performs random actions on both a string and a rope, comparing the two after each action
            string output = ReadChristmasCarol();
            Rope ropeCC = RopeBuilder.BUILD(output);
            string strCC = new string(output);

            for (int i = 0; i < 50000; ++i)
            {
                // useful values for random access
                int length = ropeCC.Length();
                int start = rand.Next(length / 2);
                int end = Math.Max(start + 1, length - rand.Next(length / 3));
                end = Math.Min(end, length);

                Array values = Enum.GetValues(typeof(Action));
                Action performAction = (Action)values.GetValue(rand.Next(values.Length));
                switch (performAction)
                {
                    case Action.Append:
                        string randomAppend = GetRandomString();
                        ropeCC = Append(ropeCC, randomAppend);
                        strCC += randomAppend;
                        break;
                    case Action.Delete:
                        ropeCC = ropeCC.Remove(start, end);
                        strCC = strCC.Remove(start, end - start);
                        break;
                    case Action.Enumerate:
                        Enumerate(ropeCC);
                        break;
                    case Action.Reverse:
                        ropeCC = Reverse(ropeCC);
                        char[] charCC = strCC.ToCharArray();
                        Array.Reverse(charCC);
                        strCC = new String(charCC);
                        break;
                    case Action.IndexOf:
                        int fromIndex = rand.Next(ropeCC.Length() / 4);
                        // take a random sequence from the string
                        int iStart = fromIndex + rand.Next((ropeCC.Length() - fromIndex) / 2);
                        int randLength = Math.Min(rand.Next(5), strCC.Length - iStart);
                        string randSubstring = strCC.Substring(iStart, randLength);
                        Assert.AreEqual(strCC.IndexOf(randSubstring, fromIndex), ropeCC.IndexOf(randSubstring, fromIndex));
                        break;
                    case Action.Insert:
                        int iOffset = rand.Next(strCC.Length / 2);
                        string strToInsert = GetRandomString();
                        ropeCC = Insert(ropeCC, iOffset, strToInsert);
                        strCC = strCC.Substring(0, iOffset) + strToInsert + strCC.Substring(iOffset);
                        break;
                    case Action.TrimStart:
                    case Action.TrimEnd:
                        ropeCC = Trim(ropeCC);
                        strCC = strCC.Trim();
                        break;
                    case Action.Subsequence:
                        ropeCC = Subsequence(ropeCC, start, end);
                        strCC = strCC.Substring(start, end - start);
                        break;
                    case Action.PadStart:
                    case Action.PadEnd:
                        ropeCC = ropeCC.PadStart(length + 5, ' ');
                        strCC = strCC.PadLeft(length + 5, ' ');
                        break;
                    case Action.StartsWith:
                    case Action.EndsWith:
                        Assert.AreEqual(ropeCC.StartsWith("a"), strCC.StartsWith("a"));
                        Assert.AreEqual(ropeCC.StartsWith("e"), strCC.StartsWith("e"));
                        Assert.AreEqual(ropeCC.EndsWith("a"), strCC.EndsWith("a"));
                        Assert.AreEqual(ropeCC.EndsWith("e"), strCC.EndsWith("e"));
                        Assert.AreEqual(ropeCC.StartsWith("he"), strCC.StartsWith("he"));
                        Assert.AreEqual(ropeCC.StartsWith("the"), strCC.StartsWith("the"));
                        Assert.AreEqual(ropeCC.EndsWith("he"), strCC.EndsWith("he"));
                        Assert.AreEqual(ropeCC.EndsWith("m."), strCC.EndsWith("m."));
                        break;

                }

                log.Flush();
                Assert.AreEqual(strCC.Length, ropeCC.Length());
                int j = 0;
                foreach (char c in ropeCC)
                {
                    Assert.AreEqual(c, strCC[j]);
                    j++;
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

        private Rope Pad(Rope ropeCC, int cPadStart, int cPadEnd)
        {
            log.WriteLine(GetCurrentMethod());
            return ropeCC.PadStart(cPadStart).PadEnd(cPadEnd);
        }

        private Rope PadRandom(Rope ropeCC)
        {
            return Pad(ropeCC, rand.Next(5), rand.Next(5));
        }

        private Rope Subsequence(Rope ropeCC, int start, int end)
        {
            log.WriteLine(GetCurrentMethod());
            log.WriteLine(String.Format("Start:{0}, End:{1}", start, end));
            return ropeCC.SubSequence(start, end);
        }

        private Rope SubsequenceRandom(Rope ropeCC)
        {
            int length = ropeCC.Length();
            int start = rand.Next(length / 2);
            int end = Math.Max(start + 1, length - rand.Next(length / 3));
            log.WriteLine(String.Format("Length:{0}, Start:{1}, End:{2}", length, start, end));
            return Subsequence(ropeCC, start, end);
        }

        private Rope Trim(Rope ropeCC)
        {
            log.WriteLine(GetCurrentMethod());
            return ropeCC.Trim();
        }

        private Rope Insert(Rope ropeCC, int dstOffset, string strToInsert)
        {
            log.WriteLine(GetCurrentMethod());
            log.WriteLine(String.Format("dstOffset:{0}, string:{1}", dstOffset, strToInsert));
            return ropeCC.Insert(dstOffset, strToInsert);
        }

        private Rope InsertRandom(Rope ropeCC)
        {
            int length = ropeCC.Length();
            int dstOffset = rand.Next(length - 1);
            return Insert(ropeCC, dstOffset, GetRandomString());
        }

        private int IndexOf(Rope ropeCC, char c, int fromIndex)
        {
            log.WriteLine(GetCurrentMethod());
            return ropeCC.IndexOf(c, fromIndex);
        }

        private int IndexOf(Rope ropeCC, string str, int fromIndex)
        {
            log.WriteLine(GetCurrentMethod());
            return ropeCC.IndexOf(str, fromIndex);
        }

        private void IndexOfRandom(Rope ropeCC)
        {
            int fromIndex = rand.Next(ropeCC.Length() / 4);
            if (rand.Next() % 2 == 0) // char
            {
                log.WriteLine(String.Format("Index of next \'e\' from index {0}: {1}", fromIndex, IndexOf(ropeCC, 'e', fromIndex)));
            }
            else // string
            {
                log.WriteLine(String.Format("Index of next occurrence of \"you\" from index {0}: {1}", fromIndex, IndexOf(ropeCC, "you", fromIndex)));
            }
        }

        private Rope Reverse(Rope ropeCC)
        {
            log.WriteLine(GetCurrentMethod());
            return ropeCC.Reverse();
        }

        private void Enumerate(Rope ropeCC)
        {
            log.WriteLine(GetCurrentMethod());
            int cH = 0;
            foreach (char c in ropeCC)
            {
                if (c.Equals('h'))
                {
                    cH++;
                }
            }

            log.WriteLine("Counted all \'h\'s in the text and found: " + cH);
        }

        private Rope Delete(Rope ropeCC, int start, int end)
        {
            log.WriteLine(GetCurrentMethod());
            log.WriteLine(String.Format("Start:{0}, End:{1}", start, end));
            return ropeCC.Remove(start, end);
        }

        private Rope DeleteRandom(Rope ropeCC)
        {
            log.WriteLine(GetCurrentMethod());

            int length = ropeCC.Length();
            int start = rand.Next(length / 2);
            int end = Math.Max(start + 1, length - rand.Next(length / 3));
            log.WriteLine(String.Format("Length:{0}, Start:{1}, End:{2}", length, start, end));
            return Delete(ropeCC, start, end);
        }

        private Rope Append(Rope ropeCC, Rope ropeAppend)
        {
            log.WriteLine(GetCurrentMethod());
            log.WriteLine(String.Format("Appending rope with string: {0}", ropeAppend.ToString()));
            return ropeCC.Append(ropeAppend);
        }

        private Rope Append(Rope ropeCC, string strAppend)
        {
            log.WriteLine(GetCurrentMethod());
            log.WriteLine(String.Format("Appending string: {0}", strAppend));
            return ropeCC.Append(strAppend);
        }

        private Rope AppendRandom(Rope ropeCC)
        {
            log.WriteLine(GetCurrentMethod());

            if (rand.Next() % 2 == 0) // append a new rope
            {
                log.WriteLine(String.Format("Appending random rope"));
                return Append(ropeCC, RopeBuilder.BUILD(GetRandomString()));
            }

            log.WriteLine(String.Format("Appending random string"));
            return Append(ropeCC, GetRandomString());
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
            return new string(Enumerable.Repeat(chars, rand.Next(1, 10))
              .Select(s => s[rand.Next(s.Length)]).ToArray());
        }

        private static string ReadChristmasCarol()
        {
            return RopesTest.TestStrings.AChristmasCarol;
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
