using UnityEngine;
using UnityEngine.SceneManagement; // �V�[����������邽�߂ɕK�v

public class SceneTransitionButton : MonoBehaviour
{
    public void GoToMainScene()
    {
        SceneManager.LoadScene("Main");
    }

}
