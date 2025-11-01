using UnityEngine;
using GS;

public class PlayerCastingManager : CharacterCastingManager
{

    [HideInInspector] public PlayerManager player;
    protected override void Start()
    {
        base.Start();
        player = GetComponent<PlayerManager>();
       
    }

    protected override void Update()
    {
        base.Update();
       
    }

   
}
