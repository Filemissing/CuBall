using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    public float speed;
    bool isMoving;
    bool moveBack;

    public void Activate()
    {
        moveBack = false;
        isMoving = true;
    }
    public void DeActivate()
    {
        moveBack = true;
        isMoving = true;
    }

    void Start()
    {
        transform.position = startPos;
    }
    void Update()
    {
        if (isMoving)
        {
            Vector3 direction = moveBack ? startPos - transform.position : endPos - transform.position;
            direction.Normalize();

            transform.Translate(direction *  speed);

            bool isDone = moveBack ? transform.position == endPos : transform.position == startPos;
            if (isDone)
            {
                isMoving = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BallPlayer"))
        {
            other.transform.parent = transform;
        }
        else if (other.CompareTag("CubePlayer"))
        {
            other.transform.parent.parent.parent = transform;
            other.transform.parent.parent.GetComponent<CubeController>().CanFall = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BallPlayer"))
        {
            other.transform.parent = null;
        }
        else if (other.CompareTag("CubePlayer"))
        {
            other.transform.parent.parent.parent = null;
            other.transform.parent.parent.GetComponent<CubeController>().CanFall = true;
        }
    }
}
