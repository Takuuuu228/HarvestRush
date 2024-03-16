using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // ����ɂ��Unity�G�f�B�^�[�ŕ\���\�ɂȂ�܂��B
public class ObjectScorePair
{
    public GameObject gameObject;
    public int score;
}

public class OrderManager : MonoBehaviour
{
    public AudioSource okAudio;
    public AudioSource noAudio;

    // ObjectScorePair�̔z����g�p���āA�I�u�W�F�N�g�Ƃ���ɑΉ�����X�R�A��ێ����܂��B
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
            // GameManager��IncreaseScore���\�b�h���Ăяo���ăX�R�A�����Z���܂��B
            // �I�u�W�F�N�g�Ɋ֘A�t����ꂽ�X�R�A��T���܂��B
            foreach (var pair in objectsToSpawnWithScore)
            {
                if (pair.gameObject.tag == firstChild.tag)
                {
                    GameManager.instance.IncreaseScore(pair.score);

                    break; // �X�R�A�����Z�����烋�[�v�𔲂��܂��B
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
            // GameManager��IncreaseScore���\�b�h���Ăяo���ăX�R�A�����Z���܂��B
            // �I�u�W�F�N�g�Ɋ֘A�t����ꂽ�X�R�A��T���܂��B
            foreach (var pair in objectsToSpawnWithScore)
            {
                if (pair.gameObject.tag == firstChild.tag)
                {
                    GameManager.instance.DecreaseScore(pair.score);

                    break; // �X�R�A�����Z�����烋�[�v�𔲂��܂��B
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
