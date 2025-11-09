using UnityEngine;

namespace GS
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        // These values gonna be taken from PlayerInputManager script 
        PlayerManager player;
        [HideInInspector] public float verticalMovement;
        [HideInInspector] public float horizontalMovement;
        [HideInInspector] public float moveAmount;

        [Header("Movement Settings")]
        private Vector3 moveDirection;
        private Vector3 targetRotationDirection;
        [SerializeField] private float walkingSpeed = 2f;
        [SerializeField] private float runningSpeed = 5f;
        [SerializeField] private float sprintingSpeed = 6f;
        [SerializeField] private float rotationSpeed = 15f;

        [Header("Dodge Settings")]
        [SerializeField] private Vector3 rollDirection;

        [Header("Jump Settings")]
        [SerializeField] private float jumpHeight = 4f;
        [SerializeField] private float jumpForwardSpeed = 5f;
        [SerializeField] private float freeFallSpeed = 2f;
        [SerializeField] private Vector3 jumpDirection;

        [Header("Magic Circle Settings")]
        [SerializeField] private float magicCirclemoveSpeed = 4f;
        [SerializeField] private float magicCircleRadius = 5f;
        
        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }

        protected override void Update()
        {
            base.Update();
        }
        public void HandleAllMovement()
        {
            // All the different movements in the game
            HandleGroundMovement();
            HandleRotation();
            HandleJumpingMovement();
            HandleFreeFallMovement();
        }
        private void GetVerticalAndHorizontalInputs()
        {   
            // Getting the vertical and horizontal input values from PlayerInputManger
            verticalMovement = PlayerInputManager.instance.verticalInput;
            horizontalMovement = PlayerInputManager.instance.horizontalInput;
            moveAmount = PlayerInputManager.instance.moveAmount;

            // Clamp movements (extra)
        }
        private void HandleGroundMovement()
        {   
            if(!player.canMove)
                return;

            GetVerticalAndHorizontalInputs();
            // Movement direction is based on player camera perspective (uses PlayerCamera script as camera transform) 
            moveDirection = PlayerCamera.instance.transform.forward * verticalMovement; // X Axis
            moveDirection = moveDirection + PlayerCamera.instance.transform.right * horizontalMovement; // Z Axis
            moveDirection.Normalize();
            moveDirection.y = 0f; // Y Axis

            if (player.isSprinting)
            {
                player.characterController.Move(moveDirection * sprintingSpeed * Time.deltaTime);
            }
            else
            {
                if (PlayerInputManager.instance.moveAmount > 0.5f)
                {
                    // Move player at running speed 
                    player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);

                }
                else if (PlayerInputManager.instance.moveAmount <= 0.5f)
                {
                    // Move player at walking speed
                    player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
                }
            }
        }

        private void HandleJumpingMovement()
        {
            if (player.isJumping)
            {
                player.characterController.Move(jumpDirection * jumpForwardSpeed * Time.deltaTime);
            }
        }

        private void HandleFreeFallMovement()
        {
            if (!player.isGrounded)
            {
                Vector3 freefallDirection = PlayerCamera.instance.transform.forward * verticalMovement;
                freefallDirection += PlayerCamera.instance.transform.right * horizontalMovement;
                freefallDirection.y = 0;

                player.characterController.Move(freefallDirection * freeFallSpeed * Time.deltaTime);
            }
        }

        private void HandleRotation()
        {
            if (!player.canRotate)
                return;

            targetRotationDirection = Vector3.zero;
            targetRotationDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
            targetRotationDirection = targetRotationDirection + PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
            targetRotationDirection.Normalize();
            targetRotationDirection.y = 0f;

            if(targetRotationDirection == Vector3.zero)
            {
                targetRotationDirection = transform.forward;
            }

            Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation,newRotation,rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
        }

        public void AttemptDodgeAction()
        {   
            if (player.isPerformingAction)
                return;

            // If we are moving and we attempt to dodge then we do dash dodge
            if(moveAmount > 0)
            {
                rollDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
                rollDirection += PlayerCamera.instance.transform.right * horizontalMovement;

                rollDirection.y = 0;

                Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
                player.transform.rotation = playerRotation;
                player.playerAnimatorManager.PlayTargetActionAnimation("Slide(Re)", true, true);
                // Peform a stylish dodge-dash animation
            }
            // If we are stationary and attempt to do dodge then we do backstep
            else
            {
                // perform a backstep dodge animation 
                player.playerAnimatorManager.PlayTargetActionAnimation("BackStep", true, true);
            }
            
        }

        public void AttemptToJump()
        {   
            // If we are performing an action we don't want to jump (will be changed when combat will be added)
            if (player.isPerformingAction)
                return;

            if (player.isJumping)
                return;

            if (!player.isGrounded)
                return;
            // One handed weapon jumping
            player.playerAnimatorManager.PlayTargetActionAnimation("Jump Start (1h)", false , false , true , true  );
            // Additionally if we are two handed weapon jumping (In future)

            player.isJumping = true;

            jumpDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
            jumpDirection += PlayerCamera.instance.cameraObject.transform.right * horizontalMovement; 

            jumpDirection.y = 0;

            if(jumpDirection != Vector3.zero)
            {   
                // If we are sprinting then full force of jum direction
                if (player.isSprinting)
                {
                    jumpDirection *= 1;
                }
                // If we are running then half the force of jump direction
                else if (moveAmount > 0.5f)
                {
                    jumpDirection *= 0.5f;
                }
                // If we are walking then quarter the force of jump direction
                else if (moveAmount <= 0.5f)
                {
                    jumpDirection *= 0.25f;
                }
            }
            
        }

        public void HandleSprinting()
        {
            if (player.isPerformingAction)
            {
                // Set sprinting to false
                player.isSprinting = false;
            }

            // If we are moving sprinting is true or else sprint bool is false
            if(moveAmount >= 0.5)
            {
                player.isSprinting = true;
            }
            else
            {
                player.isSprinting = false;
            }

            // If we are moving then sprinting is set to true

        }

        public void ApplyJumpVelocity()
        {
            // Apply jump velocity
            yVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityForce);
            Debug.Log("Jumping");
        }

    }
}
