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
    private MonoBehaviour[] scripts;
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
        if(Input.GetKeyDown(interactInput))
        {
            if(Physics.Raycast(transform.position, transform.forward, out interactObject, 5))
            {
                //check if object has scripts attached to it
                scripts = interactObject.collider.GetComponents<MonoBehaviour>();
                if (scripts.Length > 0)
                {
                    interactObject.collider.SendMessage("InteractOne");
                }

                //check if parent has scripts
                scripts = interactObject.collider.GetComponentsInParent<MonoBehaviour>();
                if (scripts.Length > 0)
                {
                    interactObject.collider.transform.parent.SendMessage(interactObject.collider.name);
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
            initialFov = freeHeadFov;
            finalFov = fov;
            tempPlayerRotation = head.rotation;
            tempCamRotation = transform.rotation;
        }
        else
        {
            //initiate
            initialFov = fov;
            finalFov = freeHeadFov;
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
