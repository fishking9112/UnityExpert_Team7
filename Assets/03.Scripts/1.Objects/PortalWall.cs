using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalWall : BasePortalAble
{
    public override Vector3 SummonPortal(Vector3 hitPosition)
    {
        Vector3 localHitPosition = transform.InverseTransformPoint(hitPosition);
        Vector3 summonPosition = new Vector3(localHitPosition.x, Mathf.Clamp(localHitPosition.y, 1.5f, 7.5f), Mathf.Clamp(localHitPosition.z, 0.8f, 5.2f));

        colliders[0].size = new Vector3(0.3f, 9f - (summonPosition.y + 1.5f), 1.6f);
        colliders[0].center = new Vector3(0,(9f + summonPosition.y + 1.5f) / 2f, summonPosition.z);
        colliders[1].size = new Vector3(0.6f, summonPosition.y - 1.5f, 1.6f);
        colliders[1].center = new Vector3(0, (summonPosition.y - 1.5f) / 2f, summonPosition.z);
        colliders[2].size = new Vector3(0.3f, 9f, summonPosition.z - 0.8f);
        colliders[2].center = new Vector3(0, 4.5f, (summonPosition.z - 0.8f) / 2f);
        colliders[3].size = new Vector3(0.3f, 9f, 6f - (summonPosition.z + 0.8f));
        colliders[3].center = new Vector3(0, 4.5f, (6f + summonPosition.z + 0.8f) / 2f);

        return transform.TransformPoint(summonPosition);
    }

}
