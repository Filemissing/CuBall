using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    public float movementSpeed;

    Rigidbody rb;
    Vector2 currentMoveDirection;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            currentMoveDirection.y = 1;
        else if (Input.GetKey(KeyCode.DownArrow))
            currentMoveDirection.y = -1;
        else
            currentMoveDirection.y = 0;

        if (Input.GetKey(KeyCode.RightArrow))
            currentMoveDirection.x = 1;
        else if (Input.GetKey(KeyCode.LeftArrow))
            currentMoveDirection.x = -1;
        else
            currentMoveDirection.x = 0;

        currentMoveDirection.Normalize();
        
        rb.linearVelocity = new Vector3(currentMoveDirection.x * movementSpeed, rb.linearVelocity.y, currentMoveDirection.y * movementSpeed);
    }
}
