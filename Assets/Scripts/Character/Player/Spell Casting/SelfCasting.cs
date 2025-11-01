using System;
using UnityEngine;

[Serializable]
public class SelfCasting : CastingStrategy
{

    public override void OnStartHold(Spell spell, CharacterCastingManager castingManager)
    {
        this.spell = spell;
        this.castingManager = castingManager;

        // for now i don't have a Idamageable to get from the player so i checking if it has playercasting manager script to identidy if it is player itself then we execute self casting 
        if(castingManager.transform.TryGetComponent<PlayerCastingManager>(out var target))
        {
            spell.HandleVFXEffects(castingManager.transform);
            Debug.Log("Self cast spell Casted on player");
        }
    }

    public override void OnPerformHold()
    {
        
    }

    public override void OnReleaseHold()
    {
        
    }

    
}
