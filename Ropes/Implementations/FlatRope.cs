namespace Ropes.Implementations
{
	/// <summary>
	/// Rope backed directly by a datasource
	/// </summary>
	interface FlatRope : Rope
	{
		/// <summary>
		/// Returns a string representation of a range in this rope
		/// </summary>
		/// <param name="offset">the offset</param>
		/// <param name="length">the length</param>
		/// <returns>string representation of a range in this rope</returns>
		string ToString(int offset, int length);
	}
}