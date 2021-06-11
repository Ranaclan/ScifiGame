using UnityEngine;

public class playerCam : MonoBehaviour
{
    //script attached to player camera object for looking up and down
    //camera
    private Camera cam;
    private int fov = 60;
    private int freeHeadFov = 50;
    //player
    private Transform head;
    private Transform player;
    //rotation
    private float rotation;
    public float sensitivity = 10f;
    private float rotationAddition;
    //interaction
    public string interactInput = "g";
    private RaycastHit interactObject;
    //free head mode
    public bool freeHeadMode = false;
    public bool freeHeadControl = true;
    private bool freeHeadToggle = false;
    private float freeHeadToggleFraction = 1;
    private float freeHeadToggleSpeed = 4;
    private int initialFov;
    private int finalFov;
    private Quaternion originalPlayerRotation;
    private Quaternion originalCamRotation;
    private Quaternion tempPlayerRotation;
    private Quaternion tempCamRotation;

    private void Start()
    {
        //camera
        Cursor.lockState = CursorLockMode.Locked;
        cam = GetComponent<Camera>();
        //player
        head = transform.parent;
        player = head.parent;

    }

    private void Update()
    {
        CalculateRotation();
        Interact();
        FreeHeadMode();
        ToggleFreeHeadMode();
    }

    private void CalculateRotation()
    {
        rotation = Input.GetAxis("Mouse Y") * -sensitivity * Time.deltaTime;
        rotationAddition += rotation;
        ClampRotation();
    }

    private void ClampRotation()
    {
        if(rotationAddition >= 90f)
        {
            rotationAddition = 90;
        }
        else if(rotationAddition <= -90f)
        {
            rotationAddition = -90;
        }
        else
        {
            Rotate();
        }
    }

    private void Rotate()
    {
        transform.Rotate(rotation, 0, 0);
    }

    private void Interact()
    {
        if(Input.GetKey(interactInput))
        {
            if(Physics.Raycast(transform.position, transform.forward, out interactObject, 5))
            {
                if(interactObject.collider.gameObject.GetComponent<interactScript>() != null)
                {
                    interactObject.collider.gameObject.GetComponent<interactScript>().interaction = 1;
                }
            }
        }
    }

    private void FreeHeadMode()
    {
        if(Input.GetKey("mouse 2") && freeHeadControl)
        {
            FreeHeadModeInitiate();
        }
    }

    public void FreeHeadModeInitiate()
    {
        freeHeadToggle = true;
        freeHeadToggleFraction = 0;
        if (freeHeadMode)
        {
            //deactivate
            initialFov = 50;
            finalFov = 60;
            tempPlayerRotation = head.rotation;
            tempCamRotation = transform.rotation;
        }
        else
        {
            //initiate
            initialFov = 60;
            finalFov = 50;
            originalPlayerRotation = player.rotation;
            originalCamRotation = transform.rotation;
        }
    }

    private void ToggleFreeHeadMode()
    {
        if(freeHeadToggleFraction < 1)
        {
            //fov
            cam.fieldOfView = Mathf.Lerp(initialFov, finalFov, freeHeadToggleFraction);
            //undo rotation on exit
            if (freeHeadMode)
            {
                head.rotation = Quaternion.Slerp(tempPlayerRotation, originalPlayerRotation, freeHeadToggleFraction);
                transform.rotation = Quaternion.Slerp(tempCamRotation, originalCamRotation, freeHeadToggleFraction);
            }

            freeHeadToggleFraction += Time.deltaTime * freeHeadToggleSpeed;
        }
        if(freeHeadToggleFraction >= 1 && freeHeadToggle)
        {
            //finish toggle
            freeHeadMode = !freeHeadMode;
            freeHeadToggle = false;
        }
    }
}
