using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Parallax : MonoBehaviour
    
{
    private float start, lungimea;
    public GameObject camera;
    public float  effectulParallax;
    // Start is called before the first frame update
    void Start()
    {
        start = transform.position.x;   // pozitia x a obiectului curent
        lungimea = GetComponent<SpriteRenderer>().bounds.size.x;  // lungimea imaginii pe axa OX
    }

    // Update is called once per frame
    void Update()
    {
        // calcularea distantei parcurse relativ la camera
        float temp = (camera.transform.position.x * (1 - effectulParallax));
        // calcularea distantei in functie de camera si effectul Parallax
        float distanta = (camera.transform.position.x * effectulParallax);
        // setarea noii pozitii a obiectului dupa distanta calculata
        transform.position = new Vector3(start + distanta, transform.position.y, transform.position.z); 
        
        // mutarea imaginilor de fiecare daca cand depasim o anumita valoare calculata la dreapta sau la stanga
        if (temp > start + lungimea)
            start += lungimea;
        else if (temp < start - lungimea)
            start -= lungimea;
    }
}
