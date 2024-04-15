using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public  float _PlayerCurrenthealth;
    [SerializeField] private float _Maxhealth;
    [SerializeField] private float _Damage;

    [SerializeField] GameObject AttackButton;
    private bool Attacked;

    private Animator _anim;

    [SerializeField] private HealthBarSystem HealthBar;

     public EnemyAi _enemy;


    private Transform AttackRangeTransform;
    [SerializeField] private float AttackRange;
    [SerializeField] private LayerMask EnemyLayer;
    [SerializeField] private bool EnemyInAttackRange;
    // Start is called before the first frame update
    void Start()
    {
        AttackRangeTransform=GetComponent<Transform>();
        HealthBar = GetComponentInChildren<HealthBarSystem>();
        _anim = GetComponent<Animator>();
        
    }

    private void Update()
    {
        EnemyInAttackRange = Physics.CheckSphere(transform.position, AttackRange, EnemyLayer);
        if (EnemyInAttackRange) AttackCheck();
        
    }

    public void PlayerTakeDamage(float damage)
    {
        _PlayerCurrenthealth -= damage;
        _anim.SetTrigger("Hit");
        HealthBar.UpdateHealthBar(_PlayerCurrenthealth, _Maxhealth);

        if (_PlayerCurrenthealth <= 0)
        {
            _anim.SetTrigger("Die");
            Invoke(nameof(DestroyPlayer), 3f);
        }
    }
    void DestroyPlayer()
    {
        Destroy(gameObject);
    }


    private void AttackCheck()
    {
        if (Attacked==true)
        {
            _anim.SetTrigger("Attack1");
            _enemy.EnemyTakeDamage(_Damage);
            Attacked = false;
        }
    }
    public void SwordAttack()
    {

        Attacked = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,AttackRange);
    }
}

