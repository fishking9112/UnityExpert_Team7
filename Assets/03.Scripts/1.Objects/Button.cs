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
    public void Interact(Interaction player)
    {
        Press(player);
    }


    public void Press(Interaction player)
    {
        // press된 상태인지 구분 
        isPressed = !isPressed; //<< 잠시 대기 사용할지 안할지 모르겟음 
        if (isPressed) // 눌렸다면
        {
            onPress.Invoke();
        }
        else //안눌렸다면 
        {
            onRelease.Invoke();
        }
    }

    public bool CanInteract(Interaction player)
    {
        // 버튼이 눌려져있지 않을때 상호작용 가능 
        return !isPressed;
    }

    public string GetInteractionPrompt()
    {
        return "누르기";
    }



}
