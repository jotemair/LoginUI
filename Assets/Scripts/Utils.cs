using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static void LoadScene(string path)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(path);
    }
}
