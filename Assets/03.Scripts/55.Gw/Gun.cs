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
            transform.SetParent(equipCamera);
            transform.localPosition = new Vector3(1f, -0.4f, 2f);
            transform.localRotation = Quaternion.identity;

            GetComponent<Rigidbody>().isKinematic = true;
        }

        player.SetHeldObject(this);
        Debug.Log("PortalGun 장착됨!");
    }

    public void Drop(Interaction player)
    {
        isPickedUp = false;

        transform.SetParent(null);

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = false;  // 물리 영향 다시 적용

        player.ClearHeldObject();
        Debug.Log("PortalGun 내려놓음!");
    }

    public string GetInteractionPrompt()
    {
        return isPickedUp ? "놓기" : "집기";
    }

    public bool CanInteract(Interaction player)
    {
        return player.CanPickUpObject();
    }
}

