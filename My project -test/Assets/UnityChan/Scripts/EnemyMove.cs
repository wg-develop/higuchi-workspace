using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMove : MonoBehaviour
{
    public Transform target;
    private Animator EnemyAnimator;
    private Vector3 EnemyMoveDirection = new Vector3(0, 0, 0);
    public GameObject timerGameObject;
    private TimerScript timerScript;
    private float EnemySpeed = 0.03f; //�ړ����x
    private string playerName = "SD_unitychan_humanoid"; //�v���C���[�I�u�W�F�N�g��

    void Start()
    {
        EnemyAnimator = GetComponent<Animator>();
        timerScript = timerGameObject.GetComponent<TimerScript>();
    }

    void Update()
    {
        EnemyMoveControl();
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
            moving(false, 0);
            this.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            moving(true, 1);
        }
        else if (enemyPosition.x > playerPosition.x + 0.6f)
        {
            // �v���C���[���G���}�C�i�X�ʒu�ɂ�����}�C�i�X�����֓���
            moving(false, 0);
            this.transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
            moving(true, -1);
        }
        else
        {
            moving(false, 0);
            return;
        }
    }

    /// <summary>
    /// �G�𑖂点�܂�
    /// </summary>
    /// <param name="is_move">����t���O</param>
    /// <param name="val">�i�ޕ���</param>
    void moving(bool is_move, int val)
    {
        if (is_move)
        {
            EnemyMoveDirection.x = EnemySpeed * val;
            transform.position += EnemyMoveDirection;
            EnemyAnimator.SetBool("is_running", true);
        }
        else
        {
            EnemyAnimator.SetBool("is_running", false);
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
