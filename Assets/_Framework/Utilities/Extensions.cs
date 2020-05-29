using UnityEngine;

public static class Extensions
{
    /// <summary>
    /// Compare (r, g, b) with an epsilon = 0.001f
    /// </sumary>
    public static bool IsEqualTo(this Color color, Color other)
        => Mathf.Abs(color.r - other.r) < 0.001f &&
           Mathf.Abs(color.g - other.g) < 0.001f &&
           Mathf.Abs(color.b - other.b) < 0.001f;


    // Custom editor n propertyDrawer
    /// <summary>
    /// Center a checkbox inside a given rect
    /// </sumary>
    public static Rect CenterCheckBox(this Rect rect)
    {
        var centeredPosition = rect.center - new Vector2(7.5f, 9f);

        return new Rect(centeredPosition, rect.size);
    }

    /// <summary>
    /// Give n percent of given rect
    /// </sumary>
    public static Vector2 ResizeX(this Rect rect, float percentX = 1f)
    {
        Vector2 newSize = rect.size;

        newSize.x *= percentX;

        return newSize;
    }
}
