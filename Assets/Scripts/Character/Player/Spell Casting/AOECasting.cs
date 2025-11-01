using System;
using GS;
using UnityEngine;

[Serializable]
public class AOECasting : CastingStrategy
{
    [Header("AOE Settings")]
    public GameObject aoePrefab;
    public float effectRadius = 1f;
    public LayerMask layerMask;
    public float circleMoveSpeed = 10f;

    private GameObject previewInstance;
    private Transform cameraTransform;
    private Vector3 targetPosition;

    public override void OnPerformHold()
    {
       
    }

    public override void OnReleaseHold()
    {
       
    }

    public override void OnStartHold(Spell spell, CharacterCastingManager castingManager)
    {
        
    }

 
    
}

