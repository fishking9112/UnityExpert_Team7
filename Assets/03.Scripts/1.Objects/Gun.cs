using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IPickable
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
        isPickedUp = true;

       
        Transform equipCamera = player.GetEquipCameraTransform();
        if (equipCamera != null)
        {
            transform.SetParent(equipCamera); // 부모의위치 = equipCamera
            transform.localPosition = new Vector3(1f, -0.94f, 1.9f); //equipcamera에서 총이 보이는 위치
            transform.localRotation = Quaternion.identity; //회전초기화(총을 발로차서 누워있는걸 들면 서있는상태로 바뀜)

            GetComponent<Rigidbody>().isKinematic = true; //물리영향 안받기
        }

        player.SetHeldObject(this);
        Debug.Log("PortalGun 장착됨!");
    }

    public void Drop(Interaction player)
    {
        
    }

    public string GetInteractionPrompt()
    {
        return "PortalGun 장착";
    }

    public bool CanInteract(Interaction player)
    {
        return player.CanPickUpObject();
    }
}

