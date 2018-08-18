using System;
using System.Reflection;

namespace Renko.Events.Internal
{
	/// <summary>
	/// Base class of the weak event variants.
	/// </summary>
	public abstract class WeakEvent<T> : IDisposable where T : class {

		protected WeakReference instance;
		protected MethodInfo methodInfo;


		public WeakEvent(object instance, MethodInfo methodInfo)
		{
			this.instance = new WeakReference(instance);
			this.methodInfo = methodInfo;
		}
		
		/// <summary>
		/// Returns whether specified parameters are linked to this event.
		/// </summary>
		public bool IsMyEvent(object otherInstance, MethodInfo otherMethodInfo)
		{
			return instance.Target == otherInstance && methodInfo == otherMethodInfo;
		}

		/// <summary>
		/// Returns the delegate instance stored in this event.
		/// May return null if the weak reference is released.
		/// </summary>
		public T GetEvent()
		{
			// Hold a strong reference to the instance.
			var inst = instance.Target;

			// If instance is already released, return null for disposal
			if(inst == null)
				return null;

			return GetEventInternal(inst);
		}

		/// <summary>
		/// Releases references.
		/// </summary>
		public virtual void Dispose()
		{
			instance = null;
			methodInfo = null;
		}

		/// <summary>
		/// Returns the delegate to invoke.
		/// </summary>
		protected abstract T GetEventInternal(object instance);
	}
}

