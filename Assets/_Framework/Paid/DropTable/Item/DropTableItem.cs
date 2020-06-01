using UnityEngine;

[System.Serializable]
public class DropTableItem
{
    public GameObject pool;

    public int weight;

    public float percent;

    public bool isMultiple;

    public IntRange amount;

    public bool isGuaranted;

    // Cach
    public int lastWeight;
}