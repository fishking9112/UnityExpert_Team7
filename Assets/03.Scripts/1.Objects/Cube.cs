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
        // 들기 
        isPickedUp= true;


        // 위치 조정
        // 부모오브젝트를 변경해주고 초기화 
        transform.SetParent(player.GetHoldTransform());
        transform.localPosition = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = true; //중력영향 X /물리적 충돌 X rigidbody 
        // player가지고있는 오브젝트 추가
        player.SetHeldObject(this);
    }
    public void Drop(Interaction player)
    {
        //놓기
        isPickedUp= false;

        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic= false;
        // player가지고있는 오브젝트 없다고 초기화
        player.ClearHeldObject();
    }

    public string GetInteractionPrompt()
    {
        return isPickedUp ? "들기" : "놓기";
    }



    public bool CanInteract(Interaction player)
    {
        // 플레이어가 이미 다른 물체를 들고 있는지 등의 조건 확인
        return player.CanPickUpObject();
    }


}
