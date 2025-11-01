using GS;
using UnityEngine;

public class CharacterLocomotionManager : MonoBehaviour
{
    CharacterManager character;

    [Header("Ground Check and Jumping")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckSphereRadius = 1f;
    [SerializeField] protected float gravityForce = -6f;
    [SerializeField] protected Vector3 yVelocity; // force at which the character is pulled up or down (jumping or falling)
    [SerializeField] protected float groundedYVelocity = -20f; // the force at which the player is sticking to the ground while they grounded
    [SerializeField] protected float fallStartYVelocity = -5f; // force at which player falls (Incremental velocity value)
    protected bool fallingVelocityHasBeenSet = false;
    protected float inAirTimer = 0;

    protected virtual void Awake()
    {
        DontDestroyOnLoad(this);
        character = GetComponent<CharacterManager>();
    }

    protected virtual void Update()
    {
        HandleGroundCheck();

        if (character.isGrounded)
        {   
            // If we are not attempting to jump
            if(yVelocity.y < 0)
            {
                inAirTimer = 0;
                fallingVelocityHasBeenSet = false;
                yVelocity.y = groundedYVelocity;
            }
        }
        else
        {   
            // If we are not jumping and falling velocity hasn't been set yet
            if (!character.isJumping && !fallingVelocityHasBeenSet)
            {
                fallingVelocityHasBeenSet = true;
                yVelocity.y = fallStartYVelocity;

            }

            inAirTimer = inAirTimer + Time.deltaTime;
            character.animator.SetFloat("inAirTimer", inAirTimer);

            yVelocity.y += gravityForce * Time.deltaTime;
            character.characterController.Move(yVelocity * Time.deltaTime);
        }
        character.characterController.Move(yVelocity * Time.deltaTime);
    }

    protected virtual void HandleGroundCheck()
    {
        character.isGrounded = Physics.CheckSphere(character.transform.position, groundCheckSphereRadius, groundLayer);
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(character.transform.position, groundCheckSphereRadius);
    }
}
