using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // �Q�[���̃X�R�A��ێ�����ϐ�
    public int score = 15000;
    [SerializeField] private ScoreDisplay scoreDisplay;

    // �V���O���g���p�^�[�����������邽�߂̐ÓI�C���X�^���X
    public static GameManager instance;

    private void Awake()
    {
        // GameManager�̃C���X�^���X���Ȃ���΁A�����ݒ肷��
        if (instance == null)
        {
            instance = this;
            // �V�[���J�ڎ��ɔj������Ȃ��悤�ɂ���
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // ���ɑ��݂���ꍇ�́A����GameObject��j������
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
        yield break;
    }

    public void CallInoperable(float i)
    {
        StartCoroutine("Inoperable", i);        
    }

    // �X�R�A�����Z���郁�\�b�h
    public void DecreaseScore(int amount)
    {
        score -= amount;
        Debug.Log("Current Score: " + score);
        scoreDisplay.UpdateScoreText();
    }
}
