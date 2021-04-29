using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class Control : MonoBehaviour
{

    //Static
    static int level=0;
    static int coins=0;
    static float timePassed=0;


    //On Object
    string gameId = "3851113";
    string videoPlacement = "video";
    bool testMode = true;

    void Start()
    {
        //Debug.Log(level);
        //Debug.Log(coins);

        //Adverts
        Advertisement.Initialize(gameId, testMode);
    }

    private void Update()
    {
        timePassed += Time.deltaTime;        
    }





    //Static Area
    //Adverts
    static public void PlayForcedAdd()
    {
        if (Advertisement.IsReady("video"))
        {
            Advertisement.Show("video");
        }
        else
        {
            Debug.Log("Error advert");
        }
        timePassed = 0;
    }
    static public void PlayAdd()
    {
        if (timePassed>=20)
        {
            if (Advertisement.IsReady("video"))
            {
                Advertisement.Show("video");
            }
            else
            {
                Debug.Log("Error advert");
            }
            timePassed = 0;
        }
    }
      

    //Other things
    static public void SetLevel(int newLevel)
    {
        level = newLevel;
    }

    static public void SetCoins(int newCoins)
    {
        coins = newCoins;
    }

    static public int GetLevel()
    {
        return level;
    }
    static public int GetCoins()
    {
        return coins;
    }
}
