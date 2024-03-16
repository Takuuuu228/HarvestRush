using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // �ǉ�: SceneManager���g�p���邽��

public class GameManager : MonoBehaviour
{
    public int score = 15000;
    private ScoreDisplay scoreDisplay; // SerializeField�������폜���Aprivate�ɐݒ�

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // ScoreDisplay�R���|�[�l���g���V�[�����猟�����Ċ��蓖�Ă�
            scoreDisplay = FindObjectOfType<ScoreDisplay>();

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 'Main'�V�[�������[�h���ꂽ�ꍇ�̂݃X�R�A�����Z�b�g
        if (scene.name == "Main")
        {
            score = 15000;
            // �V�[�����؂�ւ�邽�т�ScoreDisplay���Č������Ċ��蓖�Ă�
            scoreDisplay = FindObjectOfType<ScoreDisplay>();
            scoreDisplay.UpdateScoreText();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private IEnumerator Inoperable(float i)
    {
        GameObject seedObject = GameObject.FindWithTag("Seed");
        Draggable draggable = seedObject.GetComponent<Draggable>();

        draggable.enabled = false;
        yield return new WaitForSeconds(i);
        draggable.enabled = true;
    }

    public void CallInoperable(float i)
    {
        StartCoroutine("Inoperable", i);
    }

    // �X�R�A�����Z���郁�\�b�h
    public void DecreaseScore(int amount)
    {
        score -= amount;
        scoreDisplay.UpdateScoreText();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreDisplay.UpdateScoreText();
    }


}
