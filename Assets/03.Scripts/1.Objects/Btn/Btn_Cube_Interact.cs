using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn_Cube_Interact : MonoBehaviour
{
    private ButtonObj button;


    private void Start()
    {
        button = GetComponent<ButtonObj>();
    }

    private void OnCollisionEnter(Collision collision)
    {
         Check_Press(collision, 1);

    }
    private void OnCollisionExit(Collision collision)
    {
        Check_Press(collision,2);
    }

    private void Check_Press(Collision collision, int Pressednum)
    {
        if (collision.gameObject.CompareTag("Cube") || collision.gameObject.CompareTag("Player"))
        {
            if (button != null)
            {
                if (Pressednum == 1)
                {
                    button.ChkedPress();
                }
                else if (Pressednum == 2)
                {
                    button.ChkOutPress();
                }
            }
        }
        
    }
}
