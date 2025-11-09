using System;
using UnityEngine;

[System.Serializable]
public abstract class CastingStrategy
{
    protected Spell spell;
    protected CharacterCastingManager castingManager;

    protected bool isCasting = false;
    public bool IsCasting => isCasting;

    public abstract void OnStartHold(Spell spell,CharacterCastingManager castingManager); // Method that gets called when we start hold of the spell input
    public abstract void OnPerformHold(); // Method that gets called when we are performing the hold (keep holding)
    public abstract void OnReleaseHold();// Method that gets called when we release the hold (spell input)
    public virtual void CastShape() { } // Additional add shapes for the magic circle (future)

}
