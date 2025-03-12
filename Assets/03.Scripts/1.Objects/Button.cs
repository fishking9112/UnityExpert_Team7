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
        // press�� �������� ���� 
        isPressed = !isPressed; //<< ��� ��� ������� ������ �𸣰��� 
        if (isPressed) // ���ȴٸ�
        {
            onPress.Invoke();
        }
        else //�ȴ��ȴٸ� 
        {
            onRelease.Invoke();
        }
    }

    public bool CanInteract(Interaction player)
    {
        // ��ư�� ���������� ������ ��ȣ�ۿ� ���� 
        return !isPressed;
    }

    public string GetInteractionPrompt()
    {
        return "������";
    }



}
