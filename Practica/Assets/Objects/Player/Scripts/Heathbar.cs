using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heathbar : MonoBehaviour
{
    public float flickSpeed=0.3f;
    public int numberOfFlicks = 3;
    private int viata;
    public GameObject barPrefab;
    public Sprite[] vectorViata;

    private GameObject bar;
    private bool directie=true;

    private float input;
    private Joystick joystick;

    /*private void OnCollisionEnter2D(Collision2D other)
    {

        

        //De adaugat chestie de disparitie
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*viata = this.GetComponent<Player>().viata;
        bar.GetComponent<SpriteRenderer>().sprite = vectorViata[viata - 1];

        if (other.gameObject.name.Contains("Arrow") == true)
        {
            StartCoroutine(Flick());
        }*/
    }
    void Start()
    {
        viata = this.GetComponent<Player>().viata;

        //Transform newParent = nextOpenSpot.GetComponent<Transform>();
        bar = Instantiate(barPrefab, this.gameObject.transform.position, Quaternion.identity);
        bar.transform.SetParent(this.gameObject.transform);
        bar.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 1.5f);
        bar.GetComponent<SpriteRenderer>().sprite = vectorViata[viata-1];
        bar.SetActive(false);

        joystick = this.GetComponent<Movement>().joystick;
    }

    // Update is called once per frame
    void Update()
    {
        //De vazut poate se poate face mai eficient
        //input = Input.GetAxis("Horizontal");
        input = joystick.Horizontal;
        if (input < 0 && directie)
        {
            SchimbaDirectie();
        }
        if (input > 0 && !directie)
        {
            SchimbaDirectie();
        }
        //bar.transform.Rotate(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z); Prostie faina

    }
    void SchimbaDirectie()
    {
        directie = !directie;
        bar.transform.Rotate(UnityEngine.Vector3.up * 180);
    }
    public void StartFlick()
    {
        StartCoroutine(Flick());
    }
    public IEnumerator Flick()
    {
        for (int i = 0; i < numberOfFlicks*2; i ++)
        {
            bar.gameObject.SetActive(!bar.gameObject.activeSelf);
            yield return new WaitForSeconds(flickSpeed);
        }
        yield return null;
    }
    public void SetHealthBar(int val)
    {
        if (val-1>-1 && val-1 <11)
            bar.GetComponent<SpriteRenderer>().sprite = vectorViata[val - 1];
    }
}
