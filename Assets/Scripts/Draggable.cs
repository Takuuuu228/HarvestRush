using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Draggable : MonoBehaviour
{
    private bool m_isDragging; // �h���b�O�����ǂ�����ǐ�
    private Vector3 m_offset; // �h���b�O�J�n���̃}�E�X�ʒu�ƃI�u�W�F�N�g�ʒu�̃I�t�Z�b�g
    private float m_fMovementtime = 15f; // �ړ��ɂ����鎞�Ԃ̌W��
    private System.Nullable<Vector3> m_movementDestination; // �ړ���̈ʒu�inull�\�j

    public bool placedInDropSpace;
    public Vector3 m_lastPosition; // �h���b�O�J�n�O�̃I�u�W�F�N�g�̈ʒu
    private static Draggable m_lastDraggable; // �Ō�Ƀh���b�O���ꂽ�I�u�W�F�N�g��ǐ�

    // �}�E�X�{�^���������ꂽ���̏���
    private void OnMouseDown()
    {
        m_isDragging = true; // �h���b�O��Ԃ�^�ɐݒ�
        m_lastPosition = transform.position; // �Ō�̈ʒu�����݂̈ʒu�ɐݒ�
        m_lastDraggable = this; // ���̃h���b�K�u�����Ō�Ƀh���b�O���ꂽ���̂Ƃ��Đݒ�
        Vector3 mousePos = Input.mousePosition; // �}�E�X�̃X�N���[���ʒu���擾
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y)); // �}�E�X�ʒu�����[���h���W�ɕϊ�
        m_offset = new Vector2(transform.position.x - worldPos.x, transform.position.y - worldPos.y); // �I�t�Z�b�g���v�Z
        gameObject.layer = Layer.Dragging; // �I�u�W�F�N�g�̃��C���[���h���b�O���ɕύX

        Debug.Log("a");

    }

    // �}�E�X���h���b�O����Ă���Ԃ̏���
    private void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition; // �}�E�X�̃X�N���[���ʒu���擾
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y)); // �}�E�X�ʒu�����[���h���W�ɕϊ�

        transform.position = new Vector2(worldPos.x + m_offset.x, worldPos.y + m_offset.y); // �V�����ʒu�ɃI�u�W�F�N�g���ړ�
    }

    // �}�E�X�{�^���������ꂽ���̏���
    private void OnMouseUp()
    {
        m_isDragging = false; // �h���b�O��Ԃ��U�ɐݒ�
        gameObject.layer = Layer.Default; // �I�u�W�F�N�g�̃��C���[���f�t�H���g�ɖ߂�
    }

    // �g���K�[�ɉ��������������̏���
    private void OnTriggerEnter2D(Collider2D _other)
    {
        placedInDropSpace = false;
        Draggable colliderDraggable = _other.GetComponent<Draggable>(); // ���̃h���b�K�u���I�u�W�F�N�g
        DropSpace dropSpace = _other.GetComponent<DropSpace>(); // �h���b�v�X�y�[�X�R���|�[�l���g

        if (colliderDraggable != null && m_lastDraggable == this) // ���̃h���b�K�u���ƏՓ˂����ꍇ
        {
            // �Փ˃I�u�W�F�N�g�Ƃ̋����ƕ������v�Z
            ColliderDistance2D colliderDistance2D = _other.Distance(GetComponent<Collider2D>());
            Vector3 diff = new Vector3(colliderDistance2D.normal.x, colliderDistance2D.normal.y) * colliderDistance2D.distance;

            transform.position -= diff; // �I�u�W�F�N�g���Փ˂��Ȃ��ʒu�Ɉړ�
        }
        else if (dropSpace != null) // �h���b�v�X�y�[�X�ɓ������ꍇ
        {
            if (dropSpace.Droppable()) // �h���b�v�\�ȏꍇ
            {
                placedInDropSpace = true;
                dropSpace.SetDraggable(this); // ���̃h���b�K�u�����h���b�v�X�y�[�X�ɐݒ�
                m_movementDestination = _other.transform.position; // �ړ�����h���b�v�X�y�[�X�̈ʒu�ɐݒ�
                
            }
            else
            {

                m_movementDestination = m_lastPosition; // �ړ�����Ō�̈ʒu�ɖ߂�
            }
        }
        else
        {
            // ���̑��̃P�[�X�ł͉������Ȃ�
        }

        Debug.Log(placedInDropSpace);
    }

    // �Œ�X�V����
    private void FixedUpdate()
    {
        if (m_movementDestination.HasValue) // �ړ��悪�ݒ肳��Ă���ꍇ
        {
            if (m_isDragging) // �h���b�O���̏ꍇ
            {
                m_movementDestination = null; // �ړ�������Z�b�g
                return;
            }

            if (transform.position == m_movementDestination) // �ړI�n�ɓ��������ꍇ
            {
                gameObject.layer = Layer.Default; // ���C���[���f�t�H���g�ɖ߂�
                m_movementDestination = null; // �ړ�������Z�b�g
            }
            else
            {
                // ���݂̈ʒu����ړ���֊��炩�Ɉړ�
                transform.position = Vector3.Lerp(transform.position, m_movementDestination.Value, m_fMovementtime * Time.deltaTime);
            }
        }
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(3);
    }
}
