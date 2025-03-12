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
        // press�� �������� ���� 
        isPressed = !isPressed;
        if (isPressed) // ���ȴٸ�
        {
            onPress.Invoke();
        }
        else //�ȴ��ȴٸ� 
        {
            onRelease.Invoke();
        }
    }

    public bool CanInteract(PlayerController player)
    {
        // ��ư�� ���������� ������ ��ȣ�ۿ� ���� 
        return !isPressed;
    }

    public string GetInteractionPrompt()
    {
        return "������";
    }



}
