using UnityEngine;
using TMPro;

public class screenScript : MonoBehaviour
{
    //interaction
    private BoxCollider objectCollider;
    public string interactInput = "g";
    private GameObject player;
    private GameObject hud;
    private bool lockMouse = false;
    private bool active = false;
    private float interactDelay = 0.05f;
    private float interactCounter = 0.05f;
    //scroll
    private Transform content;
    private float scrollValue;
    private float scrollSensitivity = -0.1f;
    private Vector3 defaultPosition;
    //system
    private Transform system;
    //components menu
    private Transform componentsMenu;
    private Transform componentsList;
    private int componentCount;
    private int slotCount;
    private float slotSpacing = -0.15f;

    private void Start()
    {
        //interaction
        objectCollider = gameObject.GetComponent<BoxCollider>();
        //scroll
        content = transform.GetChild(0);
        defaultPosition = content.position;
        //transform
        system = transform.parent.parent.parent;
        //components menu
        FillComponentsMenu();
    }

    private void Update()
    {
        if(active)
        {
            ScreenActive();
        }

        DelayInteractions();
    }

    private void ScreenActive()
    {
        Scroll();

        if (Input.GetKeyDown(interactInput))
        {
            lockMouse = true;
            Lock();
        }
    }

    private void InteractOne(GameObject origin)
    {
        if (!active)
        {
            if (!lockMouse)
            {
                player = origin;
                hud = player.transform.GetChild(2).gameObject;
                Unlock();
            }
            else
            {
                lockMouse = false;
            }
        }
    }

    private void DelayInteractions()
    {
        if (interactCounter < interactDelay)
        {
            interactCounter += Time.deltaTime;
        }
    }

    private void Lock()
    {
        if (interactCounter >= interactDelay)
        {
            //exit menu and lock cursor
            Cursor.lockState = CursorLockMode.Locked;
            objectCollider.enabled = true;
            hud.SetActive(true);
            active = false;
            ResetScroll();
            interactCounter = 0;
        }
    }

    private void Unlock()
    {
        if (interactCounter >= interactDelay)
        {
            //enter menu and unlock cursor
            Cursor.lockState = CursorLockMode.None;
            objectCollider.enabled = false;
            hud.SetActive(false);
            active = true;
            ResetScroll();
            interactCounter = 0;
        }
    }

    private void Scroll()
    {
        scrollValue = Input.mouseScrollDelta.y * scrollSensitivity;
        content.Translate(0, scrollValue, 0);
    }

    public void ResetScroll()
    {
        content.position = defaultPosition;

    }

    private void FillComponentsMenu()
    {
        componentsMenu = content.Find("Components");
        componentsList = system.Find("Components");
        componentCount = componentsList.childCount;

        FillSlots();
        ListSlots();
    }

    private void FillSlots()
    {
        for (int i = 0; i < componentCount; i++)
        {
            //fill ui slot with component
            Transform uiChild = componentsMenu.GetChild(i);
            Transform componentChild = componentsList.GetChild(i);
            textButton script = uiChild.GetComponent<textButton>();
            uiChild.GetComponent<TextMeshProUGUI>().text = componentChild.name;
            thrusterScript thrusterScript = componentChild.GetComponent<thrusterScript>();
            if(thrusterScript)
            {
                uiChild.name = "Thruster";
                script.thruster = thrusterScript;
            }
        }
    }

    private void ListSlots()
    {
        slotCount = -1;
        for (int i = 0; i < componentsMenu.childCount; i++)
        {
            Transform child = componentsMenu.GetChild(i);
            Collider slotCollider = child.GetComponent<BoxCollider>();
            string text = child.GetComponent<TextMeshProUGUI>().text;

            if (text != "")
            {
                //if slot has text it is in use
                slotCount++;
                slotCollider.enabled = true;
            }
            else
            {
                slotCollider.enabled = false;
            }

            //move slot down to make room for previous slot
            child.Translate(0, slotSpacing * slotCount, 0);
        }
    }
}
