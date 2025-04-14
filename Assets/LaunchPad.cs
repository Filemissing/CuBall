using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    public Vector3 velocity;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BallPlayer"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(velocity);
            collision.gameObject.GetComponent<BallController>().canMove = false;
        }
    }
}
