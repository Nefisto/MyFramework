using UnityEngine;
using UnityEngine.Assertions;

public class RectTransformUtilities : ScriptableObject
{
    public void ResetPositionToZero(RectTransform rectTransform)
    {
        Assert.IsNotNull(rectTransform, 
            "You're trying to \"ResetPositionToZero\" of " + rectTransform.name + " but it don't have component a rectTransform");

        rectTransform.anchoredPosition = Vector2.zero;
    }
}