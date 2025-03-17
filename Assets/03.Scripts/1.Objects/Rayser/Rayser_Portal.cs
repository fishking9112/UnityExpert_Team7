using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayser_Portal : MonoBehaviour
{
    public GameObject Raybody;
    public GameObject ScaleDistance;
    public GameObject RayResult;

    private bool ChkRayser = false;
    private float maxDistance = 200f;
    private GameObject lastHitObj = null;


    private void Update()
    {
        if (ChkRayser)
        {
            RaycastHit hit;
            GameObject currentHitObj = null;

        }
        else
        {

        }
    }
}
