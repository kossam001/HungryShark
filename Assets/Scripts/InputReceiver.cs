using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReceiver : MonoBehaviour
{
    public void OnMove(InputValue vectorValue)
    {
        Vector2 moveVector = vectorValue.Get<Vector2>();

        Debug.Log(moveVector);
    }
}
