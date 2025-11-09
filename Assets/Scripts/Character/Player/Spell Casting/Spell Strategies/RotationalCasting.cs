using System;
using UnityEngine;
using GS;

[Serializable]
public class RotationalCasting : CastingStrategy
{

    [Header("Rotational casting Settings")]
    [SerializeField] float minRotationAngle = 3f;
    [SerializeField] float maxRotationAngle = 10f;
    [SerializeField] float rotationSpeed = 10f;

    // Instance variables
    private GameObject castInstance;
    private GameObject spellInstance;
    private Vector3 basePosition;
    private float currentRotationAngle = 0f;

    public override void OnStartHold(Spell spell, CharacterCastingManager castingManager)
    {
        this.spell = spell;
        this.castingManager = castingManager;

        // Spawn a cast magic circle
        basePosition = PlayerCamera.instance.transform.position;

        if (castInstance == null && spell.spellProperties.castVFX != null)
        {
            castInstance = GameObject.Instantiate(spell.spellProperties.castVFX, castingManager.transform.position, Quaternion.identity);

        }
    }

    public override void OnPerformHold()
    {
        
    }


    public override void OnReleaseHold()
    {
       
    }

}
