using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeController : MonoBehaviour
{
    [SerializeField] Transform rotationParent;
    [SerializeField] Transform cube;
    [Space]
    [SerializeField] float rollTime = .5f;
    [SerializeField] float fallAcceleration = 1;

    Vector2 movement = default;
    [HideInInspector] public bool CanInput = true;
    public bool CanFall = true;
    bool rotationParentIsInBottomLeft = true; //true = 0,0   false = 1,1

    void Update()
    {
        GetMovement();

        if (!Physics.Raycast(transform.position + new Vector3(.5f, .5f, .5f), Vector3.down, .6f) && CanInput && CanFall)
            StartCoroutine(DoFalling());

        if (movement != Vector2.zero && CanInput)
            StartCoroutine(DoMovement());
    }

    void GetMovement()
    {
        if (Input.GetKey(KeyCode.W))
            movement.y = 1;
        else if (Input.GetKey(KeyCode.S))
            movement.y = -1;
        else
            movement.y = 0;

        if (Input.GetKey(KeyCode.D))
            movement.x = 1;
        else if (Input.GetKey(KeyCode.A))
            movement.x= -1;
        else
            movement.x = 0;
    }

    IEnumerator DoMovement()
    {
        Vector2 input = movement;

        switch (input)
        {
            case Vector2 v when v.y == 1:
                input = new(0, 1);
                if (Physics.Raycast(transform.position + new Vector3(.5f, .5f, .5f), new(0, 0, 1), 1f)) yield break;
                rotationParentIsInBottomLeft = false;
                break;
            case Vector2 v when v.y == -1:
                input = new(0, -1);
                if (Physics.Raycast(transform.position + new Vector3(.5f, .5f, .5f), new(0, 0, -1), 1f)) yield break;
                rotationParentIsInBottomLeft = true;
                break;
            case Vector2 v when v.x == 1:
                input = new(1, 0);
                if (Physics.Raycast(transform.position + new Vector3(.5f, .5f, .5f), new(1, 0, 0), 1f)) yield break;
                rotationParentIsInBottomLeft = false;
                break;
            case Vector2 v when v.x == -1:
                input = new(-1, 0);
                if (Physics.Raycast(transform.position + new Vector3(.5f, .5f, .5f), new(-1, 0, 0), 1f)) yield break;
                rotationParentIsInBottomLeft = true;
                break;
        }
        CanInput = false;

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
                    rotationParent.rotation = Quaternion.Euler(Mathf.Lerp(0, 90, timer / rollTime), 0, 0);
                    break;
                case Vector2 v when v.y == -1:
                    rotationParent.rotation = Quaternion.Euler(Mathf.Lerp(0, -90, timer / rollTime), 0, 0);
                    break;
                case Vector2 v when v.x == 1: //roll around z
                    rotationParent.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(0, -90, timer / rollTime));
                    break;
                case Vector2 v when v.x == -1:
                    rotationParent.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(0, 90, timer / rollTime));
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

        CanInput = true;
    }

    IEnumerator DoFalling()
    {
        CanInput = false;
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, Vector3.down, out hit))
            yield break;
        Vector3 floorPosition = hit.point;

        float speed = 0;
        Vector3 startPosition = transform.position;
        while(transform.position.y > floorPosition.y)
        {
            speed += fallAcceleration * Time.deltaTime;
            transform.position -= new Vector3(0, speed, 0);
            if (transform.position.y <= floorPosition.y)
            {
                transform.position = floorPosition;
                break;
            }
            yield return null;
        }

        CanInput = true;
    }
}
