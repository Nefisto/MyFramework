#pragma warning disable 0414 // Assigned but never used (DeveloperDescription)
#pragma warning disable 0649 // Never assigned (eventListeners)

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEventInt", menuName = "Events/GameEvent(int)")]
public class GameEventInt : ScriptableObject
{
    [SerializeField]
    [Multiline]
    private string DeveloperDescription = "";

    public int value;

    [Header("Happen every triggered time")]
    public MyIntEvent defaultBehavior;

    [Space]

    [Header("Registered listeners")]
    [SerializeField]
    private List<GameEventListenerInt> eventListeners;// = new List<GameEventListenerInt>();

    private void OnEnable()
    {
        eventListeners.Clear();
    }

    private void OnDisable()
    {
        eventListeners.Clear();
    }

    public void Raise(int _value)
    {
        if (defaultBehavior != null)
            defaultBehavior.Invoke(_value);

        for (int i = eventListeners.Count - 1; i >= 0; i--)
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