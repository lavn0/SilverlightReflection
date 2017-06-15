using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SilverlightClassLibrary;

namespace SilverlightUnitTest
{
	[TestClass]
	public class PublicerTestStatic
	{
		[TestInitialize]
		public void TestInitialize()
		{
			Publicer.Clear();
		}

		[TestMethod]
		public void GetFieldTest()
		{
			var result = Publicer.GetField<string>(typeof(StaticClass), "field", null);
			Assert.AreEqual("field", result);
		}

		[TestMethod]
		public void SetFieldTest()
		{
			Publicer.SetField(typeof(StaticClass), "field", null, "field set value");
			var result = Publicer.GetField<string>(typeof(StaticClass), "field", null);
			Assert.AreEqual("field set value", result);
		}

		[TestMethod]
		public void GetPropertyTest()
		{
			var result = Publicer.GetProperty<string>(typeof(StaticClass), "Property", null);
			Assert.AreEqual("property", result);
		}

		[TestMethod]
		public void SetPropertyTest()
		{
			Publicer.SetProperty(typeof(StaticClass), "Property", null, "property set value");
			var result = Publicer.GetProperty<string>(typeof(StaticClass), "Property", null);
			Assert.AreEqual("property set value", result);
		}

		[TestMethod]
		public void InvokeMethodTest1()
		{
			var result = Publicer.InvokeMethod<bool>(typeof(StaticClass), "Method", new Type[0], null);
			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void InvokeMethodTest2()
		{
			var result = Publicer.InvokeMethod<string>(typeof(StaticClass), "Method", new[] { typeof(int) }, null, 5);
			Assert.AreEqual("005", result);
		}

		[TestMethod]
		public void InvokeMethodTest3()
		{
			var result = Publicer.InvokeMethod<string>(typeof(StaticClass), "Method", new[] { typeof(string), typeof(bool) }, null, "hoge", true);
			Assert.AreEqual("hoge__True", result);
		}

		[TestMethod]
		public void InvokeMethodTest123()
		{
			var result1 = Publicer.InvokeMethod<bool>(typeof(StaticClass), "Method", new Type[0], null);
			Assert.AreEqual(true, result1);
			var result2 = Publicer.InvokeMethod<string>(typeof(StaticClass), "Method", new[] { typeof(int) }, null, 5);
			Assert.AreEqual("005", result2);
			var result3 = Publicer.InvokeMethod<string>(typeof(StaticClass), "Method", new[] { typeof(string), typeof(bool) }, null, "hoge", true);
			Assert.AreEqual("hoge__True", result3);
		}
	}
}
