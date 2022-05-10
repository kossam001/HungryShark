using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType
{
    Movement,
}

public class SharkController : MonoBehaviour
{
    public float speed;

    private Vector3 movementVector;

    public void ReceiveInput(InputType inputID, object[] inputArguments)
    {
        switch (inputID)
        {
            case InputType.Movement:
                movementVector = (Vector2)inputArguments[0];                

                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += movementVector * speed * Time.deltaTime;
    }
}
