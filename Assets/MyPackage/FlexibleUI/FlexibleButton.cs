using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static UnityEngine.UI.Selectable; // .Transition

[CreateAssetMenu(menuName = "Flexible UI/Button", fileName = "Flexible Button")]
public class FlexibleButton : ScriptableObject
{
    public enum ButtonType
    {
        Default,
        Confirm,
        Decline
    }

    [Header("Basic")]
    public Sprite sprite;
    public ButtonType buttonType;

    // Default
    public Transition defaultButtonTransition;
    public SpriteState defaultSpriteState;
    public ColorBlock defaultColorBlock = ColorBlock.defaultColorBlock;

    // Confirm
    public Transition confirmButtonTransition;
    public SpriteState confirmSpriteState;
    public ColorBlock confirmColorBlock = ColorBlock.defaultColorBlock;

    
    // Decline
    public Transition declineButtonTransition;
    public SpriteState declineSpriteState;
    public ColorBlock declineColorBlock = ColorBlock.defaultColorBlock;
}