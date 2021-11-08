using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float knockbackTime = 0.3f;
    public Rigidbody2D rb;
    public Camera cam;
    Vector2 movement;
    Vector2 mousePos;
    bool blockMovement = false;
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

    }

    void FixedUpdate()
    {
        if (!blockMovement)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            Vector2 Direction = mousePos - rb.position;
            float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Vector2 difference = (other.transform.position - transform.position).normalized;
            // Debug.Log(difference);
            // rb.AddForce(difference * 2, ForceMode2D.Impulse);
            StartCoroutine(Knockback(difference));
        }
    }

    private IEnumerator Knockback(Vector2 difference)
    {
        blockMovement = true;
        rb.freezeRotation = false;
        rb.AddForce(-difference * 2f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(knockbackTime);
        blockMovement = false;
        rb.freezeRotation = true;
        yield return null;
    }
}
