using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }    
   public void PlayGame()
    {
        Control.SetLevel(1);
        SceneManager.LoadScene("Level1");
    }
    public void Retry()
    {
        Control.SetLevel(1);
        SceneManager.LoadScene("Level1");
    }    
    public void Menu()
    {
        Control.SetLevel(0);
        SceneManager.LoadScene("Meniu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
