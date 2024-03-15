using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // 追加: SceneManagerを使用するため

public class GameManager : MonoBehaviour
{
    // ゲームのスコアを保持する変数
    public int score = 15000;
    [SerializeField] private ScoreDisplay scoreDisplay;

    // シングルトンパターンを実装するための静的インスタンス
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

    // スコアを減算するメソッド
    public void DecreaseScore(int amount)
    {
        score -= amount;
        scoreDisplay.UpdateScoreText();
    }


}
