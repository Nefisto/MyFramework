using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button), typeof(Image))]
[RequireComponent(typeof(AudioSource))]
[ExecuteInEditMode]
public class FlexibleUIButton : MonoBehaviour, 
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public FlexibleButton skinData;

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
        button.transition = skinData.buttonTransition;
        button.targetGraphic = image;
    
        image.sprite = skinData.sprite;
        image.type = Image.Type.Sliced;

        button.spriteState = skinData.spriteState;
        button.colors = skinData.colorBlock;
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