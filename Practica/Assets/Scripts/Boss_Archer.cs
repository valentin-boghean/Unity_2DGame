using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Boss_Archer : Entity
{
    public float followRange;

    bool shoting = false;
    bool move = false;

    Transform player;

    public Animator animator;
    public GameObject smoke;
    public GameObject arrow;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("ob_Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (viata>0)
        {
            /*if (Vector2.Distance(player.position,this.transform.position)<15f)
            {
                Vector2.MoveTowards(this.transform.position, player.position, vitezaMiscare * Time.deltaTime);
            }*/


            float dist = Vector2.Distance(player.transform.position, this.transform.position);
            if (dist > followRange  && shoting == false && player != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, vitezaMiscare * Time.deltaTime);
                animator.SetBool("Run", true);
                move = true;
            }
            else //iddle
            {
                animator.SetBool("Run", false);
                move = false;
            }
            if (move == false && player!=null && dist < followRange)
            {
                shoting = true;
                animator.SetBool("Shot", true);
            }
            else
            {
                animator.SetBool("Shot", false);
                shoting = false;
            }
        }
        else
        {
            Instantiate(smoke, transform.position, Quaternion.identity);
            SceneManager.LoadScene("End Scene");
            Destroy(gameObject);
        }
    }
    public void ShotArrowUp()
    {
        Instantiate(arrow, transform.position, Quaternion.identity);
    }
    /*public IEnumerator FlickS()
    {
        for (int i = 0; i < numberOfFlicks * 2; i++)
        {
            bar.gameObject.SetActive(!bar.gameObject.activeSelf);
            yield return new WaitForSeconds(flickSpeed);
        }
        
    }*/
    /*void Shot()
    {
       
    }*/
}
