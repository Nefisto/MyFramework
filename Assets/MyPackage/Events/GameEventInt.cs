using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEventInt", menuName = "Events/GameEvent(int)")]
public class GameEventInt : ScriptableObject
{
    // / <summary>
    // / The list of listeners that this event will notify if it is raised.
    // / </summary>
    private readonly List<GameEventListenerInt> eventListeners = 
        new List<GameEventListenerInt>();
    
    #pragma warning disable 0414
    [SerializeField]
    [Multiline]
    private string DeveloperDescription = "";
    #pragma warning restore 0414

    public int value;

    public void Raise(int _value)
    {
        for(int i = eventListeners.Count -1; i >= 0; i--)
            eventListeners[i].OnEventRaised(_value);
    }

    public void RegisterListener(GameEventListenerInt listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventListenerInt listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }

    // public override void Raise(int _value)
    // {
    //     for (int i = eventListeners.Count - 1; i >= 0; i--)
    //         eventListeners[i].OnEventRaised(_value);
    // }
}