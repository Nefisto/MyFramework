using UnityEngine;

[CreateAssetMenu(fileName = "FloatVariable", menuName = "Variables/Float")]
public class FloatVariable : BaseVariable<float>
{
    public void ApplyChange(float value)
        => Value += value;
}