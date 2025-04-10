using UnityEngine;

public class Fan : MonoBehaviour
{
    public float range;
    public float strength;
    public Vector3 halfExtents;

    private void FixedUpdate()
    {
        if (Physics.BoxCast(transform.position, halfExtents, transform.forward, out RaycastHit hit, Quaternion.identity, range))
        {
            if (hit.transform.CompareTag("CubePlayer"))
            {
                return;
            }

            if (hit.transform.CompareTag("BallPlayer"))
            {
                hit.rigidbody.AddForceAtPosition(transform.forward * strength, hit.point, ForceMode.Force);
            }
        }
    }
}
