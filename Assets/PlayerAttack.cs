// DamageReceiver.cs

using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    // �I�u�W�F�N�g�����̃R���C�_�[�ƐڐG�����Ƃ��ɌĂяo����郁�\�b�h
    private void OnCollisionEnter(Collision collision)
    {
        // �ڐG��������̃^�O�� "Sword" �܂��� "Cutter" ���ǂ������`�F�b�N
        if (collision.gameObject.CompareTag("Sword") || collision.gameObject.CompareTag("Cutter"))
        {
            Debug.Log(gameObject.name + " �� " + collision.gameObject.name + " �ɐڐG���܂����B");

            // PlayerHP�X�N���v�g��TakeDamage���\�b�h���Ăяo����10�_���[�W��^����
            // PlayerHP.Instance �� null �łȂ����Ƃ��m�F����Ƃ����S
            if (PlayerHP.Instance != null)
            {
                PlayerHP.Instance.TakeDamage(10);
            }
            else
            {
                Debug.LogError("PlayerHP�̃C���X�^���X��������܂���I�V�[����PlayerHP�X�N���v�g�����I�u�W�F�N�g��z�u���Ă��������B");
            }
        }
    }
}