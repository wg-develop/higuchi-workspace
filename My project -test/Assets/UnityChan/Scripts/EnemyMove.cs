using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMove : MonoBehaviour
{
    public Transform target;
    private Animator enemyAnimator;
    private Vector3 enemyMoveDirection = new Vector3(0, 0, 0);
    public GameObject timerGameObject;
    private TimerScript timerScript;
    private float enemySpeed = 0.03f; //�ړ����x
    private string playerName = "SD_unitychan_humanoid"; //�v���C���[�I�u�W�F�N�g��
    private Vector3 startPosition = new Vector3(-10, -0.2f, 0); //�G�X�^�[�g�ʒu

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        timerScript = timerGameObject.GetComponent<TimerScript>();
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);

        float timer = timerScript.GetTimer();
        bool isStartedTimer = timerScript.countFlag;
        if (timer == 120.0f)
        {
            // ��~�����Z�b�g��
            transform.position = startPosition;
        }

        if (isStartedTimer)
        {
            // �^�C�}�[�N����
            if (timer == 0.0f)
            {
                // 120�b�����؂����ꍇ�F�Q�[���N���A��ʂɑJ�ځH
                Moving(false, 0);
            }
            EnemyMoveControl();
        }
        else
        {
            // �^�C�}�[��~��
            Moving(false, 0);
        }
    }

    /// <summary>
    /// �G�ƃv���C���[�̈ʒu�֌W�ɂ���ē��쐧�䂵�܂�
    /// </summary>
    void EnemyMoveControl()
    {
        //�G�̌��݈ʒu�ƃv���C���[�̌��ݒn���擾
        Vector3 enemyPosition = this.transform.position;
        Vector3 playerPosition = target.position;

        if (enemyPosition.x + 0.6f < playerPosition.x)
        {
            // �v���C���[���G���v���X�ʒu�ɂ�����v���X�����֓���
            Moving(false, 0);
            this.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            Moving(true, 1);
        }
        else if (enemyPosition.x > playerPosition.x + 0.6f)
        {
            // �v���C���[���G���}�C�i�X�ʒu�ɂ�����}�C�i�X�����֓���
            Moving(false, 0);
            this.transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
            Moving(true, -1);
        }
        else
        {
            Moving(false, 0);
            return;
        }
    }

    /// <summary>
    /// �G�𑖂点�܂�
    /// </summary>
    /// <param name="is_move">����t���O</param>
    /// <param name="val">�i�ޕ���</param>
    void Moving(bool is_move, int val)
    {
        if (is_move)
        {
            enemyMoveDirection.x = enemySpeed * val;
            transform.position += enemyMoveDirection;
            enemyAnimator.SetBool("is_running", true);
        }
        else
        {
            enemyAnimator.SetBool("is_running", false);
        }
    }

    /// <summary>
    /// �����蔻��
    /// </summary>
    /// <param name="collision">�Փ˂�������̏��</param>
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit :" + collision.gameObject.name);
        if (collision.gameObject.name == playerName)
        {
            // �G�ƃv���C���[���Ԃ�������Q�[���I�[�o�[��ʂ֑J�ڂ���
            //SceneManager.LoadScene("GameOverScene");
        }
    }

    //Unity�����̑������Đ�����i�uRunning@loop�vAnimation����Ă΂��j
    public void PlayFootstepSE()
    {
        //�G�l�~�[�ɂ�����������Ƃ��邳�����Ȃ̂ŕۗ�
    }
}
