using UnityEngine;

public static partial class Extensions
{
    /// <summary>
    /// Compare (r, g, b) with an epsilon = 0.001f
    /// </sumary>
    public static bool IsEqualTo(this Color color, Color other)
        => Mathf.Abs(color.r - other.r) < 0.001f &&
           Mathf.Abs(color.g - other.g) < 0.001f &&
           Mathf.Abs(color.b - other.b) < 0.001f;
}
