using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPortalAble : MonoBehaviour
{
    [SerializeField] BoxCollider[] colliders;
    /*
    public Vector3 SummonPortal(Vector3 hitPosition)
    {
        Vector3 localHitPosition = transform.InverseTransformPoint(hitPosition);
        Vector3 summonPosition = new Vector3(localHitPosition.x,Mathf.Clamp(localHitPosition.y,1.5f,7.5f), Mathf.Clamp(localHitPosition.z, 0.8f, 5.2f));
        
        colliders[0].size = new Vector3(0.3f,(9f-(summonPosition.y + 1.5f)/2f,summonPosition.)

    }
    */
}
