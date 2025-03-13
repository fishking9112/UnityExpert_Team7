using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    
    void Interact(Interaction player); //상호작용하기 
    bool CanInteract(Interaction player); //
    string GetInteractionPrompt();
}

public interface IPickable : IInteractable
{
    
    void PickUp(Interaction player);
    void Drop(Interaction player);
    bool IsPickedUp { get; }
}

public interface IPressable : IInteractable
{
    //void Press(PlayerController player);
    void Press(Interaction player);
    bool IsPressed { get; }
}