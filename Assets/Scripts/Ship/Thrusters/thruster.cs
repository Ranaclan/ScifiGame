using UnityEngine;

[CreateAssetMenu(fileName = "New Thruster")]
public class thruster : ScriptableObject
{
    public int efficiency;
    public int maxPower;
    public string componentType;
}
