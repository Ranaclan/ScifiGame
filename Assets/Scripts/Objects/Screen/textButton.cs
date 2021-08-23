using UnityEngine;

public class textButton : MonoBehaviour
{
    //menu change
    private Transform menuList;
    private GameObject currentMenu;
    private Transform newMenu;
    //ui
    private Transform panel;
    private screenScript screenScript;
    //components
    public thrusterScript thruster;

    private void Start()
    {
        menuList = transform.parent.parent;
        currentMenu = transform.parent.gameObject;
        newMenu = menuList.Find(name);

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
        newMenu.gameObject.SetActive(true);
        screenScript.ResetScroll();
    }

    private void Component()
    {
        if (thruster)
        {
            newMenu.GetComponent<thrusterMenu>().thruster = thruster;
        }
    }
}
