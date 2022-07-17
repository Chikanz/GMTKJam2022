using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonScript : MonoBehaviour
{
    public void Start()
    {
        //gameObject.GetComponent<Button>().onClick.AddListener(PlayGame);
        //gameObject.GetComponent<Button>().onClick.AddListener(ExitGame);

    }

    public void PlayGame() 
    {
        SceneManager.LoadScene("WorkerTest");
        Debug.Log("Error");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
