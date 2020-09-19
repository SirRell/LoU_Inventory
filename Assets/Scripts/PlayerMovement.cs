using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 1f;

    Animator anim;
    GameObject cam;
    float speed, horizontalInput, verticalInput;
    float direction;

    ///float directionDampTime = .25f;
    ///float rotationDegreePerSecond = 120f;
    ///AnimatorStateInfo stateInfo;
    ///int m_LocomotionId = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main.gameObject;
    }

    private void Update()
    {
            //if (anim)
            //{

            //    ///stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            //    ///Translate controls stick coordinates into world/cam/character space
            //    ///StickToWorldSpace(ref direction, ref speed);

            //    //Pull values from controller/keyboard

            //}
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");


        Vector3 cameraFacingDirection = transform.position - cam.transform.position;
        cameraFacingDirection.y = 0;
        Debug.DrawRay(cam.transform.position, cameraFacingDirection, Color.blue);
        Vector3 playerFacingDirection = transform.forward;
        Debug.DrawRay(transform.position + Vector3.up, playerFacingDirection, Color.blue);
        Vector3 directionPressDirection = (cameraFacingDirection.normalized * verticalInput + cam.transform.right.normalized * horizontalInput);
        Debug.DrawRay(transform.position, directionPressDirection, Color.green);


        if(horizontalInput != 0 || verticalInput != 0)
        {
            transform.forward = Vector3.Lerp(transform.forward, directionPressDirection, turnSpeed);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            anim.SetBool("Crouching", !anim.GetBool("Crouching"));
        }


    }

    private void FixedUpdate()
    {
        speed = new Vector2(horizontalInput, verticalInput).sqrMagnitude;
        anim.SetFloat("Speed", speed);
        //anim.SetFloat("Direction", horizontalInput);
    }




    //void FixedUpdate()
    //{
    //    if(IsInLocomotion() && ((direction >= 0 && horizontal >= 0) || (direction < 0 && horizontal < 0)))
    //    {
    //        Vector3 rotationAmount = Vector3.Lerp(Vector3.zero, new Vector3(0, rotationDegreePerSecond * 
    //            (horizontal < 0f ? -1f : 1f), 0f), Mathf.Abs(horizontal));
    //        Quaternion deltaRotation = Quaternion.Euler(rotationAmount * Time.deltaTime);
    //        transform.rotation *= deltaRotation;
    //    }

    //    anim.SetFloat("Speed", speed);
    //    anim.SetFloat("Direction", direction, directionDampTime, Time.deltaTime);
    //}

    //public void StickToWorldSpace(ref float directionOut, ref float speedOut)
    //{
    //    Vector3 transformDirection = transform.forward;
    //    Vector3 stickDirection = new Vector3(horizontal, 0, vertical);
    //    speedOut = stickDirection.sqrMagnitude;

    //    //Get camera rotation
    //    Vector3 cameraDirection = cam.transform.forward;
    //    cameraDirection.y = 0.0f; //Kill y
    //    Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, cameraDirection);

    //    //Convert joystick input in Worldspace coordinates
    //    Vector3 moveDirection = referentialShift * stickDirection;
    //    Vector3 axisSign = Vector3.Cross(moveDirection, transformDirection);

    //    float angleRootToMove = Vector3.Angle(transformDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);

    //    angleRootToMove /= 180f;
    //    directionOut = angleRootToMove * speed;
    //}

    //bool IsInLocomotion()
    //{
    //    return stateInfo.fullPathHash == m_LocomotionId;
    //}
}
