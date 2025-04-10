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
        //print(movement);
    }

    void Update()
    {
        if (movement != Vector2.zero && canIpnut)
            StartCoroutine(DoMovement());
    }

    IEnumerator DoMovement()
    {
        canIpnut = false;
        Vector2 input = movement;

        switch (input)
        {
            case Vector2 v when v.y == 1:
                rotationParentIsInBottomLeft = false;
                input = new(0, 1);
                break;
            case Vector2 v when v.y == -1:
                rotationParentIsInBottomLeft = true;
                input = new(0, -1);
                break;
            case Vector2 v when v.x == 1:
                rotationParentIsInBottomLeft = false;
                input = new(1, 0);
                break;
            case Vector2 v when v.x == -1:
                rotationParentIsInBottomLeft = true;
                input = new(-1, 0);
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
            switch (input)
            {
                case Vector2 v when v.y == 1: //roll around x
                    rotationParent.rotation = Quaternion.Slerp(rotationParent.rotation, Quaternion.Euler(90, 0, 0), timer / rollTime);
                    break;
                case Vector2 v when v.y == -1:
                    rotationParent.rotation = Quaternion.Slerp(rotationParent.rotation, Quaternion.Euler(-90, 0, 0), timer / rollTime);
                    break;
                case Vector2 v when v.x == 1: //roll around z
                    rotationParent.rotation = Quaternion.Slerp(rotationParent.rotation, Quaternion.Euler(0, 0, -90), timer / rollTime);
                    break;
                case Vector2 v when v.x == -1:
                    rotationParent.rotation = Quaternion.Slerp(rotationParent.rotation, Quaternion.Euler(0, 0, 90), timer / rollTime);
                    break;
            }
            yield return null;
            timer += Time.deltaTime;
        }
        switch (input)
        {
            case Vector2 v when v.y == 1: //roll around x
                rotationParent.rotation = Quaternion.Euler(90, 0, 0);
                break;
            case Vector2 v when v.y == -1:
                rotationParent.rotation = Quaternion.Euler(-90, 0, 0);
                break;
            case Vector2 v when v.x == 1: //roll around z
                rotationParent.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case Vector2 v when v.x == -1:
                rotationParent.rotation = Quaternion.Euler(0, 0, 90);
                break;
        }

        cubePosition = cube.position;
        Quaternion cubeRotation = cube.rotation;
        rotationParent.rotation = Quaternion.identity;
        transform.position += new Vector3(input.x, 0, input.y);
        cube.position = cubePosition;
        cube.rotation = cubeRotation;

        yield return null;
        canIpnut = true;
    }
}
