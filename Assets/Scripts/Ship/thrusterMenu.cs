using UnityEngine;
using TMPro;

public class thrusterMenu : MonoBehaviour
{
    public component thruster;

    private Transform efficiency;
    private Transform maxPower;

    private void Start()
    {
        efficiency = transform.Find("Efficiency");
        maxPower = transform.Find("MaxPower");
        efficiency.GetComponent<TextMeshProUGUI>().text = thruster.efficiency.ToString();
        maxPower.GetComponent<TextMeshProUGUI>().text = thruster.maxPower.ToString();
    }
}
