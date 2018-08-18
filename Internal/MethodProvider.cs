using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Renko.Events.Internal
{
	/// <summary>
	/// Container class of method provider variants.
	/// </summary>
	public class MethodProvider<T> : IEnumerable<T> where T : class {
		
		private List<WeakEvent<T>> events;


		public MethodProvider()
		{
			events = new List<WeakEvent<T>>();
		}

		/// <summary>
		/// Creates and adds a new provider to list.
		/// </summary>
		public void Add(object instance, T method)
		{
			var methodInfo = (method as Delegate).Method;
			WeakEvent<T> weakEvent = null;

			// Create weak event instance.
			if(methodInfo.IsDefined(typeof(CompilerGeneratedAttribute), false))
				weakEvent = new WeakAnonymousEvent<T>(instance, method, methodInfo);
			else
				weakEvent = new WeakMemberEvent<T>(instance, methodInfo);

			events.Add(weakEvent);
		}

		/// <summary>
		/// Removes the event associated with specified parameters.
		/// </summary>
		public void Remove(object instance, T method)
		{
			var methodInfo = (method as Delegate).Method;

			for(int i=0; i<events.Count; i++)
			{
				if(events[i].IsMyEvent(instance, methodInfo))
				{
					// Dispose event and remove.
					events[i].Dispose();
					events.RemoveAt(i);
					break;
				}
			}
		}

		public IEnumerator<T> GetEnumerator ()
		{
			// Loop from 0, even if some entries my be removed.
			// This is to make sure the execution order is preserved.
			for(int i=0; i<events.Count; i++)
			{
				var e = events[i].GetEvent();
				if(e == null)
				{
					// Dispose event and remove.
					events[i].Dispose();
					events.RemoveAt(i);
					i --;
					continue;
				}
				yield return e;
			}
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return (IEnumerator)this.GetEnumerator();
		}
	}
}