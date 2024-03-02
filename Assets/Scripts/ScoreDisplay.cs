using UnityEngine;
using UnityEngine.UI; // UI�R���|�[�l���g���g�p���邽�߂ɕK�v

public class ScoreDisplay : MonoBehaviour
{
    // �X�R�A��\�����邽�߂̃e�L�X�g�R���|�[�l���g
    public Text scoreText;

    void Start()
    {
        UpdateScoreText();
    }

    // �X�R�A���X�V����邽�тɌĂ΂�郁�\�b�h
    public void UpdateScoreText()
    {
        // GameManager���猻�݂̃X�R�A���擾���ăe�L�X�g�ɐݒ�
        scoreText.text =  GameManager.instance.score.ToString();
    }
}
