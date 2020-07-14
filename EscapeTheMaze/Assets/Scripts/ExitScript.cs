using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour
{
    public Text gameOverText;

   
    private void OnTriggerEnter(Collider other)
    {
        //When the player gets to exit, switch to end scene
        if (other.gameObject == GameObject.FindWithTag("Player"))
        {
            SceneManager.LoadScene(2);
        }
    }
}
