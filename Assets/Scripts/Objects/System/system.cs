using UnityEngine;
using TMPro;

public class system : MonoBehaviour
{
    public float energy;
    private float maxEnergy = 50;

    private Transform energyDisplay;

    private void Start()
    {
        energyDisplay = transform.Find("Screen").Find("ScreenUI").Find("Panel").Find("Content").Find("MainMenu").Find("EnergyDisplay");
    }

    private void Update()
    {
        DisplayEnergy();
        CapEnergy();
    }

    private void DisplayEnergy()
    {
        energyDisplay.GetComponent<TextMeshProUGUI>().text = Mathf.Round(energy).ToString();
    }

    private void CapEnergy()
    {
        if(energy > maxEnergy)
        {
            energy = maxEnergy;
        }

        if(energy < 0)
        {
            energy = 0;
        }
    }
}
