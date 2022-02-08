using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Transform target;
    private Animator EnemyAnimator;
    private Vector3 EnemyMoveDirection = new Vector3(0, 0, 0);
    private float EnemySpeed = 0.03f; //�ړ����x

    void Start()
    {
        EnemyAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        //�G�̌��݈ʒu�ƃv���C���[�̌��ݒn���擾
        Vector3 enemyPosition = this.transform.position;
        Vector3 playerPosition = target.position;

        //�G�ʒu=�v���C���[�ʒu�ƂȂ�悤�ɂ���
        //if(enemyPosition.x == 10.0f)
        //    moving(false,0);
        //if (enemyPosition.x == playerPosition.x + 10f)
        //    moving(false, 0);
        if (enemyPosition.x + 0.8f < playerPosition.x)
        {
            // �v���C���[���G���v���X�ʒu�ɂ�����v���X�����֓���
            this.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            moving(true,1);
        }else if (enemyPosition.x > playerPosition.x + 0.8f){
            // �v���C���[���G���}�C�i�X�ʒu�ɂ�����}�C�i�X�����֓���
            this.transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
            moving(true,-1);
        }
        else{
            moving(false,0);
            return;
        }
    }

    void moving(bool is_move, int val)
    {
        if (is_move){
            EnemyMoveDirection.x = EnemySpeed * val;
            transform.position += EnemyMoveDirection;
            EnemyAnimator.SetBool("is_running", true);
        }
        else{
            EnemyAnimator.SetBool("is_running", false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit"); // ���O��\������
        moving(false,0);
    }
}
