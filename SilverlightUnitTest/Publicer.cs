using System;
using System.Linq.Expressions;
using System.Reflection;

namespace SilverlightUnitTest
{
	public static class Publicer
	{
		public static T CallMethod<T>(object instance, Type type, string methodName)
		{
			var methodInfo = type.GetMethod(methodName, (BindingFlags)int.MaxValue);
			var lambda = Expression.Lambda<Func<T>>(Expression.Call(Expression.Constant(instance), methodInfo));
			return lambda.Compile().Invoke();
		}
	}
}
