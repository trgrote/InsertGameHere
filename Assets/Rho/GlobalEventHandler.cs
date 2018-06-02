using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using rho;

// TODO Put in own file
public class Tuple<T1,T2>
{
	public T1 Item1;
	public T2 Item2;

	public Tuple(T1 i1, T2 i2)
	{
		Item1 = i1;
		Item2 = i2;
	}
}

namespace rho
{
	// TODO Add a 'slow' unregister so objects can unregister themselves during
	// an event call
	/// <summary>
	/// Class for handling observable game events.
	/// </summary>
	public static class GlobalEventHandler
	{
		public delegate void FunctionToCall<T>(T evt) where T : IGameEvent;

		/// <summary>
		/// Dictionary with IGameEvent keys associated with a list of valid delegates.
		/// </summary>
		private static Dictionary<Type, List<Tuple<GameObject,object>>> eventList = new Dictionary<Type, List<Tuple<GameObject,object>>>();

		/// <summary>
		/// Attaches a delegate to a specific IGameEvent.
		/// Example:
		/// GlobalEventHandler.Register<PlayerDeathEvent>(OnPlayerDeathEvent)
		/// ...
		/// public void OnPlayerDeathEvent(PlayerDeathEvent evt){}
		/// </summary>
		/// <param name="evt"></param>
		/// <param name="f"></param>
		public static void Register<T>(FunctionToCall<T> f) where T: IGameEvent
		{
			// First, get the class Type
			Type type = typeof(T);
			// If it doesn't exist, add an entry for it
			if (!eventList.ContainsKey(type))
			{
				eventList.Add(type, new List<Tuple<GameObject, object>>());
			}
			// Add the delegate to the list. The Target is cast explicitly to MonoBehaviour in order to retrieve the GameObject instance
			eventList[type].Add(new Tuple<GameObject, object>(
				((MonoBehaviour)f.Target).gameObject,
				(object)f
			));
		}

		/// <summary>
		/// Removes a delegate from an IGameEvent list, if it exists.
		/// Example:
		/// GlobalEventHandler.Unregister<PlayerDeathEvent>(OnPlayerDeathEvent)
		/// ...
		/// public void OnPlayerDeathEvent(PlayerDeathEvent evt){}
		/// </summary>
		/// <param name="evt"></param>
		/// <param name="f"></param>
		public static void Unregister<T>(FunctionToCall<T> f) where T: IGameEvent
		{
			// Grab the Type and make a temporary Tuple containing all the necessary data
			Type type = typeof(T);

			// Don't bother looking if it doesn't exist
			if (eventList.ContainsKey(type))
			{
				eventList[type].RemoveAll(x=>(FunctionToCall<T>) x.Item2 == f);
			}
		}

		/// <summary>
		/// Sends the IGameEvent to all delegates attached to it.
		/// Example:
		/// GlobalEventHandler.SendEvent(new PlayerDeathEvent())
		/// </summary>
		/// <param name="evt"></param>
		public static void SendEvent<T>(T evt) where T: IGameEvent
		{
			List<Tuple<GameObject,object>> list;
			if (eventList.TryGetValue(typeof(T), out list))
			{
				for (var i = list.Count - 1; i >= 0; i--)
				{
					var e = list[i];
					if (e.Item1 != null)
					{
						var del = (FunctionToCall<T>) e.Item2;
						del(evt);
					}
					else
					{
						list.Remove(e);
					}
				}
			}
		}
	}
}
