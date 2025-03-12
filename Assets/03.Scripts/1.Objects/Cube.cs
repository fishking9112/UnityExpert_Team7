using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour,IPickable
{
    private bool isPickedUp = false;

    public bool IsPickedUp => isPickedUp;

    public void Interact(Interaction player)
    {
        if (!isPickedUp)
            PickUp(player);
        else
            Drop(player);
    }
    public void PickUp(Interaction player)
    {
        isPickedUp= true;

        // ��� 

    }
    public void Drop(Interaction player)
    {
        isPickedUp= false;

        //����

    }

    public string GetInteractionPrompt()
    {
        throw new System.NotImplementedException();
    }



    public bool CanInteract(Interaction player)
    {
        // �÷��̾ �̹� �ٸ� ��ü�� ��� �ִ��� ���� ���� Ȯ��
        throw new System.NotImplementedException();
    }


}
