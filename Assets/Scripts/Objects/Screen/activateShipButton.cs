using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class activateShipButton : MonoBehaviour
{
    private bool active;

    private Transform ship;
    private shipControl shipScript;

    private Transform panel;
    private Color32 activeColour;
    private Color32 inactiveColour;

    private void Start()
    {
        active = false;

        ship = transform.parent.parent.parent.parent.parent.parent.Find("Ship");
        shipScript = ship.GetComponent<shipControl>();

        panel = transform.parent.parent.parent;
        activeColour = new Color32(0, 0, 70, 255);
        inactiveColour = new Color32(0, 0, 50, 255);
    }

    private void ClickOne()
    {
        if (active)
        {
            Deactivate();
        }
        else
        {
            Activate();
        }
    }

    private void Deactivate()
    {
        shipScript.Deactivate();
        panel.GetComponent<Image>().color = inactiveColour;
        transform.GetComponent<TextMeshProUGUI>().text = "Activate Ship";
        active = false;
    }

    private void Activate()
    {
        shipScript.Activate();
        panel.GetComponent<Image>().color = activeColour;
        transform.GetComponent<TextMeshProUGUI>().text = "Deactivate Ship";
        active = true;
    }
}
