using UnityEngine;

namespace GS
{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera instance;
        public PlayerManager player;
        public Camera cameraObject;
        [SerializeField] Transform cameraPivotTransform;

        [Header("Camera Settings")]
        [SerializeField] private float cameraSmoothSpeed; //  Bigger the number more the time it takes the camera to reach the target
        [SerializeField] private float leftAndRightRotationSpeed = 220f; // Rotation speed for camera's left and right direction
        [SerializeField] private float upAndDownRotationSpeed = 220f; // Rotation speed for camera's up and down direction
        [SerializeField] private float minimumPivot = -30f; // Lowest point the camera is able to look 
        [SerializeField] private float maximumPivot = 60f; // Highest point the camera is able to look
        [SerializeField] private float cameraCollisionRadius = 0.2f;
        [SerializeField] private LayerMask collideWithLayers;

        [Header("Camera Values")]
        private Vector3 cameraVelocity;
        private Vector3 cameraObjectPosition; // Used for camera collisions
        [SerializeField] private float leftAndRightLookAngle;
        [SerializeField] private float upAndDownLookAngle;
        private float cameraZPosition;// Used for camera collisions
        private float targetCameraZPosition;// Used for camera collisions
        private float cameraCollisionLerpSpeed = 0.2f;

        private void Awake()
        {
            if(instance == null)
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
            cameraZPosition = cameraObject.transform.localPosition.z;
        }

        public void HandleAllCameraAction() 
        { 
            if(player != null)
            {
                HandleFollowTarget();
                HandleRotations();
                HandleCollisions();
            }
        }

        private void HandleFollowTarget()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position,player.transform.position,ref cameraVelocity,cameraSmoothSpeed * Time.deltaTime);
            transform.position = targetCameraPosition;
        }

        private void HandleRotations()
        {
            // Disclaimer
            // If we lock on , we force rotation towards target
            // Else we rotate normally

            // Normal Camera Rotation (Unlocked)
            
            //Rotate Left or Right based on Input (Keyboard and Joystick) 
            leftAndRightLookAngle += (PlayerInputManager.instance.cameraHorizontalInput * leftAndRightRotationSpeed) * Time.deltaTime;
            //Rotate Up or Down based on Input (Keyboard and Joystick) 
            upAndDownLookAngle += (PlayerInputManager.instance.cameraVerticalInput * upAndDownRotationSpeed) * Time.deltaTime;
            // Clamp the up and down look angle between a min and max pivot (float value)
            upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle,minimumPivot, maximumPivot); // We are clamping up and look angle so that we can restrict the camera going inside the ground and other stuff

            Vector3 cameraRotation = Vector3.zero;
            Quaternion targetRotation;

            // Rotate this gameobject left or right
            cameraRotation.y = leftAndRightLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            transform.rotation = targetRotation;

            // Rotate this pivot gameobject up and down
            cameraRotation = Vector3.zero;
            cameraRotation.x = upAndDownLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            cameraPivotTransform.localRotation = targetRotation;

        }

        private void HandleCollisions()
        {
            targetCameraZPosition = cameraZPosition;
            RaycastHit hit;
            Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
            direction.Normalize();

            // We are doing a physics sphere cast to check for collisions
            if(Physics.SphereCast(cameraPivotTransform.position , cameraCollisionRadius , direction , out hit , Mathf.Abs(targetCameraZPosition) , collideWithLayers))
            {   
                // If there is , we get the distance
                float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
                // We then equate our target camera z position to the following
                targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadius);
            }

            // If our target position is less than our collision radius , we subtract our collision radius (Make it snap back)
            if(Mathf.Abs(targetCameraZPosition) < cameraCollisionRadius)
            {
                targetCameraZPosition = -cameraCollisionRadius;
            }

            // We then apply final position using a lerp over time of cameraCollisionLerpSpeed
            cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, cameraCollisionLerpSpeed);
            cameraObject.transform.localPosition = cameraObjectPosition;
        }
    }

}
