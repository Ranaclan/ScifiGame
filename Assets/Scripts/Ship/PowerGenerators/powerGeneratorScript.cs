using UnityEngine;

public class powerGeneratorScript : MonoBehaviour
{
    public powerGenerator powerGenerator;

    private Transform system;
    private system systemScript;

    private void Start()
    {
        system = transform.parent.parent;
        systemScript = system.GetComponent<system>();
    }

    private void Update()
    {
        systemScript.energy += powerGenerator.power * Time.deltaTime;
    }
}
