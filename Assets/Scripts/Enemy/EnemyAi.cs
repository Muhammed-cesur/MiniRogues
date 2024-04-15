using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private Transform CharacterTransform;
    private NavMeshAgent _agent;
    private Animator _anim;

    [SerializeField] private float _Damage;




    // walk Range
    [SerializeField] private Vector3 WalkLocation;
    [SerializeField] private bool WalkLocationSet;
    [SerializeField] private float WalkPointRange;

    // Attack Range

    [SerializeField] private float AttackDelay;
    [SerializeField] private bool Attacked;
    [SerializeField] private GameObject Fireball;

    // Player and world Check
    public float SightRange, AttackRange;
    public bool PlayerInSightRange, PlayerInAttackRange;

    [SerializeField] private LayerMask GroundCeck, PlayerCheck;


    public float _EnemyCurrenthealth;
    public float _Maxhealth;
    
    
    
   

    [SerializeField] private HealthBarSystem HealthBar;
    [SerializeField] Attack Attack;




    // Start is called before the first frame update
    void Start()
    {
        HealthBar = GetComponentInChildren<HealthBarSystem>();

        CharacterTransform = GameObject.Find("Knight").transform;
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        // player in attack range or in walk range check
        PlayerInSightRange = Physics.CheckSphere(transform.position, SightRange, PlayerCheck);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, PlayerCheck);


        if (!PlayerInSightRange && !PlayerInAttackRange) RandomWalk();
        if (PlayerInSightRange && !PlayerInAttackRange) FollowPlayer();
        if (PlayerInAttackRange && PlayerInSightRange) AttackPlayer();

    }
        void SearchPlayer()
    {
        float RandomZ = Random.Range(-WalkPointRange, WalkPointRange);
        float RandomX = Random.Range(-WalkPointRange, WalkPointRange);
        
        


        WalkLocation = new Vector3(transform.position.x + RandomX, transform.position.y, transform.position.z + RandomZ);


        if (Physics.Raycast(WalkLocation, -transform.up, 1f, GroundCeck))
            WalkLocationSet = true;
          

    }

    void RandomWalk()
    {
        if (!WalkLocationSet) SearchPlayer();

        _anim.SetBool("Walk", true);
        _anim.SetBool("Run", false);

        if (WalkLocationSet)
            _agent.SetDestination(WalkLocation);

        Vector3 distanceToWalkPoint = transform.position - WalkLocation;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 2f)
            WalkLocationSet = false;

        
    }

    void FollowPlayer()
    {
        _agent.destination = CharacterTransform.position;
        _anim.SetBool("Run", true);
        _anim.SetBool("Walk", false);

    }
  public  void AttackPlayer()
    {
        _agent.SetDestination(transform.position);

        transform.LookAt(CharacterTransform);
        if (!Attacked)
        {
            ///Attack code here
            _anim.SetTrigger("Attack");
            Attack.PlayerTakeDamage(_Damage);
            if (Fireball) 
            {
                Rigidbody rb = Instantiate(Fireball, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            }

            ///End of attack code

            Attacked = true;
            
            Invoke(nameof(ResetAttack), AttackDelay);
            if (Attack._PlayerCurrenthealth==0) 
            {
             Attacked = false;
            }

        }
        _anim.SetBool("Walk", false);
        _anim.SetBool("Run", false);
    }
    void ResetAttack()
    {
        Attacked = false;
        
    }

    public void EnemyTakeDamage(float damage)
    {
        _EnemyCurrenthealth -= damage;
        _anim.SetTrigger("Hit");
        HealthBar.UpdateHealthBar(_EnemyCurrenthealth, _Maxhealth);

        if (_EnemyCurrenthealth <= 0)
        {
            _agent.speed = 0;
            SightRange = 0;
            WalkPointRange = 0;
            _anim.SetTrigger("Die");

            Invoke(nameof(DestroyEnemy), 2f);
        }
           

    }
    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, SightRange);
    }


   
}

