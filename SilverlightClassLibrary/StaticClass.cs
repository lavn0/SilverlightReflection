namespace SilverlightClassLibrary
{
	public static class StaticClass
	{
		private static string field = "field";
		private static string Property { get; set; } = "property";

		private static bool Method()
		{
			return true;
		}

		private static string Method(int i)
		{
			return i.ToString("D3");
		}

		private static string Method(string item1, bool item2)
		{
			return item1 + "__" + item2;
		}
	}
}
