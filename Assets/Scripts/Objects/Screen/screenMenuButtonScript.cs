using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenMenuButtonScript : MonoBehaviour
{
    //menus
    private Transform menuSystem;
    private screenMenuScript menuScript;

    private void Start()
    {
        menuSystem = transform.parent.parent;
        menuScript = menuSystem.GetComponent<screenMenuScript>();
    }

    public void InteractOne()
    {
        menuScript.SwitchMenu(int.Parse(name));
    }
}
