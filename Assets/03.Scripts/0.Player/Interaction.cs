using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ī�޶� �߽ɿ��� ������ ��ü�� �Ǵ��ϱ� ���� ��ũ��Ʈ.
/// </summary>

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f; // �˻� �ֱ�
    private float _lastCheckTime;   // ������ üũ�� �ð�
    public float maxCheckDist;      // ���� �Ÿ�

    public LayerMask layerMask;     // �˻��� ���̾� ����ũ

    public GameObject curInteractionOBJ;    // ���� ���� OBJ

    private Camera _camera;                 // ī�޶� �߽��� ���� ī�޶�

    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        //���� �ð����� Ray ���
        if (Time.time - _lastCheckTime > checkRate)
        {
            _lastCheckTime = Time.time;

            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            //ī�޶� �������� ���� �߻� �ؼ� �浹 ������ ?
            if (Physics.Raycast(ray, out hit, maxCheckDist, layerMask))
            {
                if (hit.collider.gameObject != curInteractionOBJ)
                {
                    // ���� ��������
                    curInteractionOBJ = hit.collider.gameObject;
                    //curInteractable = hit.collider.GetComponent<IInteractable>();

                    //���⼭ �������� �۾����ֽø� �˴ϴ�.
                }
            }
            //�ƴϸ�
            else
            {
                // ������ �ʱ�ȭ
                curInteractionOBJ = null;
                //curInteractable = null;
            }
        }
    }
}
