using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventManagerFuong<TeventArgs>
{
    private static Dictionary<string, Action<TeventArgs>> eventDictionary = new Dictionary<string, Action<TeventArgs>>();

    public static void RegisterEvent(string eventType, Action<TeventArgs> eventHandler)
    {
        if (!eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType] = eventHandler;
        }
        else
        {
            eventDictionary[eventType] += eventHandler;
        }
    }

    public static void UnregisterEvent(string eventType, Action<TeventArgs> eventHandler)
    {
        if (eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType] += eventHandler;
        }
    }

    public static void TriggerEvent(string eventType, TeventArgs eventArgs)
    {
        if(eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType]?.Invoke(eventArgs);
        }
    }
}
