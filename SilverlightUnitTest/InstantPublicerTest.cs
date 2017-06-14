using System;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SilverlightClassLibrary;

namespace SilverlightUnitTest
{
	[TestClass]
	public class UnitTest0
	{
		[TestInitialize]
		public void TestInitialize()
		{
			Publicer.Clear();
		}

		[TestMethod]
		[ExpectedException(typeof(MethodAccessException))]
		public void MethodAccessErrorTest()
		{
			var obj = new NonStaticClassForInstantPublicer();
			var methodInfo = typeof(NonStaticClassForInstantPublicer).GetMethods((BindingFlags)int.MaxValue).First(m => m.Name == "Method1");
			var result = methodInfo.Invoke(obj, null);
			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void InstantPublicerTest()
		{
			var obj = new NonStaticClassForInstantPublicer();
			var result = InstantPublicer.CallMethod<bool>(obj, obj.GetType(), "Method1");
			Assert.AreEqual(true, result);
		}
	}
}
