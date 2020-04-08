#pragma warning disable 0414

using UnityEngine;

[CreateAssetMenu(fileName = "Vector2Variable", menuName = "Variables/Vector2Int")]
public class Vector2IntVariable : ScriptableObject
{
    [SerializeField]
    [Multiline]
    private string DeveloperDescription = "";

    [SerializeField]
    private Vector2Int value;
    public Vector2Int Value
    {
        get => value;
        set => this.value = value;
    }

    public int x
    {
        get => Value.x;
        set => this.value.y = value;
    }

    public int y
    {
        get => Value.y;
        set => this.value.y = value;
    }

    public void ApplyChange(Vector2Int amount)
    {
        Value += amount;
    }

    public void ApplyChange(Vector2IntVariable amount)
    {
        Value += amount.Value;
    }
}