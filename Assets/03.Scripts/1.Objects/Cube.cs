using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour,IPickable
{
    private bool isPickedUp = false;

    public bool IsPickedUp => isPickedUp;


    public void PickUp(PlayerController player)
    {
        isPickedUp= true;

        // ��� 

    }
    public void Drop(PlayerController player)
    {
        isPickedUp= false;

        //����

    }

    public string GetInteractionPrompt()
    {
        throw new System.NotImplementedException();
    }

    public void Interact(PlayerController player)
    {
        if (!isPickedUp)
            PickUp(player);
        else
            Drop(player);
    }

    public bool CanInteract(PlayerController player)
    {
        // �÷��̾ �̹� �ٸ� ��ü�� ��� �ִ��� ���� ���� Ȯ��
        throw new System.NotImplementedException();
    }


}
