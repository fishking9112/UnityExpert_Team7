using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour, IPressable
{
    private bool isPressed = false;
    public bool IsPressed => isPressed;

    public UnityEvent onPress;
    public UnityEvent onRelease;
    public void Interact(PlayerController player)
    {
        Press(player);
    }


    public void Press(PlayerController player)
    {
        // press된 상태인지 구분 
        isPressed = !isPressed;
        if (isPressed) // 눌렸다면
        {
            onPress.Invoke();
        }
        else //안눌렸다면 
        {
            onRelease.Invoke();
        }
    }

    public bool CanInteract(PlayerController player)
    {
        // 버튼이 눌려져있지 않을때 상호작용 가능 
        return !isPressed;
    }

    public string GetInteractionPrompt()
    {
        return "누르기";
    }



}
