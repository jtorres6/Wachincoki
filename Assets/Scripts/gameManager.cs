using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return _instance; } }
    public GameObject canvas;
    public GameObject wave;
    public Text text1;
    public Text text2;
    public int truckCapacity;

    private static GameManager _instance = null;
    private int player1HP;
    private int player2HP;
    private bool gameOver;
    private int winner;
    private int truckCapacityReached;

    private IEnumerator waveCoroutine;
    private const int _animationTime = 15;

    // https://unity3d.com/es/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager?playlist=17150
    void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        winner = 0;
        gameOver = false;
        player1HP = 10;
        player2HP = 500;
        truckCapacityReached = 0;

        // Instanciamos objetos, marea etc
        waveCoroutine = WaitAndWave(10);
        StartCoroutine(waveCoroutine);
    }

    // Update is called once per frame
    void Update()
    {

        text1.text = player1HP.ToString();
        text2.text = player2HP.ToString();

        // si el jugador tira el tarro al otro, pasa el propietario al otro

        if (gameOver){
            winner = player1HP > player2HP ? 1 : 2;

            ResetPlaygame();
            canvas.GetComponent<pause>().ExitMainMenu();
        }
    }

    public void DecreaseHP(int playerID, int value) {
        if (playerID == 1) {
            player1HP -= value;
        } else {
            player2HP -= value;
        }

        if (player1HP <= 0 || player2HP <= 0) {
            gameOver = true;
        }
    }

    public void IncreaseHP(int playerID, int value) {
        if (playerID == 1) {
            player1HP += value;
        } else {
            player2HP += value;
        }
    }

    public void ResetPlaygame() {
        winner = 0;
        gameOver = false;
        player1HP = 10;
        player2HP = 500;

        waveCoroutine = WaitAndWave(10);
        StartCoroutine(waveCoroutine);
    }

    public void ResetTruck() {
        truckCapacityReached = 0;
    }
 
    public void IncreaseTruckReached(int value) {
        truckCapacityReached += value;
    }
 
    public int GetTruckCurrentCapacity() {
        return truckCapacityReached;
    }

    private IEnumerator WaitAndWave(int waitTime) {
        Debug.Log("In coroutine");
        while (!gameOver) {
            Animator waveAnim = wave.GetComponent<Animator>();
            waveAnim.SetTrigger("Activate");
            yield return new WaitForSeconds(_animationTime  + 10);
        }
    }
}
