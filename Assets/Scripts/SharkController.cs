using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType
{
    Movement,
}

public class SharkController : MonoBehaviour
{
    public void ReceiveInput(InputType inputID, object[] inputArguments)
    {
        switch (inputID)
        {
            case InputType.Movement:
                Vector2 movementVector = (Vector2)inputArguments[0];

                Debug.Log(movementVector);

                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
