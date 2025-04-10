using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Button : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public bool isActive;
    public UnityEvent onPress;
    public UnityEvent onRelease;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CubePlayer") || other.gameObject.CompareTag("BallPlayer"))
        {
            animator.SetTrigger("Pressed");
            isActive = true;
            onPress.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CubePlayer") || other.gameObject.CompareTag("BallPlayer"))
        {
            animator.SetTrigger("Released");
            isActive = false;
            onRelease.Invoke();
        }
    }
}
