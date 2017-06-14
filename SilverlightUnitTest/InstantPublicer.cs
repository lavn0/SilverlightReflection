using System;
using System.Linq;
using System.Reflection;
using Expression = System.Linq.Expressions.Expression;

namespace SilverlightUnitTest
{
	public static class InstantPublicer
	{
		/// <summary>Publicerのプロトタイプ実装</summary>
		public static T CallMethod<T>(object instance, Type type, string methodName)
		{
			var methodInfo = type.GetMethods((BindingFlags)int.MaxValue).First(m => m.Name == methodName);
			var lambda = Expression.Lambda<Func<T>>(Expression.Call(Expression.Constant(instance), methodInfo));
			return lambda.Compile().Invoke();
		}
	}
}
