using UnityEngine;
using UnityEngine.SceneManagement;

namespace StartMenu_Scripts
{
    public class StartScript : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
