using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textButton : MonoBehaviour
{
    //menu change
    private Transform menuList;
    private GameObject currentMenu;
    private GameObject newMenu;
    //ui
    private Transform panel;
    private screenScript screenScript;
    //components
    public component component;
    private string componentType;

    private void Start()
    {
        menuList = transform.parent.parent;
        currentMenu = transform.parent.gameObject;
        newMenu = menuList.Find(name).gameObject;

        panel = transform.parent.parent.parent;
        screenScript = panel.GetComponent<screenScript>();
    }

    private void ClickOne()
    {
        //check if button is in component list menu
        if(transform.parent.name == "Components")
        {
            Component();
        }
        //switch menu
        currentMenu.SetActive(false);
        newMenu.SetActive(true);
        screenScript.ResetScroll();
    }

    private void Component()
    {
        componentType = component.componentType;
        if (componentType == "Thruster")
        {
            newMenu.GetComponent<thrusterMenu>().thruster = component;
        }
    }
}
