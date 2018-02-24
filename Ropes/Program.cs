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
			Rope hello = RopeBuilder.build("hello, world");
			Console.WriteLine(hello.ToString());
			Console.WriteLine(hello.Append('!'));
			Console.WriteLine(hello.StartsWith("!"));
		}
	}
}
