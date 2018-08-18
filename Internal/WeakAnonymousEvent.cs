using System;
using System.Reflection;

namespace Renko.Events.Internal
{
	/// <summary>
	/// Weak event variant that provides delegte from anonymous methods linked to an instance.
	/// </summary>
	public class WeakAnonymousEvent<T> : WeakEvent<T> where T : class {

		/// <summary>
		/// Hold a strong reference to the anonymous method.
		/// This will make sure the method won't be collected before the instance.
		/// </summary>
		private T method;


		public WeakAnonymousEvent(object instance, T method, MethodInfo methodInfo) : base(instance, methodInfo)
		{
			this.method = method;
		}

		/// <summary>
		/// Releases references.
		/// </summary>
		public override void Dispose ()
		{
			base.Dispose ();
			method = null;
		}

		/// <summary>
		/// Returns the delegate to invoke.
		/// </summary>
		protected override T GetEventInternal (object instance)
		{
			return method;
		}
	}
}