using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    // 3��ނ�GameObject��ێ�����z����`���܂��B
    public GameObject[] objectsToSpawn;

    public void Start()
    {
        // Start���\�b�h���Ăяo���ꂽ���Ɏ��s����܂��B

        if (objectsToSpawn.Length == 0)
        {
            // objectsToSpawn����̏ꍇ�A�x�������O�ɏo�͂��ď������I�����܂��B
            Debug.LogWarning("No objects set to spawn.");
            return;
        }

        // objectsToSpawn���烉���_����GameObject��1�I�����܂��B
        int randomIndex = Random.Range(0, objectsToSpawn.Length);
        GameObject selectedObject = objectsToSpawn[randomIndex];

        if (selectedObject != null)
        {
            // �I�����ꂽGameObject�����݂�GameObject�̎q�Ƃ��ăC���X�^���X�����܂��B
            // �C���X�^���X������ہA�e�I�u�W�F�N�g�̈ʒu�����g�p���A��]���͎q�I�u�W�F�N�g�ɂ��̂܂܈����p�����܂��B
            Instantiate(selectedObject, transform.position, Quaternion.identity, transform);
        }
        else
        {
            // �I�����ꂽ�I�u�W�F�N�g��null�̏ꍇ�A�G���[���b�Z�[�W�����O�ɏo�͂��܂��B
            Debug.LogError("Selected object is null.");
        }
    }
}