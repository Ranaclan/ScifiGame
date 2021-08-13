using UnityEngine;

[CreateAssetMenu(fileName = "New Thruster")]
public class component : ScriptableObject
{
    public int efficiency;
    public int maxPower;
    public string componentType;
}
