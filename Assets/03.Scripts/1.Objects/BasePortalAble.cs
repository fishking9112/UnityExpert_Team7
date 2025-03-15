using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePortalAble : MonoBehaviour
{
    [SerializeField]protected Collider mainCollider;
    [SerializeField]protected BoxCollider[] colliders;

    protected virtual void Start()
    {
        mainCollider.enabled = true;
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
    }
    public virtual void SetMainCollider(bool value)
    {
        mainCollider.enabled = value;
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = !value;
        }
    }
    public abstract Vector3 SummonPortal(Vector3 hitPosition);
}
