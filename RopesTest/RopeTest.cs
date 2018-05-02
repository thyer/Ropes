using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using Ropes.Implementations;

namespace RopeTest
{
	[TestClass]
	public class RopeTest
	{
		private static string RopeToString(Rope rope, int start, int end)
		{
			return rope.SubSequence(start, end).ToString();
		}

		private static string StringReverse(string str)
		{
			char[] rgch = str.ToCharArray();
			Array.Reverse(rgch);
			return new string(rgch);
		}

		private static void Compare(Rope ropeTest, string strTest)
		{
			Assert.AreEqual(ropeTest.Length(), strTest.Length);
			for (int i = 0; i < ropeTest.Length(); ++i)
			{
				Assert.AreEqual(ropeTest.CharAt(i), strTest[i]);
			}
		}

		[TestMethod]
		public void TestSubstringDelete()
		{
			string s = "12345678902234567890";

			Rope rope = RopeBuilder.BUILD(s);
			rope = rope.Remove(0, 1);
			Assert.AreEqual("23", RopeToString(rope, 0, 2));
			Assert.AreEqual("", RopeToString(rope, 0, 0));
			Assert.AreEqual("902", RopeToString(rope, 7, 10));

			rope = RopeBuilder.BUILD(s.ToCharArray());
			rope = rope.Remove(0, 1);
			Assert.AreEqual("23", RopeToString(rope, 0, 2));
			Assert.AreEqual("", RopeToString(rope, 0, 0));
			Assert.AreEqual("902", RopeToString(rope, 7, 10));
		}

		[TestMethod]
		public void TestSubstringAppend()
		{
			Rope r = RopeBuilder.BUILD("");
			r = r.Append("round ");
			r = r.Append((0).ToString());
			r = r.Append(" 1234567890");

			Assert.AreEqual("round ", RopeToString(r, 0, 6));
			Assert.AreEqual("round 0", RopeToString(r, 0, 7));
			Assert.AreEqual("round 0 ", RopeToString(r, 0, 8));
			Assert.AreEqual("round 0 1", RopeToString(r, 0, 9));
			Assert.AreEqual("round 0 12", RopeToString(r, 0, 10));
			Assert.AreEqual("round 0 1234567890", RopeToString(r, 0, 18));
			Assert.AreEqual("round 0 1234567890", RopeToString(r, 0, r.Length()));
		}

		[TestMethod]
		public void TestIndexOf()
		{
			string strTest = "Hello, world!";
			Rope ropeTest = RopeBuilder.BUILD(strTest);
			Compare(ropeTest, strTest);

			ropeTest = ropeTest.Insert(7, "under");
			strTest = "Hello, underworld!";
			Compare(ropeTest, strTest);

			ropeTest = ropeTest.SubSequence(1, ropeTest.Length());
			strTest = "ello, underworld!";
			Compare(ropeTest, strTest);

			ropeTest = ropeTest.Append("123456789");
			strTest = strTest + "123456789";
			Compare(ropeTest, strTest);

			ropeTest = ropeTest.Append("123456789");
			strTest = strTest + "123456789";
			Compare(ropeTest, strTest);
		}

		[TestMethod]
		public void TestIterator()
		{
			Rope r = RopeBuilder.BUILD("01234aaa56789");
			r = r.Remove(5, 8); // "0123456789"
			for(int i = 0; i < r.Length(); ++i)
			{
				Assert.AreEqual("0123456789"[i], r.CharAt(i));
			}

			r = r.SubSequence(1, r.Length()); // "123456789"
			for (int i = 0; i < r.Length(); ++i)
			{
				Assert.AreEqual("123456789"[i], r.CharAt(i));
			}

			r = r.Append('0'); // "1234567890"
			int j = 0;
			foreach(char c in r){
				Assert.AreEqual("1234567890"[j], c);
				++j;
			}
		}

		[TestMethod]
		public void TestIEnumerator()
		{
			Rope r = RopeBuilder.BUILD("01234aaa56789");
			r = r.Insert(4, "0000001123456");
			r = r.Insert(3, "hello, stranger");
			r = r.Remove(4, 10);
			r = r.Insert(0, "      ");
			r = r.SubSequence(3, 17);

			IEnumerator enumerator = r.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Assert.IsTrue(enumerator.Current != null);
			}
		}

		[TestMethod]
		public void TestIndexOfAcrossConcatenateRopes()
		{
			Rope r = RopeBuilder.BUILD("aaaaaaaaaaaaaaaa12");
			r = r.Append("3456789aaaaaa");
			r = r.SubSequence(1, r.Length());
			Assert.AreEqual(r.IndexOf("123456789", 3), 15);

			r = RopeBuilder.BUILD("aaaaaaaaaaaa1111");
			r = r.Append("1bbbbbbbbbbbbbbbb");
			Assert.AreEqual(r.IndexOf("1111", 3), 12);
		}

		[TestMethod]
		public void AddRemoveFromMiddle_RopeStringEquivalence()
		{
			string str = "11111";
			Rope r = RopeBuilder.BUILD(new RepeatedCharacterSequence('1', 5));

			str = str.Insert(1, "aa");
			r = r.Insert(1, "aa");
			Compare(r, str);

			Assert.AreEqual(str.IndexOf("aa", 1), r.IndexOf("aa", 1));

			str = str.Remove(1, 2);
			r = r.Remove(1, 3);
			Compare(r, str);

			Assert.AreEqual(str.IndexOf("aa", 1), r.IndexOf("aa", 1));
			Assert.AreEqual(str.IndexOf("doesn't exist", 1), r.IndexOf("doesn't exist", 1));
		}

		[TestMethod]
		public void TrimWhitespace_RopeStringEquivalence()
		{
			string str = "11111";
			Rope r = RopeBuilder.BUILD(new RepeatedCharacterSequence('1', 5));

			str = str.Insert(1, "aa");
			r = r.Insert(1, "aa");
			Compare(r, str);

			// Add spaces onto the end and trim
			r = r.Append("  ");
			str = str + "  ";
			Compare(r, str);

			r = r.Trim();
			str = str.Trim();

			Compare(r, str);

			// Trim without any whitespace
			r = r.Trim();
			str = str.Trim();

			Compare(r, str);

			// Lots of whitespace at the beginning
			r = r.PadStart(r.Length() + 2);
			str = "  " + str;
			Compare(r, str);

			r = r.Trim();
			str = str.Trim();

			Compare(r, str);
		}

		[TestMethod]
		public void SubstringRopeReverse_RopeStringEquivalence()
		{
			string str = "abcdefghijklmnopqrstuvwxyz";
			Rope r = RopeBuilder.BUILD(str);

			str = str.Substring(15);
			r = r.SubSequence(15, r.Length());
			Compare(r, str);

			str = StringReverse(str);
			r = r.Reverse();
			Compare(r, str);

			str = str + "  ";
			r = r.PadEnd(r.Length() + 2);
			Compare(r, str);
		}

		[TestMethod]
		public void TrimPaddedReverseRope_RopeStringEquivalence()
		{
			string str = "abcdefghijklmnopqrstuvwxyz   ";
			Rope r = RopeBuilder.BUILD(str);

			str = "   " + str;
			r = r.PadStart(r.Length() + 3);
			Compare(r, str);

			str = str.Substring(2, 18);
			r = r.SubSequence(2, 20);
			Compare(r, str);

			str = StringReverse(str);
			r = r.Reverse();
			Compare(r, str);

			str = str.Trim();
			r = r.Trim();
			Compare(r, str);
		}

		[TestMethod]
		public void AppendReversedSubstrings_RopeStringEquivalence()
		{
			string str = "abcdefghijklmnopqrstuvwxyz   ";
			Rope r = RopeBuilder.BUILD(str);

			str = "   " + str;
			r = r.PadStart(r.Length() + 3);
			Compare(r, str);

			str = StringReverse(str.Substring(2)) + StringReverse(str.Substring(2, 6));
			r = r.SubSequence(2).Reverse().Append(r.SubSequence(2, 8).Reverse());
			Compare(r, str);

			str = str.Trim();
			r = r.Trim();
			Compare(r, str);

			str = str.Substring(5);
			r = r.SubSequence(5);
			Compare(r, str);
		}

		[TestMethod]
		public void DeleteFromMiddleOfReverse_RopeStringEquivalence()
		{
			string str = "Hello darkness, my old 12345friend";
			Rope r = RopeBuilder.BUILD(str);

			str = StringReverse(str);
			r = r.Reverse();
			Compare(r, str);

			str = str.Remove(6, 5);
			string s1 = r.SubSequence(0, 6).ToString();
			string s2 = r.SubSequence(11, r.Length()).ToString();
			r = r.Remove(6, 11);
			Compare(r, str);
		}

		[TestMethod]
		public void TrimShortStrings_RopeStringEquivalence()
		{
			string str = " ";
			Rope r = RopeBuilder.BUILD(str);

			str = str.Trim();
			r = r.Trim();
			Compare(r, str);
		}
	}
}
