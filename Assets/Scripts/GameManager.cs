using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // �ǉ�: SceneManager���g�p���邽��

public class GameManager : MonoBehaviour
{
    // �Q�[���̃X�R�A��ێ�����ϐ�
    public int score = 15000;
    [SerializeField] private ScoreDisplay scoreDisplay;

    // �V���O���g���p�^�[�����������邽�߂̐ÓI�C���X�^���X
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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


}
