using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReceiver : MonoBehaviour
{
    public SharkController sharkController;

    public void OnMove(InputValue vectorValue)
    {
        Vector2 moveVector = vectorValue.Get<Vector2>();

        if (sharkController)
            sharkController.ReceiveInput(InputType.Movement, new object[] { moveVector });
    }
}
