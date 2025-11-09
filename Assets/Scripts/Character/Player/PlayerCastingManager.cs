using UnityEngine;
using GS;

public class PlayerCastingManager : CharacterCastingManager
{

    [HideInInspector] public PlayerManager player;
    public override void Start()
    {
        base.Start();
        player = GetComponent<PlayerManager>();
       
    }

    public override void Update()
    {
        base.Update();
       
    }

    public override void ClearCurrentStrategy()
    {
        base.ClearCurrentStrategy();
    }

    public override void SetCurrentStrategy(CastingStrategy strategy)
    {
        base.SetCurrentStrategy(strategy);
    }
   
}
