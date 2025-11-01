using UnityEngine;

namespace GS
{
    public class WorldAudioManager : MonoBehaviour
    {
        public static WorldAudioManager instance;

        [Header("Audio Clips")]
        public AudioClip dodgeForwardSFX;

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
        }
    }

}
