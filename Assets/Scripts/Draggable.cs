using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool isDragging = false;
    private GameObject clone;
    private Vector3 offset;

    void Update()
    {
        if (isDragging)
        {
            // �}�E�X�̃��[���h���W�ɃI�t�Z�b�g�������āA�N���[���̈ʒu���X�V
            Vector3 mousePosition = GetMouseWorldPosition() + offset;
            clone.transform.position = mousePosition;
        }
    }

    void OnMouseDown()
    {
        // �h���b�O�J�n���ɃI�u�W�F�N�g���N���[�����A�h���b�O���J�n
        isDragging = true;
        clone = Instantiate(gameObject, transform.position, Quaternion.identity);
        clone.name = gameObject.name + " clone";
        // �N���[����Draggable�R���|�[�l���g��ǉ�
        if (clone.GetComponent<Draggable>() == null)
        {
            clone.AddComponent<Draggable>();
        }

        // �}�E�X�̃��[���h���W���擾���A�I�t�Z�b�g���v�Z
        Vector3 mousePosition = GetMouseWorldPosition();
        offset = transform.position - mousePosition;
        // �h���b�O���̓N���[���𖳎����郌�C���[�ɐݒ�
        clone.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    void OnMouseUp()
    {
        // �}�E�X�𗣂����Ƃ��Ƀh���b�O���I��
        StopDragging();
    }

    // �}�E�X�̃��[���h���W���擾����w���p�[���\�b�h
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.nearClipPlane; // �J��������̋���
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    // �h���b�O���I�����郁�\�b�h
    public void StopDragging()
    {
        isDragging = false;
        // �N���[����DropSpace�̎q�łȂ��ꍇ�͔j��
        if (clone.transform.parent == null || !clone.transform.parent.GetComponent<DropSpace>())
        {
            Destroy(clone);
        }
    }
}
