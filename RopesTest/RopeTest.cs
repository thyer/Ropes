using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace RopeTest
{
	[TestClass]
	public class RopeTest
	{
		private static string RopeToString(Rope rope, int start, int end)
		{
			return rope.SubSequence(start, end).ToString();
		}

		[TestMethod]
		public void TestSubstringDelete()
		{
			string s = "12345678902234567890";

			Rope rope = RopeBuilder.BUILD(s);
			rope = rope.Delete(0, 1);
			Assert.AreEqual("23", RopeToString(rope, 0, 2));
			Assert.AreEqual("", RopeToString(rope, 0, 0));
			Assert.AreEqual("902", RopeToString(rope, 7, 10));

			rope = RopeBuilder.BUILD(s.ToCharArray());
			rope = rope.Delete(0, 1);
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

		private void Compare(Rope ropeTest, string strTest)
		{
			Assert.AreEqual(strTest.IndexOf("H"), ropeTest.IndexOf("H"));
			Assert.AreEqual(strTest.IndexOf('H'), ropeTest.IndexOf('H'));
			Assert.AreEqual(strTest.IndexOf("Hello, world"), ropeTest.IndexOf("Hello, world"));
			Assert.AreEqual(strTest.IndexOf("Hello, world!"), ropeTest.IndexOf("Hello, world!"));
			Assert.AreEqual(strTest.IndexOf("el"), ropeTest.IndexOf("el"));
			Assert.AreEqual(strTest.IndexOf("l"), ropeTest.IndexOf("l"));
			Assert.AreEqual(strTest.IndexOf('1'), ropeTest.IndexOf('1'));
			Assert.AreEqual(strTest.IndexOf("ld!"), ropeTest.IndexOf("ld!"));
			Assert.AreEqual(strTest.IndexOf("x"), ropeTest.IndexOf("x"));
			Assert.AreEqual(strTest.IndexOf('x'), ropeTest.IndexOf('x'));
			Assert.AreEqual(strTest.IndexOf("hello, world"), ropeTest.IndexOf("hello, world"));
			Assert.AreEqual(strTest.IndexOf("ld! "), ropeTest.IndexOf("ld! "));
		}

		[TestMethod]
		public void TestIterator()
		{
			Rope r = RopeBuilder.BUILD("01234aaa56789");
			r = r.Delete(5, 8); // "0123456789"
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
			r = r.Delete(4, 10);
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
	}
}
