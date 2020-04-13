using UnityEngine;

public abstract class BaseVariable<T> : ScriptableObject
{
    #pragma warning disable 0414
    
    [SerializeField]
    [Multiline]
    private string DeveloperDescription = "";
    
    #pragma warning restore 0414

    [Header("Will have an default value?")]
    public bool haveDefaultValue = false;
    
    [SerializeField]
    private T value;
    private T runTimeValue;
    
    public T Value
    {
        get => haveDefaultValue ? runTimeValue : value;
        set
        {
            if (haveDefaultValue)
                runTimeValue = value;
            else
                this.value = value;
        }
    }

    private void OnEnable()
    {
        if (haveDefaultValue)
            runTimeValue = value;
    }
}