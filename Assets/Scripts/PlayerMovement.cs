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
        anim.SetFloat("Direction", horizontalInput);
    }
}
