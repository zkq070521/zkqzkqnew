using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/PlayerControllerEvent")]
public class PlayerControllerEvent : ScriptableObject
{
    public UnityAction<PlayerController> OnEventRaised;

    public void RaiseEvent(PlayerController playerController)
    {
        OnEventRaised?.Invoke(playerController);
    }

}