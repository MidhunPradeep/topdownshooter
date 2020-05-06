using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float sprintBoost = 50f;
    public bool sprinting = false;

    private Rigidbody2D rb;

    private Vector2 movement;
    private Vector3 lookDirection;
    private float sprintSpeed;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprintSpeed = (sprintBoost / 100) * moveSpeed;
    }


    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Sprint"))
        {
            moveSpeed += sprintSpeed;
            sprinting = true;
        }
        if (Input.GetButtonUp("Sprint"))
        {
            moveSpeed -= sprintSpeed;
            sprinting = false;
        }

        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }


    private void FixedUpdate()
    {
        rb.velocity = Vector2.zero;

        if (movement != Vector2.zero)
        {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }

        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

}
