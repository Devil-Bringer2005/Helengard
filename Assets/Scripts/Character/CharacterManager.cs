using UnityEngine;

namespace GS
{
    public class CharacterManager : MonoBehaviour
    {
        public CharacterController characterController;
        public Animator animator;

        [Header("Action Flags")]
        public bool isPerformingAction = false;
        public bool isJumping = false;
        public bool isGrounded = true;
        public bool applyRootMotion = false;
        public bool canRotate = true;
        public bool canMove = true;
        public bool isSprinting = true;
        public bool isCasting = false;


        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);

            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>(); 
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            animator.SetBool("isGrounded", isGrounded);
        }

        protected virtual void LateUpdate()
        {

        }

        public virtual void OnIntialize()
        {

        }
    }

}

