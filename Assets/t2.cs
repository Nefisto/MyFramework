using UnityEngine;

[CreateAssetMenu(fileName = "t2", menuName = "")]
public class t2 : ScriptableObject, t1
{
    public void Play()
    {
        Debug.Log("OI");
    }
}