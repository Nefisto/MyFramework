using UnityEngine;

[CreateAssetMenu(menuName = "Properties/Base")]
public class MasterProperty : ScriptableObject
{
    public float speed;
    public float runSpeed;

    [SerializeField]
    public MasterAction defaultAction;
    [SerializeField]
    public MasterEvent defaultEvent;
    [SerializeField]
    public MasterAction currentAction;
}
