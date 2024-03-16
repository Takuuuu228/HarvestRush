using UnityEngine;
using UnityEngine.EventSystems; // �C�x���g�V�X�e�����g�p���邽�߂ɕK�v

// �h���b�v�G���A��\���N���X
public class DropSpace : MonoBehaviour
{
    public Draggable m_hasDraggable; // ���݂��̃h���b�v�X�y�[�X�ɔz�u����Ă���h���b�K�u���I�u�W�F�N�g
    public AudioSource audioSource;

    // �h���b�v�\���ǂ����𔻒f����֐�
    public bool Droppable()
    {
        // m_hasDraggable��null�Ȃ�΁A�h���b�v�X�y�[�X�͋�ł���A�V�����I�u�W�F�N�g���󂯓���邱�Ƃ��ł���
        return m_hasDraggable == null;
    }

    // �h���b�v�X�y�[�X�Ƀh���b�K�u���I�u�W�F�N�g��ݒ肷��֐�
    public void SetDraggable(Draggable _draggable)
    {
        // �����Ƃ��Ď󂯎�����h���b�K�u���I�u�W�F�N�g��m_hasDraggable�ɐݒ�
        m_hasDraggable = _draggable;
        if (m_hasDraggable.gameObject.tag == "Seed")
        {
            audioSource.Play();
        }

    }

    // ���̃R���C�_�[�����̃h���b�v�X�y�[�X����o�čs�������ɌĂ΂��֐�
    private void OnTriggerExit2D(Collider2D _other)
    {
        // �C�x���g�����������I�u�W�F�N�g����Draggable�R���|�[�l���g���擾
        Draggable draggable = _other.GetComponent<Draggable>();

        // �擾����Draggable��null�łȂ��A���A���݂̃h���b�v�X�y�[�X�ɂ���Draggable������Draggable�Ɠ����ł����
        if (draggable != null && m_hasDraggable == draggable)
        {
            // �h���b�v�X�y�[�X����Draggable���폜����inull��ݒ�j
            m_hasDraggable = null;
        }
    }
}
