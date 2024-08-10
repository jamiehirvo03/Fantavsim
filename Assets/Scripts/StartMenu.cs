using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public Button startButton;




    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
