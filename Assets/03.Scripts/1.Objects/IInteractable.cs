using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    //bool CanInteract(PlayerController player);
    //void Interact(PlayerController player);
    void Interact(); //상호작용하기 
    bool CanInteract(); //
    string GetInteractionPrompt();
}

public interface IPickable : IInteractable
{
    //void PickUp(PlayerController player);
    //void Drop(PlayerController player);
    void PickUp();
    void Drop();
    bool IsPickedUp { get; }
}

public interface IPressable : IInteractable
{
    //void Press(PlayerController player);
    void Press();
    bool IsPressed { get; }
}