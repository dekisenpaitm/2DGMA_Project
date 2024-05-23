using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private string _sceneName;

    public void SwitchScene()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
