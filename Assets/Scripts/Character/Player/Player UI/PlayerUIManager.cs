using UnityEngine;

namespace GS
{
    public class PlayerUIManager : CharacterUIManager
    {
        public static PlayerUIManager instance;
        protected override void Awake()
        {   
            base.Awake();

            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

        }

        protected override void Start()
        {   
            base.Start();
            DontDestroyOnLoad(gameObject);
        }

    }
}
