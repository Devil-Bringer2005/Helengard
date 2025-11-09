using GS;
using Unity.VisualScripting;
using UnityEngine;


public class SpellCaster : MonoBehaviour
{
    public PlayerManager player;
    public Spell[] Spells;
    public Spell currentSpell;
    public CharacterCastingManager castingManager;


    private void Awake()
    {
        player = GetComponent<PlayerManager>();
        castingManager = GetComponent<CharacterCastingManager>();
    }

    // Test implementation of skill selector using D pad

    public void SkillSelector(int index)
    {
        if (index < Spells.Length)
        {
            currentSpell = Spells[index]; // Select the skill based on index

        }
    }

    // Public Methods used along with player inputmanager for activating Spell methods like (StartCast , PerformCast , ReleaseCast)
    // For now we only cast the firt (0th) spell in the list 
    public void OnCastStart()
    { 
        currentSpell.StartCast(castingManager);
    }

    public void OnCastPerform()
    {   
        currentSpell.PerformCast();
    }

    public void OnCastRelease()
    {
        currentSpell.ReleaseCast();
        currentSpell = null;
    }

    // also i need to manage what the current spell is in each state of input
}
