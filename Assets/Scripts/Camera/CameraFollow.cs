using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;

    public Vector2 minBoundary;
    public Vector2 maxBoundary;

    private Rigidbody rb;
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;

    private GlobalReference globalReference;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        globalReference = GameObject.Find("Global Reference").GetComponent<GlobalReference>();
    }


    void FixedUpdate()
    {
        if (globalReference.playerIsAlive)
        {
            targetPosition = new Vector3(target.position.x, target.position.y, rb.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minBoundary.x, maxBoundary.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minBoundary.y, maxBoundary.y);
            if (targetPosition != rb.position)
            {
                rb.MovePosition(Vector3.SmoothDamp(rb.position, targetPosition, ref velocity, smoothTime));
            }
        }
    }
}
