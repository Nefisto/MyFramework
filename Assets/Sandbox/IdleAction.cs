using UnityEngine;


[CreateAssetMenu(fileName = "IdleAction", menuName = "Car", order = 0)]
public class IdleAction : ScriptableObject
{
    public int points;

    public void Save()
    {
        PlayerPrefs.SetInt("points", points);
    }

    public void Load()
    {
        points = PlayerPrefs.GetInt("points", 0);
    }
}