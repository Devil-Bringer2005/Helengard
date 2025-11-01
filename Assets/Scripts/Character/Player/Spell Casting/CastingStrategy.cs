using System;
using UnityEngine;

[System.Serializable]
public abstract class CastingStrategy
{
    protected Spell spell;
    protected CharacterCastingManager castingManager;

    protected bool isCasting = false;
    public bool IsCasting => isCasting;

    public abstract void OnStartHold(Spell spell,CharacterCastingManager castingManager); // Method that activates the spell 
    public abstract void OnPerformHold();
    public abstract void OnReleaseHold();
    public virtual void CastShape() { } // Additional add shapes for the magic circle (future)

}
