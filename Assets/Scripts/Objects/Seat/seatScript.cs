using UnityEngine;

public class seatScript : MonoBehaviour
{
    //player
    private Transform player;
    private Transform playerCam;
    private playerMovement playerScript;
    private playerCam camScript;
    //interpolation
    private float interpolationFraction = 1;
    //enter
    private bool entering;
    private Vector3 playerPosition;
    private Quaternion playerRotation;
    private Quaternion cameraRotation;
    //exit
    private bool exiting;
    private Vector3 exitPosition;
    //seat
    private bool sitting = false;
    
    private void Start()
    {
        //player
        player = GameObject.Find("Player").transform;
        playerCam = player.GetChild(0).GetChild(0);
        playerScript = player.GetComponent<playerMovement>();
        camScript = playerCam.GetComponent<playerCam>();
    }

    public void InteractOne()
    {
        //player has interacted with seat object
        if (!sitting)
        {
            InitiateEnter();
        }
    }

    private void Update()
    {
        if(entering)
        {
            Enter();
        }

        if(sitting)
        {
            player.position = transform.position;
            InitiateExit();
        }

        if(exiting)
        {
            Exit();
        }
    }

    private void InitiateEnter()
    {
        //player
        player.parent = transform;
        playerScript.move = false;
        playerPosition = player.position;
        playerRotation = player.rotation;
        cameraRotation = playerCam.rotation;
        player.GetComponent<CapsuleCollider>().enabled = true;
        //enter
        entering = true;
        interpolationFraction = 0;
    }

    private void Enter()
    {
        //interpolate
        if (interpolationFraction < 1)
        {
            player.position = Vector3.Lerp(playerPosition, transform.position, interpolationFraction);
            player.rotation = Quaternion.Slerp(playerRotation, transform.rotation, interpolationFraction);
            playerCam.rotation = Quaternion.Slerp(cameraRotation, transform.rotation, interpolationFraction);
            interpolationFraction += Time.deltaTime;
        }

        //finish interpolation
        if (interpolationFraction >= 1 && entering)
        {
            //finish enter
            sitting = true;
            exitPosition = transform.position;
            entering = false;
        }
    }

    private void InitiateExit()
    {
        //determine exit position
        if(Input.GetKey(playerScript.forwardInput))
        {
            //forward exit
            exitPosition = transform.position + new Vector3(0, 0, 3);
        }
        if(Input.GetKey(playerScript.rightInput))
        {
            //right exit
            exitPosition = transform.position + new Vector3(3, 0, 0);
        }
        if(Input.GetKey(playerScript.leftInput))
        {
            //left exit
            exitPosition = transform.position + new Vector3(-3, 0, 0);
        }

        //begin exit
        if(exitPosition != transform.position && !exiting)
        {
            //begin interpolation
            interpolationFraction = 0;
            exiting = true;
        }
    }

    private void Exit()
    {
        //interpolate
        if (interpolationFraction < 1)
        {
            player.position = Vector3.Lerp(transform.position, exitPosition, interpolationFraction);
            interpolationFraction += Time.deltaTime;
        }

        //finish interpolation
        if (interpolationFraction >= 1)
        {
            //finish interpolation
            sitting = false;
            player.GetComponent<CapsuleCollider>().enabled = true;
            playerScript.move = true;
            exiting = false;
        }
    }
}
