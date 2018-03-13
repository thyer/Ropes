using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ropes
{
	/// <summary>
	/// For compatability between Java's CharSequence class and Rope
	/// </summary>
	public interface CharSequence
	{
		char CharAt(int index);
		int Length();
		CharSequence SubSequence(int start, int end);
		String ToString();
	}
}
