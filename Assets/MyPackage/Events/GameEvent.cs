#pragma warning disable 0414 // Assigned but never used (DeveloperDescription)
#pragma warning disable 0649 // Never assigned (eventListeners)

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Events/GameEvent(void)")]
public class GameEvent : ScriptableObject
{
    [SerializeField]
    [Multiline]
    private string DeveloperDescription = "";

    [Header("Happen every triggered time")]
    public UnityEvent defaultBehavior;

    [Space]

    [Header("Registered listeners")]
    [SerializeField]
    private List<GameEventListener> eventListeners;// = new List<GameEventListenerInt>();

    private void OnEnable()
    {
        eventListeners.Clear();
    }

    private void OnDisable()
    {
        eventListeners.Clear();
    }

    public void Raise()
    {
        if (defaultBehavior != null)
            defaultBehavior.Invoke();

        for (int i = eventListeners.Count - 1; i >= 0; i--)
            eventListeners[i].OnEventRaised();
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}