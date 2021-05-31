using UnityEngine;

using sManager = UnityEngine.SceneManagement.SceneManager;

namespace Managers
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField] private int menuSceneBuildIndex = 0;

        public void ReLoadSync()
        {
            sManager.LoadScene(sManager.GetActiveScene().buildIndex);
        }
        public void LoadSync(string sceneName)
        {
            sManager.LoadScene(sceneName);
        }

        public void LoadSync(int sceneBuildIndex)
        {
            sManager.LoadScene(sceneBuildIndex);
        }

        public void LoadNextSceneSync()
        {
            sManager.LoadScene(sManager.GetActiveScene().buildIndex + 1);
        }
        public void LoadMenu()
        {
            this.LoadSync(menuSceneBuildIndex);
        }
    }
}
