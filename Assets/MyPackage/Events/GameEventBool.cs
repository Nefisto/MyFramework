#pragma warning disable 0414 // Assigned but never used (DeveloperDescription)
#pragma warning disable 0649 // Never assigned (eventListeners)

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEventBool", menuName = "Events/GameEvent(bool)")]
public class GameEventBool : ScriptableObject
{
    [SerializeField]
    [Multiline]
    private string DeveloperDescription = "";

    [Header("Debug")]
    public bool value;

    [Header("Happen every trigger")]
    public MyBoolEvent defaultBehavior;

    [Space]

    [Header("Registered listeners")]
    [SerializeField]
    private List<GameEventListenerBool> eventListeners;// = new List<GameEventListenerInt>();

    private void OnEnable()
    {
        eventListeners.Clear();
    }

    private void OnDisable()
    {
        eventListeners.Clear();
    }

    public void Raise(bool _value)
    {
        if (defaultBehavior != null)
            defaultBehavior.Invoke(_value);

        for (int i = eventListeners.Count - 1; i >= 0; i--)
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
}