using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;



public class Player : MonoBehaviour
{
    public int viata = 10;

    [Header("Attack Script")]
    public float timeBetweenAttacks;
    private float startTimeBetweenAttacks = 0;

    //public Transform attackPosition;
    public float offsetAttackRange = 0.5f; //Daca e 0, atunci o sa faca un cerc pe langa sprite, daca e pozitiv in drepata.
    private Vector3 cercAtac;
    public float attackRange = 1f;
    public LayerMask enemyMask;

    private Movement miscare = null;

    private Animator animator;

    public Weapon weapon;
    private GameObject weaponHolder;
    private Weapon tempWeapon;
    public Vector3 offsetWeapon;

    public bool shield = false;

    private bool coliziuneArma = false;

    private PlayerSounds playerSoundManager;
    private Joystick joystick;
    private float interactionCooldown=1f;
    private float helper = 1f;

    public Button attackButton;
    public Button shieldButton;

    void OnTriggerEnter2D(Collider2D other)
    {
        //Cand atinge arma de pe jos
        tempWeapon = other.gameObject.GetComponent<Weapon>();
        if (tempWeapon != null) //De inlocuit Sword cu toate armele
        {
            coliziuneArma = true;
        }
        //Player lovit de sageata
        if (other.gameObject.name.Contains("Arrow") == true && shield == false)
        {
            viata -= other.GetComponent<Arrow>().damage;
            this.GetComponent<Heathbar>().SetHealthBar(viata);//bar.GetComponent<SpriteRenderer>().sprite = vectorViata[viata - 1];

            Destroy(other.gameObject);

            StartCoroutine(Camera.main.GetComponentInChildren<CameraShake>().Shake(0.5f, 0.25f));
            StartCoroutine(this.GetComponent<Heathbar>().Flick());

        }
        if (other.gameObject.name.Contains("Falling") == true && shield == false)
        {
            viata -= other.GetComponent<SpecialArrowV2>().damage;
            this.GetComponent<Heathbar>().SetHealthBar(viata);//bar.GetComponent<SpriteRenderer>().sprite = vectorViata[viata - 1];

            Destroy(other.gameObject);

            StartCoroutine(Camera.main.GetComponentInChildren<CameraShake>().Shake(0.5f, 0.25f));
            StartCoroutine(this.GetComponent<Heathbar>().Flick());

        }
        //Cand sunt lovit de barbar in in scriptu de barbar


        //Cand ies de pe mapa
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //Dispare cand apesi e
        //Cand nu atinge arma de pe jos (cand iese din coliderul acesteia) 
        coliziuneArma = false;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        //
    }
    void Start()
    {
        startTimeBetweenAttacks = timeBetweenAttacks;
        cercAtac = new Vector3(transform.position.x + offsetAttackRange, transform.position.y+offsetWeapon.y, transform.position.z);
        miscare = this.GetComponent<Movement>(); //Dreapta true, stanga false
        joystick = miscare.joystick;

        //Animatie
        animator = miscare.animator;
        animator.SetLayerWeight(animator.GetLayerIndex("Normal"), 1);

        AnimationCheck();
        //End Animatie

        //Sunet
        playerSoundManager = this.gameObject.GetComponent<PlayerSounds>();
        //END Sunet

        WeaponChange();
    }

    // Update is called once per frame
    void Update()
    {
        if (viata <= 0)
        {
            Control.PlayForcedAdd();
            SceneManager.LoadScene("Death");
        }
        bool attackButtonState = attackButton.GetComponent<BetterButton>().pressed;
        bool shieldButtonState = shieldButton.GetComponent<BetterButton>().pressed;
        if (shield == false)
        {
            if (joystick.Vertical < -0.5f && coliziuneArma && helper >= interactionCooldown)
            {
                coliziuneArma = false;
                helper = 0;

                if (weapon.gameObject.name == "Fist")
                {
                    weapon = tempWeapon;
                }
                else
                {
                    Destroy(weapon.gameObject);
                    GameObject tmp = Instantiate(weapon.gameObject, this.transform.position+offsetWeapon, Quaternion.identity);
                    tmp.name = tmp.name.Replace("(Clone)", "");
                    tmp.GetComponent<Collider2D>().enabled = true;
                    tmp.GetComponent<Rigidbody2D>().isKinematic = false;
                    tmp.SetActive(true);

                    weapon = tempWeapon;
                }
                tempWeapon.gameObject.SetActive(false);

                WeaponChange();
                //weapon = tempWeapon;
                //test = Instantiate(weapon.gameObject, this.transform.position, Quaternion.identity) as GameObject;
                //test.SetActive(true);


                //Activare animatii
                AnimationCheck();
                //END activare animatii

            }
        }
        helper += Time.deltaTime;
        //Shield
        //if (Input.GetKeyDown(KeyCode.Z))
        if (shieldButtonState == true)
        {
            Transform copil = this.transform.Find("Shield");
            if (copil)
            {
                copil.gameObject.SetActive(true);
                shield = true;
            }
        }
        //if (Input.GetKeyUp(KeyCode.Z))
        else
        {
            Transform copil = this.transform.Find("Shield");
            if (copil)
            {
                copil.gameObject.SetActive(false);
                shield = false;
            }
        }
        //Attac
        if (timeBetweenAttacks <= 0)
        {
            //if (Input.GetKeyDown(KeyCode.LeftControl) && shield == false)
            if (attackButtonState == true && shield == false)
            {
                //Animatie atac in functie de arma
                animator.SetBool("Atac", true);
                //End Animatie
                Attack();
                timeBetweenAttacks = startTimeBetweenAttacks;
            }
        }
        else
        {
            //Animatie terminare
            animator.SetBool("Atac", false);
            //END Animatie terminare
            timeBetweenAttacks -= Time.deltaTime;
        }
    
        if (miscare.directie)
            cercAtac = new Vector3(transform.position.x + offsetAttackRange, transform.position.y + offsetWeapon.y, transform.position.z);
        else
            cercAtac = new Vector3(transform.position.x - offsetAttackRange, transform.position.y + offsetWeapon.y, transform.position.z);
        //END Atac
    }
    //Debug Atac
    private void OnDrawGizmosSelected()
    {
        //Sa vedem cercu de atac
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(cercAtac, attackRange);
    }
   
    void Attack()
    {
        //Toti inamicii din cerc primesc damage
        Collider2D[] enemyToDamage = Physics2D.OverlapCircleAll(cercAtac, attackRange, enemyMask);
        //End Coll
        //Sound
        playerSoundManager.Attack(weapon.gameObject.name);
        //End Sound
        {
            for (int i = 0; i < enemyToDamage.Length; i++)
            {
                enemyToDamage[i].GetComponent<Entity>().GetDamage(weapon.damage);
            }
        }
    }




    void AnimationCheck()
    {
        if (weapon.gameObject.name == "Sword")
        {
            animator.SetLayerWeight(animator.GetLayerIndex("Sword"), 1);
            animator.SetLayerWeight(animator.GetLayerIndex("Halberd"), 0);
            animator.SetLayerWeight(animator.GetLayerIndex("Normal"), 0);
        }
        else if (weapon.gameObject.name == "Halberd")
        {
            animator.SetLayerWeight(animator.GetLayerIndex("Sword"), 0);
            animator.SetLayerWeight(animator.GetLayerIndex("Halberd"), 1);
            animator.SetLayerWeight(animator.GetLayerIndex("Normal"), 0);
        }
        else
        {
            animator.SetLayerWeight(animator.GetLayerIndex("Sword"), 0);
            animator.SetLayerWeight(animator.GetLayerIndex("Halberd"), 0);
            animator.SetLayerWeight(animator.GetLayerIndex("Normal"), 1);
        }
    }

    void WeaponChange()
    {
        GameObject usingHand = GameObject.Find("LeftArmTarget");
        if (usingHand)
        {
            //Weapon
            if (weaponHolder != null)
            {
                Destroy(weaponHolder);
            }
            weaponHolder = Instantiate(weapon.gameObject, this.transform.position,Quaternion.identity);
            if (weaponHolder.GetComponent<Collider2D>() != null)
                weaponHolder.GetComponent<Collider2D>().enabled = false;
            weaponHolder.SetActive(true);
            weaponHolder.transform.SetParent(usingHand.GetComponent<Transform>());
            weaponHolder.name = weaponHolder.name.Replace("(Clone)", "");
            if (weaponHolder.gameObject.name == "Halberd")
            {
                weaponHolder.transform.position = new Vector3(usingHand.transform.position.x - 0.5f, usingHand.transform.position.y + 0.5f, usingHand.transform.position.z);
                weaponHolder.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                weaponHolder.transform.eulerAngles = new Vector3(0, 0, 0);
                weaponHolder.transform.localPosition = new Vector3(usingHand.transform.position.x - 0.5f, usingHand.transform.position.y + 0.5f, usingHand.transform.position.z);
                weaponHolder.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                weaponHolder.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            if (weaponHolder.gameObject.name == "Sword")
            {
                weaponHolder.transform.position = new Vector3(usingHand.transform.position.x -0.5f, usingHand.transform.position.y +0.5f, usingHand.transform.position.z);
                weaponHolder.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                weaponHolder.transform.eulerAngles = new Vector3(0, 0, 0);
                weaponHolder.transform.localPosition = new Vector3(usingHand.transform.position.x - 0.5f, usingHand.transform.position.y + 0.5f, usingHand.transform.position.z);
                weaponHolder.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                weaponHolder.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            //End Weapon
        }

    }
}
