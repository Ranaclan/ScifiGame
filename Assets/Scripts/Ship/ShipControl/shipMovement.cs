using UnityEngine;

public class shipMovement : MonoBehaviour
{
    public bool active;

    private void Update()
    {
        if (active)
        {
            if(Input.GetKey("e"))
            {
                transform.Translate(2, 0, 0);
            }
        }
    }
}
