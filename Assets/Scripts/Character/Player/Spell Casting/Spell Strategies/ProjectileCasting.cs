using System;
using GS;
using TMPro;
using UnityEngine;

[Serializable]
public class ProjectileCasting : CastingStrategy
{
    [Header("Projectile Spell Settings")]
    [SerializeField] private float projectileSpeed;
    [SerializeField] private Vector3 projectileSpawn;


    // Instance variables
    private GameObject castInstance;
    private Vector3 basePosition;
    private GameObject projectileVFX;

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
        if (castInstance)
        {
            GameObject.Destroy(castInstance);
        }

        if (spell.spellProperties.spellVFX != null)
        {
            projectileVFX = GameObject.Instantiate(spell.spellProperties.spellVFX , basePosition + projectileSpawn, spell.spellProperties.spellVFX.transform.rotation);
            Rigidbody projectileRb = projectileVFX.GetComponent<Rigidbody>();
            projectileRb.AddForce(PlayerCamera.instance.transform.forward * projectileSpeed,ForceMode.Impulse);
            GameObject.Destroy(projectileVFX, spell.spellProperties.spellDuration);
        }
    }


   
}
