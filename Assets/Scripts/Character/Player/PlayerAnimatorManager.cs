using UnityEngine;

namespace GS
{
    public class PlayerAnimatorManager : CharacterAnimatorManager
    {
        PlayerManager player;

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }
        private void OnAnimatorMove()
        {
            if (player.applyRootMotion)
            {
                // Applies the velocity and rotation of the Animation (that is playing) to the Player
                Vector3 velocity = player.animator.deltaPosition;
                player.characterController.Move(velocity);
                //player.transform.rotation = player.animator.deltaRotation;
            }
        }
    }

}
