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

        // 들기 

    }
    public void Drop(Interaction player)
    {
        isPickedUp= false;

        //놓기

    }

    public string GetInteractionPrompt()
    {
        throw new System.NotImplementedException();
    }



    public bool CanInteract(Interaction player)
    {
        // 플레이어가 이미 다른 물체를 들고 있는지 등의 조건 확인
        throw new System.NotImplementedException();
    }


}
