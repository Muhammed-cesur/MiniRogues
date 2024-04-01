using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    private Animator _anim;
    [Header ("Buttons")]
    [SerializeField] private GameObject FireBallButton;
    [SerializeField] private GameObject GarenButton;
    // [SerializeField] private GameObject SwordRainButton;
    // [SerializeField] private GameObject AttackButton;

    // [SerializeField] private GameObject GarenPassiveButton;
    // [SerializeField] private GameObject SwordRainPassiveButton;
    // [SerializeField] private GameObject FireBallPassiveButton;


    [Header("Vfx")]
    [SerializeField] private ParticleSystem fireBall;
   // [SerializeField] private GameObject swordRain;
    
    Movement movement;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
        movement = GetComponent<Movement>();
        _anim = GetComponent<Animator>();

        fireBall.Stop();
        //swordRain.SetActive(false);


        FireBallButton.SetActive(true);
        GarenButton.SetActive(true);
        // SwordRainButton.SetActive(true);
        // AttackButton.SetActive(true);

        StartCoroutine(Waittttt());
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void Attack()
    {
        _anim.SetTrigger("Attack1");
        movement.playerSpeed = 0;
        

        StartCoroutine(Waittttt());
        StartCoroutine(CharacterFireWait());
    
    }
    public void GarenE()
    {
            _anim.SetTrigger("GarenE");
            StartCoroutine(Waittttt());
            GarenButton.SetActive(false);
        //GarenPassiveButton.SetActive(true);
    }
    public void FireBall() 
    {
         
        _anim.SetTrigger("FireShoot");
        movement.playerSpeed = 0;
        fireBall.Play();

        StartCoroutine(Waittttt());
        StartCoroutine(CharacterFireWait());

        FireBallButton.SetActive(false);
        //FireBallPassiveButton.SetActive(true);

    }
    /*
    public void SwordRain ()
    {
        _anim.SetTrigger("SwordRain");
        
        movement.playerSpeed = 0;

        SwordRainButton.SetActive(false);
        SwordRainPassiveButton.SetActive(true);
        StartCoroutine(Waittttt());
        StartCoroutine(CharacterSwordWait());
        swordRain.SetActive(true);
    }
    */

    IEnumerator Waittttt() 
    {
        //swordRain.SetActive(false);
        yield return new WaitForSecondsRealtime(3f);

        FireBallButton.SetActive(true);
        GarenButton.SetActive(true);
        // SwordRainButton.SetActive(true);
        //AttackButton.SetActive(true);


        Debug.Log("calisiyor");
 
    }
    IEnumerator CharacterFireWait()
    {
        yield return new WaitForSecondsRealtime(1.75f);
        movement.playerSpeed = 3f;
        _anim.SetBool("Walk", false);
        _anim.SetBool("Run", false);
    }
    IEnumerator CharacterSwordWait()
    {
        yield return new WaitForSecondsRealtime(4f);
        movement.playerSpeed = 3f;
        _anim.SetBool("Walk", false);
        _anim.SetBool("Run", false);
    }
}


