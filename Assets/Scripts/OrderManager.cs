using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    // 3��ނ�GameObject��ێ�����z����`���܂��B
    public GameObject[] objectsToSpawn;

    void Start()
    {
        InstantiateObject();

    }

    // Trigger�ɓ������ۂɌĂ΂�郁�\�b�h
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ����GameObject�̎q�I�u�W�F�N�g�̒��ōŏ��Ɍ����������̂̃^�O���擾
        // �q�I�u�W�F�N�g�����݂��Ȃ��ꍇ�A�������X�L�b�v
        if (transform.childCount == 0) return;

        GameObject firstChild = transform.GetChild(0).gameObject;

        // �Ԃ������I�u�W�F�N�g�̃^�O�ƁA����GameObject�̎q�I�u�W�F�N�g�̃^�O����v���邩�`�F�b�N
        if (collision.gameObject.tag == firstChild.tag)
        {
            // ��v�����ꍇ�A�Ԃ�����GameObject�Ƃ���GameObject�̍ŏ��̎q�I�u�W�F�N�g������
            Destroy(collision.gameObject); // �Ԃ�����GameObject���폜
            Destroy(firstChild); // ����GameObject�̍ŏ��̎q�I�u�W�F�N�g���폜

            // OrderManager��Start���\�b�h���Ăяo���A���������ēx�s��
            InstantiateObject();
        }
    }

    void InstantiateObject()
    {

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