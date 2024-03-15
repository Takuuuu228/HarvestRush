using UnityEngine;
using UnityEngine.SceneManagement; // シーン操作をするために必要

public class SceneTransition : MonoBehaviour
{
    public void GoToMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void GoToResultScene()
    {
        SceneManager.LoadScene("Result");
    }

}
