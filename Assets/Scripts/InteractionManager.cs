using UnityEngine;

// ���̃X�N���v�g��GameObject�ɃA�^�b�`����邱�Ƃ�z�肵�Ă��܂��B
public class InteractionManager : MonoBehaviour
{
    // OrderManager�ւ̎Q��
    public OrderManager orderManager;

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
            orderManager.Start();
        }
    }
}
