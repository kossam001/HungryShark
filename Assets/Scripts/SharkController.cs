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
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        transform.position += movementVector * speed * Time.deltaTime;

        // Check vertical bounds
        if (transform.position.y > Camera.main.orthographicSize ||
            transform.position.y < -Camera.main.orthographicSize)
        {
            transform.position = Vector3.Scale(new Vector3(1, -1), transform.position);
        }

        float cameraHorizontalSize = Camera.main.orthographicSize * Screen.width / Screen.height;

        if (transform.position.x > cameraHorizontalSize || 
            transform.position.x < -cameraHorizontalSize)
        {
            transform.position = Vector3.Scale(new Vector3(-1, 1), transform.position);
        }
    }
}
