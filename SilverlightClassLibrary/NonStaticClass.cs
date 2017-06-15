namespace SilverlightClassLibrary
{
	public class NonStaticClass
	{
		private static string StaticField = "static field";
		private static string StaticProperty { get; set; } = "static property";

		private string field = "field";
		private string Property { get; set; } = "property";

		private static bool StaticMethod()
		{
			return false;
		}

		private static string StaticMethod(int i)
		{
			return "static " + i.ToString("D2");
		}

		private static string StaticMethod(string item1, bool item2)
		{
			return "static " + item1 + item2;
		}

		private bool Method()
		{
			return true;
		}

		private string Method(int i)
		{
			return i.ToString("D2");
		}

		private string Method(string item1, bool item2)
		{
			return item1 + item2;
		}
	}
}
