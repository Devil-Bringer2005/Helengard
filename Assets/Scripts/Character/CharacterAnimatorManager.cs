using UnityEngine;
using UnityEngine.TextCore.Text;

namespace GS
{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        CharacterManager character;
        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }
        public void UpdateAnimatorMovementParameters(float horizontalValue,float verticalValue,bool isSprinting)
        {     
            float horizontalAmount = horizontalValue;
            float verticalAmount = verticalValue;

            if (isSprinting)
            {
                verticalAmount = 2;
            }
            character.animator.SetFloat("Horizontal", horizontalAmount, 0.1f, Time.deltaTime);
            character.animator.SetFloat("Vertical", verticalAmount, 0.1f, Time.deltaTime);
        }

        public virtual void PlayTargetActionAnimation(string targetAnimation 
            ,bool isPerformingAction = true
            ,bool applyRootMotion = true 
            ,bool canRotate = false
            ,bool canMove = false)
        {   
            character.applyRootMotion = applyRootMotion;
            character.animator.CrossFade(targetAnimation, 0.2f);
            // Can be used to stop character from doing certain actions (action checker)
            // if the isPerforming flag is true in character then we don't allow animation to play and wise versa
            character.isPerformingAction = isPerformingAction;
            character.canMove = canMove;
            character.canRotate = canRotate;
        }
    }
}
