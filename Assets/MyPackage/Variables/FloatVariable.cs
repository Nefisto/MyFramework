#pragma warning disable 0414

using UnityEngine;

[CreateAssetMenu(fileName = "FloatVariable", menuName = "Variables/Float")]
public class FloatVariable : ScriptableObject
{
    [SerializeField]
    [Multiline]
    private string DeveloperDescription = "";

    [SerializeField]
    private float value;
    public float Value
    {
        get => value;
        set => this.value = value;
    }


    // Useful when calling by event as SLIDER ONCHANGE EVENT
    // public void SetValue(float value)
    // {
    //     Value = value;
    // }

    // public void SetValue(FloatVariable value)
    // {
    //     Value = value.Value;
    // }

    public void ApplyChange(float amount)
    {
        Value += amount;
    }

    public void ApplyChange(FloatVariable amount)
    {
        Value += amount.Value;
    }
}