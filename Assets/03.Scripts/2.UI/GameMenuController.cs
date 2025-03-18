using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameMenuCanvers 컨트롤러 입니다.
/// </summary>

public class GameMenuController : MonoBehaviour
{
    //CrossHair
    public CrossHair crossHair;
    //Sound

    private void Awake()
    {
        crossHair = GetComponentInChildren<CrossHair>();
    }

}
