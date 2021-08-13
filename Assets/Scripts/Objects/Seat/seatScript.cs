using UnityEngine;

public class seatScript : MonoBehaviour
{
    //script attached to seat objects
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

    private void Update()
    {
        if(sitting)
        {
            player.position = transform.position;
            InitiateExit();
            Exit();
        }
        else
        {
            Enter();
        }
    }

    private void InteractOne(GameObject origin)
    {
        if (!entering && !sitting)
        {
            Debug.Log("aa");
            //player has interacted with seat object
            //player
            player.parent = transform;
            playerScript.move = false;
            playerPosition = player.position;
            playerRotation = player.rotation;
            cameraRotation = playerCam.rotation;
            player.GetComponent<CapsuleCollider>().enabled = true;
            //enter
            entering = true;
            exiting = false;
            interpolationFraction = 0;
        }
    }

    private void Enter()
    {
        if (interpolationFraction < 1 && entering)
        {
            player.position = Vector3.Lerp(playerPosition, transform.position, interpolationFraction);
            player.rotation = Quaternion.Slerp(playerRotation, transform.rotation, interpolationFraction);
            playerCam.rotation = Quaternion.Slerp(cameraRotation, transform.rotation, interpolationFraction);
            interpolationFraction += Time.deltaTime;
        }
        if (interpolationFraction >= 1 && entering)
        {
            //finish interpolation
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
            interpolationFraction = 0;
            exiting = true;
        }
    }

    private void Exit()
    {
        if (interpolationFraction < 1 && exiting)
        {
            player.position = Vector3.Lerp(transform.position, exitPosition, interpolationFraction);
            interpolationFraction += Time.deltaTime;
        }
        if (interpolationFraction >= 1 && exiting)
        {
            //finish interpolation
            sitting = false;
            player.GetComponent<CapsuleCollider>().enabled = true;
            playerScript.move = true;
            exiting = false;
        }
    }
}
