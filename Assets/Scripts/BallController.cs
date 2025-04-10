using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    public float movementSpeed;

    Rigidbody rb;
    Vector2 moveDirection;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
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

        //Vector3 targetSpeed = moveDirection * movementSpeed;

        //Vector3 currentSpeed = rb.linearVelocity;

        //Vector3 difference = currentSpeed - MaxByAbs(currentSpeed, targetSpeed);

        //rb.AddForce(difference);

        rb.linearVelocity = new Vector3(moveDirection.x * movementSpeed, rb.linearVelocity.y, moveDirection.y * movementSpeed);
    }

    Vector3 MaxByAbs(Vector3 vector1, Vector3 vector2)
    {
        float x = Mathf.Max(Mathf.Abs(vector1.x), Mathf.Abs(vector2.x));
        float y = Mathf.Max(Mathf.Abs(vector1.y), Mathf.Abs(vector2.y));
        float z = Mathf.Max(Mathf.Abs(vector1.z), Mathf.Abs(vector2.z));

        return new Vector3(x, y, z);
    }
}
