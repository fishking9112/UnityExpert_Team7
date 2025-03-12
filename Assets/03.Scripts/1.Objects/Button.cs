using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour, IPressable
{
    private bool isPressed = false;
    public bool IsPressed => isPressed;

    [SerializeField]private UnityEvent onPress;
    [SerializeField]private UnityEvent onRelease;

    public void Press()
    {
        isPressed = !isPressed;
        if (isPressed)
        {
            onPress.Invoke();
        }
        else
        {
            onRelease.Invoke();
        }
    }

    public bool CanInteract()
    {
        // 버튼을 누를 수 있는 조건 확인
        throw new System.NotImplementedException();
    }

    public string GetInteractionPrompt()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        Press();
    }


}
