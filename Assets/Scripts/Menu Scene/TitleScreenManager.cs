using UnityEngine;

namespace GS
{
    public class TitleScreenManager : MonoBehaviour
    {
        public void StartNewGame()
        {
            StartCoroutine(WorldGameSaveManager.instance.LoadNewGame());
        }
    }
}

