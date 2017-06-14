using System.Linq;
using System.Reflection;
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
			var result = Publicer.GetMember<int>(obj.GetType(), "GetMethod", obj);
			Assert.AreEqual(1, result);
		}

		[TestMethod]
		public void GetMemberTestInfo()
		{
			var obj = new NonStaticClass();
			var result = Publicer.GetMember<int>(obj.GetType().GetMember("GetMethod", (BindingFlags)int.MaxValue).First(), obj);
			Assert.AreEqual(1, result);
		}

		[TestMethod]
		public void GetMethodTestStr()
		{
			var obj = new NonStaticClass();
			var result = Publicer.CallMethod<string>(typeof(NonStaticClass), "GetMethodWithParams", obj, "hoge", true);
			Assert.AreEqual("hogeTrue", result);
		}

		[TestMethod]
		public void GetMethodTestInfo()
		{
			var obj = new NonStaticClass();
			var result = Publicer.CallMethod<string>(obj.GetType().GetMethod("GetMethodWithParams", (BindingFlags)int.MaxValue), obj, "hoge", true);
			Assert.AreEqual("hogeTrue", result);
		}
	}
}
