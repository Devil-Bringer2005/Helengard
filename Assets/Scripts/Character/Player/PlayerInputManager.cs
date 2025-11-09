using UnityEngine;
using UnityEngine.SceneManagement;

namespace GS
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager instance;
        public PlayerManager player;

        PlayerControls playerControls;

        [Header("Player Movement Input")]
        [SerializeField] Vector2 movementInput;
        public float verticalInput;
        public float horizontalInput;
        public float moveAmount;

        [Header("Camera Movement Input")]
        [SerializeField] Vector2 cameraInput;
        public float cameraVerticalInput;
        public float cameraHorizontalInput;

        [Header("Player Action Inputs")]
        [SerializeField] private bool dodgeInput = false;
        [SerializeField] private bool sprintInput = false;
        [SerializeField] private bool jumpInput = false;
        [SerializeField] private bool spellInput = false;
        [SerializeField] private int skillIndex = 0;


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {

            DontDestroyOnLoad(gameObject);

            // Subscribes to the event that checks if scene 
            SceneManager.activeSceneChanged += OnSceneChange;
        }

        private void Update()
        {
            HandleAllInputs();
        }
        private void HandleAllInputs()
        {
            HandleMovementInput();
            HandleCameraMovementInput();
            HandleDodgeInput();
            HandleSprintInput();
            HandleJumpInput();
            HandleSpellInput();
            HandleSkillSelectionInput();
        }

        private void OnSceneChange(Scene oldScene, Scene newScene)
        {
            // Used to enable the instance of this script according to scene change

            // If we are loading in to test scene (player world scene)
            //if (newScene.buildIndex == WorldGameSaveManager.instance.GetWorldIndex())
            //{
            //    instance.enabled = true;
            //}
            //// If we are loading into any other scene like menu scene
            //else
            //{
            //    instance.enabled = false;
            //}

        }
        // If we minimize the game player input manager stops
        private void OnApplicationFocus(bool focus)
        {
            if (enabled)
            {
                if (focus)
                {
                    playerControls.Enable();
                }
                else
                {
                    playerControls.Disable();
                }
            }
        }
        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();

                // Taken from player controls that is the input action C# GENERATED SCRIPT
                // Here we take from player controls script the action map and action to read input values from input device(like keyboard,mouse,controller)

                // MOVE INTERACTIONS
                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();

                // TAP INTERACTIONS
                playerControls.PlayerActions.Dodge.performed += i => dodgeInput = true;
                playerControls.PlayerActions.Jump.performed += i => jumpInput = true;

                // HOLD INTERACTIONS
                playerControls.PlayerActions.Sprint.performed += i => sprintInput = true;
                playerControls.PlayerActions.Sprint.canceled += i => sprintInput = false;

                playerControls.PlayerActions.MagicCircle.performed += i => spellInput = true;
                playerControls.PlayerActions.MagicCircle.canceled += i => spellInput = false;

                // D PAD INTERACTIONS
                playerControls.SkillSelection.SkillLeft.performed += i => skillIndex = 0;
                playerControls.SkillSelection.SkillRight.performed += i => skillIndex = 1;
                playerControls.SkillSelection.SkillUp.performed += i => skillIndex = 2;
                playerControls.SkillSelection.SkillDown.performed += i => skillIndex = 3;
            }

            playerControls.Enable();
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnSceneChange;
        }

        private void HandleMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;

            // Return the absolute number , (no negative number for move amount)
            moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

            // We clamp the values to 0.5 and 1 (walk and run) (optional clamp code)
            if (moveAmount <= 0.5 && moveAmount > 0)
            {
                moveAmount = 0.5f;
            }
            else if (moveAmount > 0.5 && moveAmount <= 1)
            {
                moveAmount = 1f;
            }

            if (player == null)
                return;

            // If we are not locked on
            // we are assigning horizontalValue as zero because when we aren't locked on we should only straf in forward direction (walk , run , sprint) 
            player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount, player.isSprinting);

            // If we are locked on then implement lock on animation logic

        }

        private void HandleCameraMovementInput()
        {
            cameraVerticalInput = cameraInput.y;
            cameraHorizontalInput = cameraInput.x;
        }

        private void HandleDodgeInput()
        {
            if (dodgeInput == true)
            {
                dodgeInput = false;

                // Return if menu is open or something like that
                // Perform a dodge action
                player.playerLocomotionManager.AttemptDodgeAction();
            }
        }

        private void HandleSprintInput()
        {
            if (sprintInput)
            {
                player.playerLocomotionManager.HandleSprinting();
            }
            else
            {
                player.isSprinting = false;
            }
        }

        private void HandleJumpInput()
        {
            if (jumpInput == true)
            {
                jumpInput = false;

                // Implement jump logic 
                // Menu on then no jumping allowed

                player.playerLocomotionManager.AttemptToJump();
            }
        }

        private void HandleSpellInput()
        {

            // When spell input starts

            if (spellInput && !player.isCasting)
            {
                player.isCasting = true; 
                player.canMove = false;
                player.spellCaster.OnCastStart();

            }

            if (spellInput && player.isCasting)
            {
                // Move or adjust the targeting circle }
                player.spellCaster.OnCastPerform();
            }

            // When spell input is released
            if (!spellInput && player.isCasting)
            {
                player.isCasting = false; 
                player.canMove = true;
                player.spellCaster.OnCastRelease();

            }
        }

        private void HandleSkillSelectionInput()
        {
            // here we use D pad to select the spell or skill we want to use
            player.spellCaster.SkillSelector(skillIndex);
        }



    }

}