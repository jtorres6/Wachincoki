using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{

    public static bool paused = false;
    // Start is called before the first frame update
    public GameObject pausaUI;

    void Start(){
        pausaUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)){
            if (paused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    void Resume(){

        pausaUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;

    }

    void Pause(){

        pausaUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;

    }
}
