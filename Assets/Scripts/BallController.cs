using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    public float movementSpeed;
    public bool canMove;

    Rigidbody rb;
    Vector2 moveDirection;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!canMove) return;

        if (Input.GetKey(KeyCode.UpArrow))
            moveDirection.y = 1;
        else if (Input.GetKey(KeyCode.DownArrow))
            moveDirection.y = -1;
        else
            moveDirection.y = 0;

        if (Input.GetKey(KeyCode.RightArrow))
            moveDirection.x = 1;
        else if (Input.GetKey(KeyCode.LeftArrow))
            moveDirection.x = -1;
        else
            moveDirection.x = 0;

        moveDirection.Normalize();

        rb.linearVelocity = new Vector3(moveDirection.x * movementSpeed, rb.linearVelocity.y, moveDirection.y * movementSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!canMove) canMove = true;
    }
}
