using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUILoader : MonoBehaviour
{
    public static PlayerUILoader instance = null;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            SceneManager.LoadScene("MapExample");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            ToggleUI();
    }

    public void ToggleUI()
    {
        if (SceneManager.GetSceneByName("PlayerUI").isLoaded == false)
            SceneManager.LoadScene("PlayerUI", LoadSceneMode.Additive);
        else
            SceneManager.UnloadSceneAsync("PlayerUI");
    }
}
