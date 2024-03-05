using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // ゲームのスコアを保持する変数
    public int score = 15000;
    [SerializeField] private ScoreDisplay scoreDisplay;

    // シングルトンパターンを実装するための静的インスタンス
    public static GameManager instance;

    private void Awake()
    {
        // GameManagerのインスタンスがなければ、これを設定する
        if (instance == null)
        {
            instance = this;
            // シーン遷移時に破棄されないようにする
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 既に存在する場合は、このGameObjectを破棄する
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

    // スコアを減算するメソッド
    public void DecreaseScore(int amount)
    {
        score -= amount;
        Debug.Log("Current Score: " + score);
        scoreDisplay.UpdateScoreText();
    }
}
