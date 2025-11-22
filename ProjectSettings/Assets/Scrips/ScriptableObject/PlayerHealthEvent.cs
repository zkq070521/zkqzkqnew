using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/PlayerHealthEvent")]
public class PlayerHealthEvent : ScriptableObject
{
    public UnityAction<PlayerHealth> OnEventRaised;

    public void RaiseEvent(PlayerHealth playerHealth)
    {
        OnEventRaised?.Invoke(playerHealth);
    }
}
