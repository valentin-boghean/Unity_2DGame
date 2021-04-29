using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioClip[] jump;
    public AudioClip attackFist;
    public AudioClip attackSword;
    public AudioClip attackHalberd;

    public AudioClip movement;
    //public AudioSource[] hitOrAttack;


    private GameObject soundGO;
    private AudioSource sursa;
    private AudioSource sursaStep;
    void Start()
    {
        soundGO = new GameObject("Sound");
        soundGO.transform.SetParent(this.transform);
        sursa = soundGO.AddComponent<AudioSource>();
        sursaStep = soundGO.AddComponent<AudioSource>();
        sursaStep.clip = movement;
    }
    public void Jump()
    {
        int clip = Random.Range(0, 4);
        if (!sursa.isPlaying)
            sursa.PlayOneShot(jump[clip]);
    }
    public void Attack(string numeArma)
    {
        if (numeArma == "Fist")
        {
            sursa.PlayOneShot(attackFist);
        }
        if (numeArma == "Sword")
        {
            sursa.PlayOneShot(attackSword);
        }
        if (numeArma =="Halberd")
        {
            sursa.PlayOneShot(attackHalberd);
        }
    }
    public void MovementStart()
    {
        if (!sursaStep.isPlaying)
        {
            sursaStep.Play();
        }
         
    }
    public void MovementStop()
    {
        sursaStep.Stop();
    }
}
