using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // これによりUnityエディターで表示可能になります。
public class ObjectScorePair
{
    public GameObject gameObject;
    public int score;
}

public class OrderManager : MonoBehaviour
{
    public AudioSource okAudio;
    public AudioSource noAudio;

    // ObjectScorePairの配列を使用して、オブジェクトとそれに対応するスコアを保持します。
    public ObjectScorePair[] objectsToSpawnWithScore;

    public ParticleSystem particleSystemOfMoney;
    public ParticleSystem particleSystemOfCalm;

    void Start()
    {
        InstantiateObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.childCount == 0) return;

        GameObject firstChild = transform.GetChild(0).gameObject;

        if (collision.gameObject.tag == firstChild.tag)
        {
            // GameManagerのIncreaseScoreメソッドを呼び出してスコアを加算します。
            // オブジェクトに関連付けられたスコアを探します。
            foreach (var pair in objectsToSpawnWithScore)
            {
                if (pair.gameObject.tag == firstChild.tag)
                {
                    GameManager.instance.IncreaseScore(pair.score);

                    break; // スコアを加算したらループを抜けます。
                }
            }

            okAudio.Play();
            Destroy(collision.gameObject);
            Destroy(firstChild);

            PlayParticleSystem(particleSystemOfMoney);
            InstantiateObject();


        }
        else if(collision.gameObject.tag != firstChild.tag)
        {
            // GameManagerのIncreaseScoreメソッドを呼び出してスコアを加算します。
            // オブジェクトに関連付けられたスコアを探します。
            foreach (var pair in objectsToSpawnWithScore)
            {
                if (pair.gameObject.tag == firstChild.tag)
                {
                    GameManager.instance.DecreaseScore(pair.score);

                    break; // スコアを減算したらループを抜けます。
                }
            }

            noAudio.Play();
            Destroy(collision.gameObject);

            PlayParticleSystem(particleSystemOfCalm);
        }
    }

    void InstantiateObject()
    {
        if (objectsToSpawnWithScore.Length == 0)
        {
            Debug.LogWarning("No objects set to spawn.");
            return;
        }

        int randomIndex = Random.Range(0, objectsToSpawnWithScore.Length);
        GameObject selectedObject = objectsToSpawnWithScore[randomIndex].gameObject;

        if (selectedObject != null)
        {
            Instantiate(selectedObject, transform.position, Quaternion.identity, transform);
        }
        else
        {
            Debug.LogError("Selected object is null.");
        }
    }

    void PlayParticleSystem(ParticleSystem particle)
    {
        if (particle != null)
        {
            particle.transform.position = transform.position;
            particle.Play();
        }
        else
        {
            Debug.LogWarning("Particle system to play is not set.");
        }
    }
}
