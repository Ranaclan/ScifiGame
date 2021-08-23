using UnityEngine;

public class thrusterScript : MonoBehaviour
{
    public thruster thruster;
    public float power;
    public float powerUsed;

    private Transform system;
    private system systemScript;
    private Transform ship;
    private Rigidbody shiprb;
    private shipControl shipScript;
    private bool active = false;

    private void Start()
    {
        system = transform.parent.parent;
        systemScript = system.GetComponent<system>();
        ship = system.Find("Ship");
        shiprb = ship.GetComponent<Rigidbody>();
        shipScript = ship.GetComponent<shipControl>();
    }

    private void Update()
    {
        if(active)
        {
            if(Input.GetKey("e"))
            {
                SubtractPower();
                ApplyForce();
            }
        }
    }

    private void SetActive(bool value)
    {
        active = value;
    }

    private void SubtractPower()
    {
        if (systemScript.energy >= power)
        {
            powerUsed = power;
            systemScript.energy -= power;
        }

        if (systemScript.energy < power)
        {
            powerUsed = systemScript.energy;
            systemScript.energy = 0;
        }
    }

    private void ApplyForce()
    {
        if (powerUsed > 0)
        {
            Vector3 addedVelocity = transform.forward * (Mathf.Sqrt(2 * powerUsed) / shipScript.mass);
            shiprb.AddForce(addedVelocity * shipScript.mass);
        }
    }
}
