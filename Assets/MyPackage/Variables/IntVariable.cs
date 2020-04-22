#pragma warning disable 0414

using UnityEngine;

[CreateAssetMenu(fileName = "IntVariable", menuName = "Variables/Int")]
public class IntVariable : BaseVariable<int>
{
    public void ApplyChange(int value)
        => Value += value;
}