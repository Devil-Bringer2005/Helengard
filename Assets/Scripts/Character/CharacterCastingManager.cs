using UnityEngine;
using GS;
public class CharacterCastingManager : MonoBehaviour
{

    protected CastingStrategy currentStrategy;
   
    public virtual void Start()
    {

    }
    public virtual void Update()
    {
       
    }

    public virtual void SetCurrentStrategy(CastingStrategy strategy)
    {
        currentStrategy = strategy;
    }

    public virtual void ClearCurrentStrategy()
    {
        currentStrategy = null;
    }
}
