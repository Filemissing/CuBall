using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Button : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("CubePlayer") || collision.gameObject.CompareTag("BallPlayer"))
        {
            animator.SetTrigger("Pressed");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("CubePlayer") || collision.gameObject.CompareTag("BallPlayer"))
        {
            animator.SetTrigger("Released");
        }
    }
}
