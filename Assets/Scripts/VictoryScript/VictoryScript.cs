using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScene : MonoBehaviour
{
    public void BackToMenu()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 85);

    }

  
}
