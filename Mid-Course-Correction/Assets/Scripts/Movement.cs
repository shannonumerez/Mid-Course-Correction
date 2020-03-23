using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    //ATTACH TO PLAYH
    public string soundClipName;

    Vector3 playerPos;
    Vector3 targetDestination;
    public Vector3 direction;

    public float moveSpeed;

    public bool rightSideActive;
   // private bool movementLock;
    public bool stopped;


    public Movement partner;

    public Text clickSide;

    Rigidbody rb;



    void Start()
    {
        partner = partner.GetComponent<Movement>();
        rb = GetComponent<Rigidbody>();
        targetDestination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        print(rightSideActive);
        CheckMousePos();
        movePlayer();
        DisplayText();
        stopped = false;
    }

    void movePlayer()
    {
        rb.MovePosition(Vector3.MoveTowards(rb.position, targetDestination, moveSpeed * Time.deltaTime));

    }

    void CheckMousePos()        //set direction to new inidcated position.
    {
        if (Input.GetMouseButtonDown(0) && (!stopped && !partner.stopped))        //mouse left mouse down
        {
            Vector2 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);      //current mouse position = mouse as on current screen

            if ((currentMousePos.x > 0 && rightSideActive) || (currentMousePos.x < 0/*clicked on left side*/ && !rightSideActive))        // if mouse position/x is greater than 0 and isBlue is true OR mouse position.x is less than 0 and isBlue is false
            {
                targetDestination = currentMousePos;

                if (Mathf.Sign(targetDestination.x) == Mathf.Sign(-transform.position.x))          //if destination is a negative x then reflect < for second player
                {
                    targetDestination.x *= -1;
                }
            }
        }
    }

    void DisplayText()  //Display current active side
    {

        if (rightSideActive == true)
        {
            clickSide.text = "Right side active";
        }
        else
        {
            clickSide.text = "Left side active";
        }
    }

    public void changePartnerBool()
    {
        partner.rightSideActive = rightSideActive;  //partners bool is the same as current bool
    }

     void OnCollisionEnter(Collision collision)      //when a player collides with a wall, stop moving....set position to current location
    {
       // FindObjectOfType<AudioManager>().Play(soundClipName);
        targetDestination = transform.position;
        stopped = true;
    }
    private void OnCollisionExit(Collision collision)   //After collision, stopped bool is false and player can move again
    {
        stopped = false;
    }

}


