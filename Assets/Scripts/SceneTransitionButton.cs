using UnityEngine;
using UnityEngine.SceneManagement; // シーン操作をするために必要

public class SceneTransitionButton : MonoBehaviour
{
    public void GoToMainScene()
    {
        SceneManager.LoadScene("Main");
    }

}
