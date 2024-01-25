using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    

    public float jumpHeight = 0.0f;
    public float jumpSpeed = 0.0f;

    SoundManager soundManager = null;

    float playerStrafeSpeed = 0.0f;
    float playerJumpSpeed = 0.0f;
    float zMove = 0.0f;
    float moveLimit = 7.0f;

    bool isJump = false;
    bool isAirborne = false;
    bool isDead = false;
    bool isJumpScore = false;

    Rigidbody rbPlayer;

    Quaternion playerRot;

    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();

        playerRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetAxisRaw("Horizontal"));

        zMove = Input.GetAxisRaw("Horizontal") * playerStrafeSpeed;

        CheckJump();
        

        KeepMoveLimit();

    }

    /*private void FixedUpdate()
    {
        
    }*/

    public bool HasDied()
    {
        return isDead;
    }
    public bool HasJumpScore()
    { 
        return isJumpScore; 
    }

    public bool HasAirborneJump()
    {
        return isAirborne;
    }

    public float GetJumpHeight()
    {
        return jumpHeight;
    }

    public void InitialisePlayer(float speed, Material colour, SoundManager sound)
    {
        // for setting strafe speed
        playerStrafeSpeed = speed;
        //Debug.Log("Speed Set: " + playerStrafeSpeed.ToString());

        gameObject.GetComponent<MeshRenderer>().material = colour;

        soundManager = sound;
    }

    public void ResetJumpScore()
    {
        isJumpScore = false;
    }

    public void UpdateSpeed(float multiplier)
    {
        playerStrafeSpeed *= multiplier;
        jumpSpeed *= multiplier;

        /*playerStrafeSpeed += multiplier;
        jumpSpeed += multiplier;*/

        Debug.Log("Strafe Speed: " + playerStrafeSpeed + "\n" +
            "Jump Speed: " + jumpSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // check for if dead or if player has crossed a jump collision
        GameObject go = other.gameObject;

        if (go.CompareTag("Obstacle"))
        {
            //Debug.Log("Dead");
            isDead = true;

        }
        else if (go.CompareTag("JumpBox"))
        {
            if (isAirborne && !isJumpScore)
            {
                isJumpScore = true;

                soundManager.PlayJumpSound();
                //Debug.Log("Jump Score");
            }
            
        }
    }

    void KeepMoveLimit()
    {
        if (transform.position.z >= moveLimit)
        {
            Vector3 moveLimitPos = new Vector3(transform.position.x, transform.position.y, moveLimit);

            transform.SetPositionAndRotation(moveLimitPos, playerRot);
        }
        else if (transform.position.z <= -moveLimit)
        {
            Vector3 moveLimitPos = new Vector3(transform.position.x, transform.position.y, -moveLimit);

            transform.SetPositionAndRotation(moveLimitPos, playerRot);
        }
    }

    void CheckJump()
    {
        // if space is pressed while grounded, start jump
        if (Input.GetKey(KeyCode.Space) &&
         transform.position.y == 0)
        {
            isJump = true;
            playerJumpSpeed = jumpSpeed;
        }

        // if jumping, send player up
        if (isJump)
        {
            rbPlayer.velocity = new Vector3(rbPlayer.velocity.x, playerJumpSpeed, zMove);

            if (transform.position.y >= jumpHeight)
            {
                isJump = false;
            }
        }
        else
        {
            // fall down if in the air and jump is completed / player is not jumping
            if (transform.position.y > 0)
            {
                rbPlayer.velocity = new Vector3(rbPlayer.velocity.x, -playerJumpSpeed, zMove);
            }
            else if (transform.position.y < 0)
            {
                Vector3 positionYReset = new Vector3(transform.position.x, 0, transform.position.z);

                transform.SetPositionAndRotation(positionYReset, playerRot);
            }
            else
            {
                rbPlayer.velocity = new Vector3(rbPlayer.velocity.x, rbPlayer.velocity.y, zMove);
            }


        }

        // as long as y is higher than 0, they are airborne
        if (transform.position.y > 0)
        {
            
            if (!isAirborne)
            {
                isAirborne = true;
                //Debug.Log("Airborne: " + isAirborne.ToString());
            }




        }
        else
        {
            if (isAirborne)
            {
                //Debug.Log("Land");
                isAirborne = false;
            }
            
        }
    }
}
