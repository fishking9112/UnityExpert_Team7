using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour, IPickable
{
    private bool isPickedUp = false;
    public bool IsPickedUp => isPickedUp;
    [SerializeField] Animator animator;

    public void ShootAnimation(bool isRed)
    {
        if (isRed)
        {
            animator.SetTrigger("RedShoot");
        }
        else
        {
            animator.SetTrigger("BlueShoot");
        }
        
    }

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

        PortalGun portalGun = player.GetComponentInChildren<PortalGun>();
        portalGun.ActivatePortalGun(true);
        portalGun.SetGun(this);
       
        Transform equipCamera = player.GetEquipCameraTransform();
        if (equipCamera != null)
        {
            transform.SetParent(equipCamera); // 부모의위치 = equipCamera
            transform.localPosition = new Vector3(0.75f, -0.5f, 1.25f); //equipcamera에서 총이 보이는 위치
            transform.localRotation = Quaternion.Euler(8f, 270f, 0); //회전초기화(총을 발로차서 누워있는걸 들면 서있는상태로 바뀜)
            transform.localScale = new Vector3(20, 20, 20);
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<Collider>());
        }
        
        player.SetHeldObject(this);
        Debug.Log("PortalGun 장착됨!");

        this.gameObject.layer = 7;
        this.gameObject.transform.GetChild(0).gameObject.layer = 7;
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

