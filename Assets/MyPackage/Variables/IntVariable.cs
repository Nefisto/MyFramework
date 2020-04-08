#pragma warning disable 0414

using UnityEngine;

[CreateAssetMenu(fileName = "IntVariable", menuName = "Variables/Int")]
public class IntVariable : ScriptableObject
{
    [SerializeField]
    [Multiline]
    private string DeveloperDescription = "";

    [SerializeField]
    private int value;
    public int Value
    {
        get => value;
        set => this.value = value;
    }

    // public void SetValue(int value)
    // {
    //     Value = value;
    // }

    // public void SetValue(IntVariable value)
    // {
    //     Value = value.Value;
    // }

    public void ApplyChange(int amount)
    {
        Value += amount;
    }

    public void ApplyChange(IntVariable amount)
    {
        Value += amount.Value;
    }
}