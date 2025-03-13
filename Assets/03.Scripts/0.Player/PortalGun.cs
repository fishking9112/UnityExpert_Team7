using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalGun : MonoBehaviour
{
    [SerializeField] LayerMask canSummonPotalLayerMask;
    [SerializeField] Portal redPortal;
    [SerializeField] Portal bluePortal;
    [SerializeField] GameObject redPortalPrefab;
    [SerializeField] GameObject bluePortalPrefab;

    private void Awake()
    {
        if(redPortal == null)
        {
            redPortal = Instantiate(redPortalPrefab).GetComponent<Portal>();
        }
        if (bluePortal == null)
        {
            bluePortal = Instantiate(bluePortalPrefab).GetComponent<Portal>();
        }
        redPortal.SetOtherPortal(bluePortal);
        bluePortal.SetOtherPortal(redPortal);

        redPortal.gameObject.SetActive(false);
        bluePortal.gameObject.SetActive(false);
    }
    public void OnShotRedPortal(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

            if (Physics.Raycast(ray, out RaycastHit hit, 2000, canSummonPotalLayerMask))
            {
                
                Debug.Log($"맞은물체 {hit.transform.name}");
                redPortal.SummonPortal(hit.point, hit.normal, hit.collider);
            }
        }
        
    }
    public void OnShotBluePortal(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

            if (Physics.Raycast(ray, out RaycastHit hit, 2000, canSummonPotalLayerMask))
            {
                bluePortal.SummonPortal(hit.point, hit.normal, hit.collider);
            }
        }
        
    }
}
