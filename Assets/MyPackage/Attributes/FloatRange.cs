using UnityEngine;

[System.Serializable]
public class FloatRange 
{
    public float minValue = 1f;
    public float maxValue = 1f;

    public float GetRandom()
    {
        return Random.Range(minValue, maxValue);
    }
}
