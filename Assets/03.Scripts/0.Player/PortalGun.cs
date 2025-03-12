using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalGun : MonoBehaviour
{
    LayerMask canSummonPotalLayerMask;
    Portal redPortal;
    Portal bluePortal;
    public void OnShotRedPortal(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray,out RaycastHit hit, 200, canSummonPotalLayerMask))
        {

        }
    }
}
