using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEventBool", menuName = "Events/GameEvent(bool)")]
public class GameEventBool : ScriptableObject
{
    // / <summary>
    // / The list of listeners that this event will notify if it is raised.
    // / </summary>
    private readonly List<GameEventListenerBool> eventListeners = 
        new List<GameEventListenerBool>();
    
    #pragma warning disable 0414
    [SerializeField]
    [Multiline]
    private string DeveloperDescription = "";
    #pragma warning restore 0414

    public bool value;

    public void Raise(bool _value)
    {
        for(int i = eventListeners.Count -1; i >= 0; i--)
            eventListeners[i].OnEventRaised(_value);
    }

    public void RegisterListener(GameEventListenerBool listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventListenerBool listener)
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