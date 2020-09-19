using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    public Vector3 offset = new Vector3(0, .75f, 0);
    public float distanceUp = 2.5f, distanceAway = -2.7f;
    public float lookOffset = 1;

    Vector3 velocityCamSmooth = Vector3.zero;
    public float camSmoothDampTime = .1f;

    Transform target;
    Vector3 lookDir;
    Vector3 targetPos;
    
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    void LateUpdate()
    {
        Vector3 characterOffset = target.position + offset;

        //Calculate direction from camera to player, kill Y, and normalize to give a valid direction with unit magnitude
        lookDir = characterOffset - transform.position;
        lookDir.y = 0;
        lookDir.Normalize();



        targetPos = characterOffset + target.up * distanceUp + lookDir * distanceAway;
        SmoothPosition(transform.position, targetPos);
        transform.LookAt(target.position + Vector3.up * lookOffset);
    }

    void SmoothPosition(Vector3 fromPos, Vector3 toPos)
    {
        //Making a smooth transition between camera's current position and the position it wants to be in
        transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
    }
}
