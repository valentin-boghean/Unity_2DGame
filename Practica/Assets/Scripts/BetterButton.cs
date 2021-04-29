using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BetterButton : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update
    public bool pressed;
    void Start()
    {
        pressed = false;
    }

    // Update is called once per frame
    public void OnPointerDown(PointerEventData s)
    {
        pressed = true;
    }
    public void OnPointerUp(PointerEventData s)
    {
        pressed = false;
    }
}
