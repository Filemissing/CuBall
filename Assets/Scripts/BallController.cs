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
    public void OnMove(InputValue context)
    {
        Vector2 direction = context.Get<Vector2>();
        currentMoveDirection = direction;
    }

    private void Update()
    {
        rb.linearVelocity = new Vector3(currentMoveDirection.x * movementSpeed, rb.linearVelocity.y, currentMoveDirection.y * movementSpeed);
    }
}
