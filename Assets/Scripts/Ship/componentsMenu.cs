using UnityEngine;
using TMPro;

public class componentsMenu : MonoBehaviour
{
    //components
    private Transform system;
    private Transform components;
    private int componentCount;

    private void Start()
    {
        system = transform.parent.parent.parent.parent.parent;
        components = system.Find("Components");
        componentCount = components.childCount;
    }
}
