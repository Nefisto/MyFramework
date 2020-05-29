using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using static FlexibleButton; // ButtonType

[RequireComponent(typeof(Button), typeof(Image))]
[RequireComponent(typeof(AudioSource))]
[ExecuteInEditMode]
public class FlexibleUIButton : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public FlexibleButton skinData;
    public ButtonType buttonType;

    [Header("Events")]
    public UnityEvent onClick;
    public UnityEvent onPointerEnter;
    public UnityEvent onPointerExit;

    Image image;
    Button button;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    // Called in editor mode
    public void Repaint()
    {
        button.targetGraphic = image;

        image.sprite = skinData.sprite;
        image.type = Image.Type.Sliced;

        switch (buttonType)
        {
            case ButtonType.Default:
                button.transition = skinData.defaultButtonTransition;
                button.spriteState = skinData.defaultSpriteState;
                button.colors = skinData.defaultColorBlock;
                break;

            case ButtonType.Confirm:
                button.transition = skinData.confirmButtonTransition;
                button.spriteState = skinData.confirmSpriteState;
                button.colors = skinData.confirmColorBlock;
                break;
            
            case ButtonType.Decline:
                button.transition = skinData.declineButtonTransition;
                button.spriteState = skinData.declineSpriteState;
                button.colors = skinData.declineColorBlock;
            break;
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        Repaint();
#endif
    }

    public void OnPointerEnter(PointerEventData eventData)
        => onPointerEnter.Invoke();

    public void OnPointerExit(PointerEventData eventData)
        => onPointerExit.Invoke();

    public void OnPointerClick(PointerEventData eventData)
        => onClick.Invoke();
}