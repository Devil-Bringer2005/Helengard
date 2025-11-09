using GS;
using UnityEngine;

[System.Serializable]
public class RapidProjectile : CastingStrategy
{
    [Header("Rapid Fire Settings")]
    [SerializeField] private float fireRate = 0.1f;     // seconds between shots
    [SerializeField] private float projectileSpeed = 25f;
    [SerializeField] private Transform firePoint;       // optional custom fire point (e.g., hand or staff)

    private GameObject projectilePrefab;
    private float fireTimer = 0f;
    private bool isFiring = false;

    public override void OnStartHold(Spell spell, CharacterCastingManager castingManager)
    {
        this.spell = spell;
        this.castingManager = castingManager;

        // assign projectile prefab from spell properties if not set
        if (projectilePrefab == null && spell.spellProperties.spellVFX != null)
            projectilePrefab = spell.spellProperties.spellVFX;

        isFiring = true;
        fireTimer = 0f;
    }

    public override void OnPerformHold()
    {
        if (!isFiring || projectilePrefab == null) return;

        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0f)
        {
            FireProjectile();
            fireTimer = fireRate; // reset timer for next bullet
        }
    }

    public override void OnReleaseHold()
    {
        isFiring = false;
    }

    private void FireProjectile()
    {
        // determine where to spawn from
        Vector3 spawnPos = firePoint != null
            ? firePoint.position
            : castingManager.transform.position + castingManager.transform.forward * 1f;

        Quaternion rotation = Quaternion.LookRotation(PlayerCamera.instance.transform.forward);

        GameObject projectile = GameObject.Instantiate(projectilePrefab, spawnPos, rotation);

        if (projectile.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.linearVelocity = PlayerCamera.instance.transform.forward * projectileSpeed;
        }

        // optional: handle VFX, sound, or pool-based spawning here
    }
}
