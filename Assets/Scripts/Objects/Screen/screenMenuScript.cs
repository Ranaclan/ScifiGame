using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenMenuScript : MonoBehaviour
{
    //menus
    public int activeMenuIndex;
    private int previousMenuIndex;

    private void Start()
    {
        //menus
        activeMenuIndex = 0;
        previousMenuIndex = 0;
    }

    public void SwitchMenu(int index)
    {
        //find new menu
        previousMenuIndex = activeMenuIndex;
        activeMenuIndex = index;
        //activate new menu
        transform.GetChild(previousMenuIndex).gameObject.SetActive(false);
        transform.GetChild(activeMenuIndex).gameObject.SetActive(true);

    }
}
