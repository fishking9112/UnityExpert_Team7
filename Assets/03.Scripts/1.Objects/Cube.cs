using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour,IPickable
{
    private bool isPickedUp = false;

    public bool IsPickedUp => isPickedUp;


    public void PickUp()
    {
        isPickedUp= true;

        // ��� 

    }
    public void Drop()
    {
        isPickedUp= false;

        //����

    }

    public string GetInteractionPrompt()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        if (!isPickedUp)
            PickUp();
        else
            Drop();
    }

    public bool CanInteract()
    {
        // �÷��̾ �̹� �ٸ� ��ü�� ��� �ִ��� ���� ���� Ȯ��
        throw new System.NotImplementedException();
    }


}
