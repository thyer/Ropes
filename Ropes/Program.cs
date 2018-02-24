using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ropes
{
	class Program
	{
		static void Main(string[] args)
		{
			Rope hello = RopeBuilder.BUILD("hello, world");
			Console.WriteLine(hello.ToString());
			Rope helloYell = hello.Append('!').Append("!!");
			Console.WriteLine(hello.StartsWith("!"));
		}
	}
}
