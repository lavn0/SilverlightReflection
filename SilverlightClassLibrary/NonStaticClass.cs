namespace SilverlightClassLibrary
{
	public class NonStaticClass
	{
		private string field = "field";
		private string Property { get; set; } = "property";

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
