namespace SilverlightClassLibrary
{
	public class NonStaticClassForInstantPublicer
	{
		private string field = "field";
		private string Property { get; set; } = "property";

		private bool Method1()
		{
			return true;
		}

		private string Method2(int i)
		{
			return i.ToString("D2");
		}

		private string Method3(string item1, bool item2)
		{
			return item1 + item2;
		}
	}
}
