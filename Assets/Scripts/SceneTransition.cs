using UnityEngine;
using UnityEngine.SceneManagement; // �V�[����������邽�߂ɕK�v

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
