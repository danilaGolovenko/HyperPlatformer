using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneMonoComponent : MonoBehaviour
{
    public void OnFadeComplete(int sceneToLoadIndex)
    {
        SceneManager.LoadScene(sceneToLoadIndex);
    }
}
