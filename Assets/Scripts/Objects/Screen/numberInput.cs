using UnityEngine;
using TMPro;

public class numberInput : MonoBehaviour
{
    private bool active = false;
    private int value;
    private Color32 inactiveColour;
    private Color32 activeColour;

    private string defaultText;
    private int minLength;

    public thrusterScript thruster;

    private void Start()
    {
        activeColour = new Color32(255, 150, 0, 255);
        inactiveColour = new Color32(255, 70, 0, 255);

        defaultText = gameObject.GetComponent<TextMeshProUGUI>().text;
        minLength = defaultText.Length;
    }

    private void ClickOne()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = defaultText;
        Activate();
    }

    private void ClickTwo()
    {
        Activate();
    }

    private void Activate()
    {
        if (!active)
        {
            active = true;
            gameObject.GetComponent<TextMeshProUGUI>().color = activeColour;
        }
    }

    private void Update()
    {
        if(active)
        {
            Active();
        }
    }

    private void Active()
    {
        Type();
        Back();
        Deactivate();
    }

    private void Type()
    {
        for (int i = 0; i < 10; ++i)
        {
            if (Input.GetKeyDown("" + i))
            {
                gameObject.GetComponent<TextMeshProUGUI>().text += i.ToString();
            }
        }
    }

    private void Back()
    {
        string text = gameObject.GetComponent<TextMeshProUGUI>().text;
        if (Input.GetKeyDown("backspace") && text.Length > minLength)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = text.Remove(text.Length - 1, 1);
        }
    }

    private void Deactivate()
    {
        if(Input.GetKeyDown("return") || Input.GetKeyDown("enter"))
        {
            active = false;
            value = int.Parse(gameObject.GetComponent<TextMeshProUGUI>().text.Substring(7));
            gameObject.GetComponent<TextMeshProUGUI>().color = inactiveColour;
            if (name == "PowerGiven")
            {
                thruster.power = value;
            }
        }
    }

}
