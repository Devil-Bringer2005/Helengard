using UnityEngine;

namespace GS
{
    public class CharacterSFXManager : MonoBehaviour
    {
        private AudioSource audioSource;

        protected virtual void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayDodgeForwardSFX()
        {
            audioSource.PlayOneShot(WorldAudioManager.instance.dodgeForwardSFX);
        }
    }

}
