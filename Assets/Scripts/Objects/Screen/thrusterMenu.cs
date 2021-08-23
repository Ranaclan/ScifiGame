using UnityEngine;
using TMPro;

public class thrusterMenu : MonoBehaviour
{
    public thrusterScript thruster;
    private thruster thrusterObject;

    private Transform efficiency;
    private Transform maxPower;

    private void Start()
    {
        thrusterObject = thruster.thruster;
        efficiency = transform.Find("Efficiency");
        maxPower = transform.Find("MaxPower");
        efficiency.GetComponent<TextMeshProUGUI>().text = "Efficiency: " + thrusterObject.efficiency.ToString();
        maxPower.GetComponent<TextMeshProUGUI>().text = "Max Power: " + thrusterObject.maxPower.ToString();

        GiveComponents();
    }

    private void GiveComponents()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<numberInput>())
            {
                transform.GetChild(i).GetComponent<numberInput>().thruster = thruster;
            }
        }
    }
}
