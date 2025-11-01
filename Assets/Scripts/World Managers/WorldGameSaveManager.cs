using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace GS
{
    public class WorldGameSaveManager : MonoBehaviour
    {
        public static WorldGameSaveManager instance;

        [SerializeField] int worldSceneIndex = 1;
        private void Awake()
        {   
            // Only one instance of game scene manager in the scene at a time

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

        public IEnumerator LoadNewGame()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);
            yield return null;
        }

        public int GetWorldIndex()
        {
            return worldSceneIndex;
        }

    }
}

