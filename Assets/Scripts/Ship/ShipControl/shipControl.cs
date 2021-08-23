using UnityEngine;

public class shipControl : MonoBehaviour
{
    private bool active = false;
    public float mass;

    private Transform components;

    private void Start()
    {
        mass = 1;
        components = transform.parent.Find("Components");
    }

    public void Activate()
    {
        active = true;
        foreach (Transform i in components)
        {
            i.SendMessage("SetActive", true, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void Deactivate()
    {
        active = false;
        active = true;
        foreach (Transform i in components)
        {
            i.SendMessage("SetActive", false, SendMessageOptions.DontRequireReceiver);
        }
    }
}
