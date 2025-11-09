using System;
using UnityEngine;


[Serializable]
public class Spell
{
    [Header("Spell properties")]
    [Space]
    public SpellProperties spellProperties;

    [Header("Casting Strategy")]
    [SerializeReference, SubclassSelector] CastingStrategy castingStrategy;

    [Header("Casting Control")]
    public bool canMoveWhileCasting = false;
    public bool canRotateWhileCasting = true;



    // Called when button is pressed
    public void StartCast(CharacterCastingManager castingManager)
    {
        if (castingStrategy != null)
        {
            castingStrategy.OnStartHold(this, castingManager);
        }
    }
    // Called while button is held
    public void PerformCast()
    {
        if (castingStrategy != null)
        {
            castingStrategy.OnPerformHold();
        }
    }
    // Called when button is released
    public void ReleaseCast()
    {
        if (castingStrategy != null)
        {
            castingStrategy.OnReleaseHold();
        }
    }

    public void HandleVFXEffects(Transform origin)
    {
        if (spellProperties.castVFX != null)
        {

            GameObject currentCastVFX = GameObject.Instantiate(spellProperties.castVFX, origin.position, origin.rotation);
            GameObject.Destroy(currentCastVFX , 0.2f);
        }

        if (spellProperties.spellSFX != null)
        {
            AudioSource.PlayClipAtPoint(spellProperties.spellSFX, origin.position);
        }

        if (spellProperties.spellVFX != null)
        {
            GameObject currentSpellVFX = GameObject.Instantiate(spellProperties.spellVFX, origin.position, Quaternion.identity);
            GameObject.Destroy(currentSpellVFX , spellProperties.spellDuration);
            
        }
        
    }

   
}

