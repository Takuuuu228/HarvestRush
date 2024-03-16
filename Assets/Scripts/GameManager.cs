using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // 追加: SceneManagerを使用するため

public class GameManager : MonoBehaviour
{
    public int score = 15000;
    private ScoreDisplay scoreDisplay; // SerializeField属性を削除し、privateに設定

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // ScoreDisplayコンポーネントをシーンから検索して割り当てる
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
        // 'Main'シーンがロードされた場合のみスコアをリセット
        if (scene.name == "Main")
        {
            score = 15000;
            // シーンが切り替わるたびにScoreDisplayを再検索して割り当てる
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

    // スコアを減算するメソッド
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
