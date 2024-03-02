using UnityEngine;
using UnityEngine.UI; // UIコンポーネントを使用するために必要

public class ScoreDisplay : MonoBehaviour
{
    // スコアを表示するためのテキストコンポーネント
    public Text scoreText;

    void Start()
    {
        UpdateScoreText();
    }

    // スコアが更新されるたびに呼ばれるメソッド
    public void UpdateScoreText()
    {
        // GameManagerから現在のスコアを取得してテキストに設定
        scoreText.text =  GameManager.instance.score.ToString();
    }
}
