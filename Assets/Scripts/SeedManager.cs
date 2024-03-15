using UnityEngine;
using System.Collections;

public class SeedManager : MonoBehaviour
{
    // crop�Ƃ������O��GameObject��ݒ肷�邽�߂�public�ϐ��B
    // ���̕ϐ��ɂ�UnityEditor����Prefab������GameObject���A�^�b�`�����B
    public GameObject crop;
    [SerializeField] private float waitTime;
    [SerializeField] private Draggable draggable;

    void Update()
    {
        HandlePlacementChanged(draggable.placedInDropSpace);
    }

    // Draggable�X�N���v�g��placedInDropSpace�̏�Ԃ��ς�������ɌĂ΂�郁�\�b�h�B
    private void HandlePlacementChanged(bool placed)
    {
        // ����placedInDropSpace��true�ɂȂ�����ACoroutine���J�n����B
        if (placed)
        {
            StartCoroutine(RemoveAndSpawnCoroutine());
        }
    }

    // GameObject���폜���A�w�肳�ꂽPrefab�𐶐�����Coroutine�B

    private IEnumerator RemoveAndSpawnCoroutine()
    {
        // 5�b�҂B
        yield return new WaitForSeconds(waitTime);

        // ����GameObject���폜�B
        Destroy(gameObject);

        // crop��Prefab�����݂̈ʒu�ɐ������邪�Az���W��0�ɐݒ肷��B
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, -1.0f);
        GameObject spawnedCrop = Instantiate(crop, spawnPosition, Quaternion.identity);
    }

}

