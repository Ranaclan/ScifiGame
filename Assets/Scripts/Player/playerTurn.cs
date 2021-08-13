using UnityEngine;

public class playerTurn : MonoBehaviour
{
    //script attached to player object for looking left and right
    //rotation
    public bool canRotate = true;
    private Transform rotationObject;
    private float rotation;
    public float sensitivity = 10f;
    //free head mode
    private Transform head;
    private Transform playerCam;
    private playerCam camScript;

    private void Start()
    {
        head = transform.GetChild(0);
        playerCam = head.GetChild(0);
        camScript = playerCam.GetComponent<playerCam>();
    }

    private void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            CalculateRotation();
        }
    }

    private void CalculateRotation()
    {
        rotation = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        SpecifyRotation();
    }

    void SpecifyRotation()
    {
        if(camScript.freeHeadMode)
        {
            FreeHeadRotate();
        }
        if(!camScript.freeHeadMode && canRotate)
        {
            Rotate();
        }
    }

    private void Rotate()
    {
        transform.Rotate(0, rotation, 0);
    }

    private void FreeHeadRotate()
    {
        head.Rotate(0, rotation, 0);
    }
}
