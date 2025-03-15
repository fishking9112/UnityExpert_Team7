using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalFloor : BasePortalAble
{
    public override Vector3 SummonPortal(Vector3 hitPosition)
    {
        Vector3 localHitPosition = transform.InverseTransformPoint(hitPosition);
        Vector3 summonPosition = new Vector3( Mathf.Clamp(localHitPosition.x, 0.8f, 5.2f), localHitPosition.y, Mathf.Clamp(localHitPosition.z, 1.5f, 4.5f));

        colliders[0].size = new Vector3(1.6f, 0.25f, 6f - (summonPosition.z + 1.5f));
        colliders[0].center = new Vector3(summonPosition.x, -0.125f, (6f + summonPosition.z + 1.5f) / 2f);
        colliders[1].size = new Vector3(1.6f, 0.25f,(summonPosition.z - 1.5f));
        colliders[1].center = new Vector3(summonPosition.x, -0.125f, (summonPosition.z - 1.5f) / 2f);
        colliders[2].size = new Vector3(6f-(summonPosition.x+0.8f),0.25f,6f);
        colliders[2].center = new Vector3((6f+(summonPosition.x+0.8f))/2f, -0.125f,3f);
        colliders[3].size = new Vector3(summonPosition.x - 0.8f, 0.25f, 6f);
        colliders[3].center = new Vector3((summonPosition.x - 0.8f) / 2f, -0.125f, 3f);

        return transform.TransformPoint(summonPosition);
    }
}
