using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Entity : MonoBehaviour
    {
        [Header("Specificatii entitate")]
        public int viata;

        public float vitezaMiscare;
        public float vitezaAtac;

        [HideInInspector]
        public bool directie = false;

        //private Transform player;
        // Start is called before the first frame update
         /*void Start()
         {
                player = GameObject.Find("ob_Player").transform;
         }

         //Update is called once per frame*/
         /*void Update()
         {

         }*/

        public void GetDamage(int damage)
        {
            viata -= damage;
        }
        public int CheckArea()
        {
        //0 stanga, 1 dreapta, 3 sus, 2 jos
            Transform player = GameObject.Find("ob_Player").transform;
            Vector2 relativePoint = transform.InverseTransformPoint(player.transform.position);
            if (relativePoint.x < 0f && Mathf.Abs(relativePoint.x) > Mathf.Abs(relativePoint.y))
            {
            return 0;
            }
            if (relativePoint.x > 0f && Mathf.Abs(relativePoint.x) > Mathf.Abs(relativePoint.y))
            {
            return 1;
            }
            if (relativePoint.y > 0 && Mathf.Abs(relativePoint.x) < Mathf.Abs(relativePoint.y))
            {
            return 2;
            }
            if (relativePoint.y < 0 && Mathf.Abs(relativePoint.x) < Mathf.Abs(relativePoint.y))
            {
            return 3;
            }
        return 0;
        }
        public void CheckDir()
    {
        if (CheckArea() == 0 && !directie)
        {
            directie = !directie;
        }
        if (CheckArea() == 1 && directie)
        {
            directie = !directie;
            transform.Rotate(UnityEngine.Vector3.up * 180);
        }
    }
    }

