using UnityEngine;
using GS;
public class CharacterCastingManager : MonoBehaviour
{

    protected CastingStrategy currentStrategy;
   
    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {
       
    }

    protected virtual void SetCurrentStrategy(CastingStrategy strategy)
    {
        currentStrategy = strategy;
    }

    protected virtual void ClearCurrentStrategy()
    {
        currentStrategy = null;
    }
}
