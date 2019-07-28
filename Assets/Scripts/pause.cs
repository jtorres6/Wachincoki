using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{

    public static bool paused = false;
    public static bool onMainMenu = true;

    // Start is called before the first frame update
    public GameObject pausaUI;
    public GameObject mainMenuUI;

    void Start() {
        pausaUI.SetActive(false);

        ToMainMenu();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)){
            if (onMainMenu) {
                ExitMainMenu();
            } else {
                if (paused){
                    Resume();
                } else{
                    Pause();
                }
            }
        }
    }

    public void Resume() {
        pausaUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;

    }

    public void Pause() {
        pausaUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void ToMainMenu() {
        mainMenuUI.SetActive(true);
        Time.timeScale = 0f;
        onMainMenu = true;
    }

    public void ExitMainMenu() {
        mainMenuUI.SetActive(false);
        Time.timeScale = 1f;
        onMainMenu = false;
    }
}
