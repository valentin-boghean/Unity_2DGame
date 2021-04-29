using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    public float viteza;
    [Range(1,20)]
    public float fortaSaritura;
    public Animator animator;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public bool debug = false;

    [HideInInspector]
    public bool directie = true; //True - dreapta, false stanga

    public LayerMask podea;

    private Rigidbody2D body= null;
    private CircleCollider2D colider = null;

    private PlayerSounds playerSoundManager;

    public Joystick joystick;
    private float input;
    private float inputH;

    private float jumpCooldown = 1f;
    private float jumpAux = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        colider = this.GetComponent<CircleCollider2D>();

        //Sunet
        playerSoundManager = this.gameObject.GetComponent<PlayerSounds>();
        //END Sunet
    }
    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Player>().shield == false)
        {

            input = joystick.Horizontal;
            inputH = joystick.Vertical;
            if (debug == true)
                input = Input.GetAxis("Horizontal"); //Stanga dreapta
                                                     //Move
            body.velocity = new UnityEngine.Vector2(input * viteza, body.velocity.y); //proprietate
            //Move END
            //Smooth Jump
            if (body.velocity.y < 0)
            {
                body.velocity += UnityEngine.Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            //else if (body.velocity.y > 0 && !(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)))
            else if (body.velocity.y > 0 && (inputH < 0.5f))
            {
                body.velocity += UnityEngine.Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
            //Smooth Jump END
            //Jump
            if ((inputH>0.5f || /*debug */(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))) && PePamant())
            {
                if (BlockAbove())
                {
                    if (jumpAux >= jumpCooldown)
                    {
                        Jump();
                    }
                }
                else
                {
                    Jump();
                }
            }
            else
            {
                animator.SetBool("Jumping", false);
            }
            //Jump END
        }
        else
        {
            input = 0;
            body.velocity = new UnityEngine.Vector2(0, body.velocity.y);

        }
        //Animatation Run
        if (input < 0 && directie)
        {
            SchimbaDirectie();
        }
        if (input > 0 && !directie)
        {
            SchimbaDirectie();
        }

        animator.SetFloat("Speed", Mathf.Abs(body.velocity.x));
        //Animation Run END
        //Sound
        if (input != 0 && PePamant())
        {
            playerSoundManager.MovementStart();
        }
        else
        {
            playerSoundManager.MovementStop();
        }
        //Sound End
        jumpAux += Time.deltaTime;
    }
    void FixedUpdate()
    {


    }
    void SchimbaDirectie()
    {
        directie = !directie;
        transform.Rotate(UnityEngine.Vector3.up * 180);
    }

    bool PePamant()
    {
        /*ContactFilter2D contact = new ContactFilter2D();
        contact.ClearLayerMask();
        //contact.layerMask = podea;   --De tinut minte
        //contact.useLayerMask = true; -
        //Sau
        contact.SetLayerMask(podea); //Functia seteaza automat pe podea si da enable tru la Masca
        return Physics2D.IsTouching(colider, contact);*/

        /*float offset = 0.3f;
        CircleCollider2D collider = this.gameObject.GetComponent<CircleCollider2D>();
        RaycastHit2D col =  Physics2D.Raycast(collider.bounds.center, UnityEngine.Vector2.down, collider.bounds.extents.y+offset,podea);


        Debug.DrawRay(collider.bounds.center, UnityEngine.Vector2.down * (collider.bounds.extents.y + offset));
        return col.collider != null;*/

        float offset = 0.2f;
        CircleCollider2D collider = this.gameObject.GetComponent<CircleCollider2D>();
        RaycastHit2D col = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size,0f,UnityEngine.Vector2.down, collider.bounds.extents.y + offset, podea);

        return col.collider != null;

    }

    bool BlockAbove()
    {

        float offset = 0.2f;
        BoxCollider2D collider = this.gameObject.GetComponent<BoxCollider2D>();
        //Debug.DrawRay(collider.bounds.center, UnityEngine.Vector2.up * (collider.bounds.extents.y - offset));
        RaycastHit2D col = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, UnityEngine.Vector2.up, collider.bounds.extents.y + offset + 1, podea);
        //Debug.Log(col.collider);
        return col.collider != null;

    }

    void Jump()
    {
        //Jump
        body.velocity = UnityEngine.Vector2.up * fortaSaritura;
        //End Jump
        //Animation
        animator.SetBool("Jumping", true);
        //EndAnimation
        //Sound
        playerSoundManager.Jump();
        //End Sound
        jumpAux = 0;
    }
}
