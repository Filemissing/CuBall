using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(BoxCollider))]
public class Door : MonoBehaviour
{
    new MeshRenderer renderer;
    BoxCollider boxCollider;
    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public void Activate()
    {
        renderer.enabled = false;
        boxCollider.enabled = false;
    }
    public void DeActivate()
    {
        renderer.enabled = true;
        boxCollider.enabled = true;
    }
}
