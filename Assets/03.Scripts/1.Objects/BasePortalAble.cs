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
    public virtual void SetMainCollider(bool value)     //true면 1개의 벽(메인) 콜라이더만 활성화,  false면 메인끄고 나머지4개의 콜라이더 활성화
    {
        mainCollider.enabled = value;
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = !value;
        }
    }
    public abstract Vector3 SummonPortal(Vector3 hitPosition);
}
