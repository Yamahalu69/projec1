// PlayerHP.cs

using UnityEngine;
using UnityEngine.UI; // UI�e�L�X�g�Ȃǂ�HP��\������ꍇ�ɔ����Ēǉ�

public class PlayerHP : MonoBehaviour
{
    // ���̃X�N���v�g�̗B��̃C���X�^���X��ێ�����ÓI�ϐ��i�V���O���g���j
    public static PlayerHP Instance { get; private set; }

    // �y���v�]�ʂ�zHP���i�[����private�ϐ�
    private int Player_HP;

    // Inspector�Őݒ�ł���ő�HP
    [SerializeField]
    private int maxHp = 100;

    // ���̃X�N���v�g���猻�݂�HP�����S�ɓǂݎ�邽�߂̃v���p�e�B
    public int CurrentHp
    {
        get { return Player_HP; }
    }

    void Awake()
    {
        // �V���O���g���p�^�[���̎���
        // �����C���X�^���X���܂��Ȃ���΁A���̃C���X�^���X��o�^
        if (Instance == null)
        {
            Instance = this;
            // �V�[�����܂����ł�HP�Ǘ��I�u�W�F�N�g���j�󂳂�Ȃ��悤�ɂ������ꍇ�͉��̃R�����g���O��
            // DontDestroyOnLoad(gameObject); 
        }
        else
        {
            // ���łɃC���X�^���X�����݂���ꍇ�́A���̃I�u�W�F�N�g��j������
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // �Q�[���J�n����HP���ő�l�ɐݒ�
        Player_HP = maxHp;
        Debug.Log("�v���C���[��HP������������܂����B���݂�HP: " + Player_HP);
    }

    // �_���[�W���󂯂邽�߂̌��J���\�b�h
    public void TakeDamage(int damageAmount)
    {
        // HP����_���[�W�ʂ�����
        Player_HP -= damageAmount;

        Debug.Log(damageAmount + "�̃_���[�W���󂯂��I �c��HP: " + Player_HP);

        // HP��0�ȉ��ɂȂ����ꍇ�̏���
        if (Player_HP <= 0)
        {
            Player_HP = 0; // HP���}�C�i�X�ɂȂ�Ȃ��悤��0�ŌŒ�
            Die();
        }
    }

    // HP��0�ɂȂ����Ƃ��̏���
    private void Die()
    {
        Debug.Log("�v���C���[�͗͐s����...�B");
        // �����ɃQ�[���I�[�o�[�����Ȃǂ��L�q���܂�
        // ��: Time.timeScale = 0; // �Q�[�����~����
    }
}