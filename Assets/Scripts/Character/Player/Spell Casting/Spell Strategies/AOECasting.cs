using System;
using GS;
using UnityEngine;

[Serializable]
public class AOECasting : CastingStrategy
{
    [Header("AOE Settings")]
    public float moveSpeed = 10f;
    public float maxRange = 10f;
    public float effectRadius = 1f;
    public LayerMask groundMask;

    // Instance variables
    private GameObject castInstance;
    private Transform cameraTransform;
    private Vector3 targetPosition;

    public override void OnStartHold(Spell spell, CharacterCastingManager castingManager)
    {
        this.spell = spell;
        this.castingManager = castingManager;
        cameraTransform = PlayerCamera.instance.transform;

        // Start with a point in front of the camera
        targetPosition = cameraTransform.position + cameraTransform.forward * 5f;

        // Spawn a cast magic circle
        if (castInstance == null && spell.spellProperties.castVFX != null)
            castInstance = GameObject.Instantiate(spell.spellProperties.castVFX, targetPosition, Quaternion.identity);
    }

    public override void OnPerformHold()
    {
        // Input-based direction
        Vector3 moveDirection = cameraTransform.forward * PlayerInputManager.instance.verticalInput +
                                cameraTransform.right * PlayerInputManager.instance.horizontalInput;

        moveDirection.y = 0f;
        moveDirection.Normalize();

        // Move the target within allowed range
        targetPosition += moveDirection * moveSpeed * Time.deltaTime;
        targetPosition = cameraTransform.position + (targetPosition - cameraTransform.position).normalized *
                         Mathf.Min(Vector3.Distance(cameraTransform.position, targetPosition), maxRange);

        // Ground snapping using raycast (maybe remove)
        if (Physics.Raycast(targetPosition + Vector3.up * 5f, Vector3.down, out RaycastHit hit, 10f, groundMask))
        {
            targetPosition = hit.point;
        }

        // Update cast position
        if (castInstance != null)
            castInstance.transform.position = targetPosition;
    }

    public override void OnReleaseHold()
    {
        if (castInstance)
            GameObject.Destroy(castInstance);

        // Spawn final effect
        GameObject currentAoeVFX = GameObject.Instantiate(spell.spellProperties.spellVFX, targetPosition, Quaternion.identity);
        GameObject.Destroy(currentAoeVFX, 5f);
    }
}

