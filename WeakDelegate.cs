using System;
using Renko.Events.Internal;

namespace Renko.Events
{
	public abstract class WeakDelegateBase<T> where T : class {

		protected MethodProvider<T> provider;


		public WeakDelegateBase() { provider = new MethodProvider<T>(); }

		/// <summary>
		/// Adds the specified method to event pool.
		/// </summary>
		public void Add(object instance, T method) { provider.Add(instance, method); }

		/// <summary>
		/// Adds the specified method to event pool.
		/// </summary>
		public void Remove(object instance, T method) { provider.Remove(instance, method); }
	}

	public class WeakDelegate : WeakDelegateBase<Action> {
		
		/// <summary>
		/// Invokes all registered delegates.
		/// </summary>
		public void Invoke()
		{
			foreach(var callback in provider)
				callback();
		}
	}

	public class WeakDelegate<T> : WeakDelegateBase<Action<T>> {

		/// <summary>
		/// Invokes all registered delegates.
		/// </summary>
		public void Invoke(T t)
		{
			foreach(var callback in provider)
				callback(t);
		}
	}

	public class WeakDelegate<T1, T2> : WeakDelegateBase<Action<T1, T2>> {

		/// <summary>
		/// Invokes all registered delegates.
		/// </summary>
		public void Invoke(T1 t1, T2 t2)
		{
			foreach(var callback in provider)
				callback(t1, t2);
		}
	}

	public class WeakDelegate<T1, T2, T3> : WeakDelegateBase<Action<T1, T2, T3>> {

		/// <summary>
		/// Invokes all registered delegates.
		/// </summary>
		public void Invoke(T1 t1, T2 t2, T3 t3)
		{
			foreach(var callback in provider)
				callback(t1, t2, t3);
		}
	}

	public class WeakDelegate<T1, T2, T3, T4> : WeakDelegateBase<Action<T1, T2, T3, T4>> {

		/// <summary>
		/// Invokes all registered delegates.
		/// </summary>
		public void Invoke(T1 t1, T2 t2, T3 t3, T4 t4)
		{
			foreach(var callback in provider)
				callback(t1, t2, t3, t4);
		}
	}
}