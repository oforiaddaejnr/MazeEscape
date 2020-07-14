using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
   //Load game scene
    public void loadTrack(int trackIndex)
    {
        SceneManager.LoadScene(trackIndex);
    }
}
