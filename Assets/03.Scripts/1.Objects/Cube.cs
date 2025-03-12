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
        throw new System.NotImplementedException();
    }
    public void Drop()
    {
        isPickedUp= false;
        throw new System.NotImplementedException();
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
        // 플레이어가 이미 다른 물체를 들고 있는지 등의 조건 확인
        throw new System.NotImplementedException();
    }


}
