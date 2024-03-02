using UnityEngine; // UnityEngine�̖��O��Ԃ��g�p

// SeedShop�N���X���`���AMonoBehaviour���p������
public class Shop : MonoBehaviour
{
    // Inspector�Őݒ�ł���悤�ɁAGameObject�^��Prefab�ϐ������J����
    public GameObject prefab;

    [SerializeField] private int seedPay = 500;
    [SerializeField] private int insecticidePay = 10000;

    // OnMouseDown���\�b�h���I�[�o�[���C�h����
    void OnMouseDown()
    {
        // �v���n�u���ݒ肳��Ă���ꍇ�A���̃v���n�u�̃C���X�^���X�𐶐�����
        if (prefab != null)
        {
            // Instantiate���\�b�h���g�p���āA�v���n�u�̃C���X�^���X�����݂�GameObject�̈ʒu�ɐ�������
            Instantiate(prefab, transform.position, Quaternion.identity);
            if (this.gameObject.CompareTag("SeedShop"))
            {
                // GameManager�̃C���X�^���X���g�p���ăX�R�A��500���Z����
                GameManager.instance.DecreaseScore(seedPay);
            }
            if (this.gameObject.CompareTag("InsecticideShop"))
            {
                GameManager.instance.DecreaseScore(insecticidePay);
            }
        }
    }
}
