using UnityEngine;

[CreateAssetMenu(fileName = "Spell Properties", menuName = "Spell/ New Spell")]
public class SpellProperties : ScriptableObject
{
    [Header("Spell Properties")]
    public string SpellName;     // Name of the casted spell
    public float spellDuration; // Duration of spell 

    [Header("Spell References")]
    public GameObject castVFX;     // Casting VFX
    public GameObject spellVFX;   // Spell VFX
    public AudioClip castSFX;    // Casting SFX
    public AudioClip spellSFX;  // Spell SFX


}