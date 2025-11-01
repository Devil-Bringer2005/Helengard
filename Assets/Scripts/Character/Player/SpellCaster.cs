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

    // Public Methods used along with player inputmanager for activating Spell methods like (StartCast , PerformCast , ReleaseCast)
   

    public void OnCastStart()
    {
        currentSpell = Spells[0];
        currentSpell.StartCast(castingManager);
    }

    public void OnCastPerform()
    {
        currentSpell = Spells[0];
        currentSpell.PerformCast();
    }

    public void OnCastRelease()
    {
        currentSpell = Spells[0];
        currentSpell.ReleaseCast();
    }


}
