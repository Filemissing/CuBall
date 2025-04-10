using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeController : MonoBehaviour
{
    [SerializeField] Transform rotationParent;
    [SerializeField] Transform cube;
    [Space]
    [SerializeField] float rollTime = .5f;

    Vector2 movement = default;
    bool canIpnut = true;
    bool rotationParentIsInBottomLeft = true; //true = 0,0   false = 1,1

    //rotationparent at relative 0,0 can move in -z and -x
    //rotationparent at relative 1,1 can move in +z and +x

    public void OnMove(InputValue inputValue)
    {
        movement = inputValue.Get<Vector2>();
    }

    void Update()
    {
        if (movement != Vector2.zero && canIpnut)
            StartCoroutine(DoMovement());
    }

    IEnumerator DoMovement()
    {
        canIpnut = false;

        switch (movement)
        {
            case Vector2 v when v.y == 1:
                rotationParentIsInBottomLeft = false;
                break;
            case Vector2 v when v.y == -1:
                rotationParentIsInBottomLeft = true;
                break;
            case Vector2 v when v.x == 1:
                rotationParentIsInBottomLeft = false;
                break;
            case Vector2 v when v.x == -1:
                rotationParentIsInBottomLeft = true;
                break;
        }

        //set rotationParent position
        Vector3 cubePosition = cube.position;
        if (rotationParentIsInBottomLeft)
            rotationParent.localPosition = new(0, 0, 0);
        else
            rotationParent.localPosition = new(1, 0, 1);
        cube.position = cubePosition;

        //rotate
        float timer = 0;
        while (timer < rollTime)
        {
            switch (movement)
            {
                case Vector2 v when v.y == 1: //roll around x
                    //rotationParent.rotation = Quaternion..sl
                    break;
                case Vector2 v when v.y == -1:

                    break;
                case Vector2 v when v.x == 1: //roll around z

                    break;
                case Vector2 v when v.x == -1:

                    break;
            }


            yield return null;
            timer += Time.deltaTime;
        }


        yield return null;
        canIpnut = true;
    }
}
