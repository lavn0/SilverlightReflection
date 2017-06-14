using Microsoft.VisualStudio.TestTools.UnitTesting;
using SilverlightClassLibrary;

namespace SilverlightUnitTest
{
	[TestClass]
	public class UnitTest1
	{
		[TestInitialize]
		public void TestInitialize()
		{
			Publicer.Clear();
		}

		[TestMethod]
		public void GetMemberTestStr()
		{
			var obj = new NonStaticClass();
			var result = Publicer.GetField<int>(obj.GetType(), "GetMethod", obj);
			Assert.AreEqual(1, result);
		}

		[TestMethod]
		public void GetMethodTestStr()
		{
			var obj = new NonStaticClass();
			var result = Publicer.InvokeMethod<string>(typeof(NonStaticClass), "GetMethodWithParams", obj, "hoge", true);
			Assert.AreEqual("hogeTrue", result);
		}
	}
}
