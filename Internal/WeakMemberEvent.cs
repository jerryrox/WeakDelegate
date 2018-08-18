using System;
using System.Collections.Generic;
using System.Reflection;

namespace Renko.Events.Internal
{
	/// <summary>
	/// Weak event variant that provides delegate from the instance's member.
	/// </summary>
	public class WeakMemberEvent<T> : WeakEvent<T> where T : class {

		/// <summary>
		/// Callback cached for performance.
		/// </summary>
		private WeakReference<T> cachedCallback;


		public WeakMemberEvent(object instance, MethodInfo methodInfo) : base(instance, methodInfo)
		{
			cachedCallback = new WeakReference<T>(null);
		}

		/// <summary>
		/// Releases references.
		/// </summary>
		public override void Dispose ()
		{
			base.Dispose();
			cachedCallback = null;
		}

		/// <summary>
		/// Returns the delegate to invoke.
		/// </summary>
		protected override T GetEventInternal (object instance)
		{
			// Hold a strong reference to the delegate.
			var callback = cachedCallback.Target;

			// Reset cache if disposed.
			if(callback == null)
			{
				callback = Delegate.CreateDelegate(typeof(T), instance, methodInfo) as T;
				cachedCallback.Target = callback;
			}

			return callback;
		}
	}
}