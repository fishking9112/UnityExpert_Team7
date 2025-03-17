using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    [SerializeField] Image crossHairRed;
    [SerializeField] Image crossHairBlue;

    private void Start()
    {
        crossHairRed.enabled = false;
        crossHairBlue.enabled = false;
    }
    public void CanShotRed(bool value)
    {
        crossHairRed.enabled = value;
    }
    public void CanShotBlue(bool value)
    {
        crossHairBlue.enabled = value;
    }
}
