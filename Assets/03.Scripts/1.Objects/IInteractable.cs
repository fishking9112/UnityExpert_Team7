using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    
    void Interact(PlayerController player); //��ȣ�ۿ��ϱ� 
    bool CanInteract(PlayerController player); //
    string GetInteractionPrompt();
}

public interface IPickable : IInteractable
{
    
    void PickUp(PlayerController player);
    void Drop(PlayerController player);
    bool IsPickedUp { get; }
}

public interface IPressable : IInteractable
{
    //void Press(PlayerController player);
    void Press(PlayerController player);
    bool IsPressed { get; }
}