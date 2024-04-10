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



    // walk Range
    [SerializeField] private Vector3 WalkLocation;
    [SerializeField] private bool WalkLocationSet;
    [SerializeField] private float WalkPointRange;

    // Attack Range

    [SerializeField] private float AttackDelay;
    [SerializeField] private bool Attacked;
    [SerializeField] private GameObject Project;

    // Player and world Check
    public float SightRange, AttackRange;
    public bool PlayerInSightRange, PlayerInAttackRange;

    [SerializeField] private LayerMask GroundCeck, PlayerCheck;


    [SerializeField] private float _Currenthealth;
    [SerializeField] private float _Maxhealth;
    
    
    
   

    [SerializeField] private HealthBarSystem HealthBar;




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
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
         TakeDamage(5);
            
            
        }

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
    void AttackPlayer()
    {
        _agent.SetDestination(transform.position);

        transform.LookAt(CharacterTransform);
        if (!Attacked)
        {
            ///Attack code here
            _anim.SetTrigger("Attack");

            ///End of attack code

            Attacked = true;
            
            Invoke(nameof(ResetAttack), AttackDelay);

        }
        _anim.SetBool("Walk", false);
        _anim.SetBool("Run", false);
    }
    void ResetAttack()
    {
        Attacked = false;
        
    }

    public void TakeDamage(float damage)
    {
        _Currenthealth -= damage;
        _anim.SetTrigger("Hit");
        HealthBar.UpdateHealthBar(_Currenthealth, _Maxhealth);

        if (_Currenthealth <= 0)
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

