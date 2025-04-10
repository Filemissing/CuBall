using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField] Transform rotationParent;
    [SerializeField] Transform cube;

    //rotationparent at relative 0,0 can move in -z and -x
    //rotationparent at relative 1,1 can move in +z and +x

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
