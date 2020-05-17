using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestChange : MonoBehaviour
{
    public void Change()
    {
        SceneManager.LoadScene("MapTempTest");
    }

    public void ChangeBack()
    {
        SceneManager.LoadScene("MapExample");
    }
}
