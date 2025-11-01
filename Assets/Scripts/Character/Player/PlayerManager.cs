using UnityEngine;

namespace GS
{
    public class PlayerManager : CharacterManager
    {   
        public PlayerLocomotionManager playerLocomotionManager;
        public PlayerAnimatorManager playerAnimatorManager;
        public PlayerCastingManager playerCastingManager;
        public SpellCaster spellCaster;


        protected override void Awake()
        {
            base.Awake();

            // Player awake method
            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            playerCastingManager = GetComponent<PlayerCastingManager>();
            spellCaster = GetComponent<SpellCaster>();
            
        }

        protected override void Start()
        {
            base.Start();
            OnIntialize();
        }

        protected override void Update()
        {
            base.Update();

            // Handle Movement 
            playerLocomotionManager.HandleAllMovement();

        }
        protected override void LateUpdate()
        {
            base.LateUpdate();

            // Camera movement and Updation
            PlayerCamera.instance.HandleAllCameraAction();
        }

        public override void OnIntialize()
        {
            base.OnIntialize();
            
            // Instance setup
            PlayerCamera.instance.player = this;
            PlayerInputManager.instance.player = this;
        }
    }
}

