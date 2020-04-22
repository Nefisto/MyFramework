using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName= "Flexible UI/Button", fileName= "Flexible Button")]
public class FlexibleButton : ScriptableObject
{
    [Header("Basic")]
    public Sprite sprite;
    public Selectable.Transition buttonTransition;

    public SpriteState spriteState;
    public ColorBlock colorBlock = ColorBlock.defaultColorBlock;
}