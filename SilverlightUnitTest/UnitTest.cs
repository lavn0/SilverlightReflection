using System;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SilverlightClassLibrary;

namespace SilverlightUnitTest
{
	[TestClass]
	public class UnitTest
	{
		[TestInitialize]
		public void TestInitialize()
		{
			Publicer.Clear();
		}

		[TestMethod]
		public void InstantPublicerTest()
		{
			var obj = new Class1();
			var result = InstantPublicer.CallMethod<string>(obj, obj.GetType(), "GetTest");
			Assert.AreEqual("hoge", result);
		}

		[TestMethod]
		[ExpectedException(typeof(MethodAccessException))]
		public void MethodAccessErrorTest()
		{
			var obj = new Class1();
			var methodInfo = typeof(Class1).GetMethod("GetTest", (BindingFlags)int.MaxValue);
			var result = methodInfo.Invoke(obj, null);
			Assert.AreEqual("hoge", result);
		}

		[TestMethod]
		public void GetMemberTestStr()
		{
			var obj = new Class1();
			var result = Publicer.GetMember<string>(obj.GetType(), "GetTest", obj);
			Assert.AreEqual("hoge", result);
		}

		[TestMethod]
		public void GetMemberTestInfo()
		{
			var obj = new Class1();
			var result = Publicer.GetMember<string>(obj.GetType().GetMember("GetTest", (BindingFlags)int.MaxValue).First(), obj);
			Assert.AreEqual("hoge", result);
		}

		[TestMethod]
		public void GetMethodTestStr()
		{
			var obj = new Class1();
			var result = Publicer.CallMethod<string>(typeof(Class1), "GetTests", obj, "hoge", true);
			Assert.AreEqual("hogeTrue", result);
		}

		[TestMethod]
		public void GetMethodTestInfo()
		{
			var obj = new Class1();
			var result = Publicer.CallMethod<string>(obj.GetType().GetMethod("GetTests", (BindingFlags)int.MaxValue), obj, "hoge", true);
			Assert.AreEqual("hogeTrue", result);
		}
	}
}
