using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float MoveSpeed = 5f;
    public Rigidbody2D RB;
    public Camera cam;
    Vector2 Movement;
    Vector2 MousePos;
    // Update is called once per frame
    void Update()
    {
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");

        MousePos = cam.ScreenToWorldPoint(Input.mousePosition);

    }

    void FixedUpdate()
    {
        RB.MovePosition(RB.position + Movement * MoveSpeed * Time.fixedDeltaTime);
        Vector2 Direction = MousePos - RB.position;
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        RB.rotation = angle;
    }
}
