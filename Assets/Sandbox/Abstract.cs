using UnityEngine;

public abstract class MasterAction : MonoBehaviour
{
    public abstract void Action<T>(T value) where T : MasterEvent;
}

public abstract class MasterEvent : MonoBehaviour
{
    public abstract T Answer<T>();
}
