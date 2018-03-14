using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
	}
}
