using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;

    public static GameManager Instance { get { return _instance; } }

    private int player1HP;
    private int player2HP;
    
    public Text text1;
    public Text text2;

    private bool isRunning;
    private bool gameOver;

    private int winner;

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
        isRunning = false;
        player1HP = 1000;
        player2HP = 1000;
    }

    // Update is called once per frame
    void Update()
    {

        text1.text = player1HP.ToString();
        text2.text = player2HP.ToString();

        if (isRunning){

            // instanciamos objetos, marea etc

            // si la marea elimina un determinado objeto, resta vida del jugador de ese objeto

            // si el jugador tira el tarro al otro, pasa el propietario al otro

            // si el jugador tira el objeto al camion, sumale la mitad del valor de la vida
            if (gameOver){
                winner = player1HP > player2HP ? 1 : 2;
            }
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

        Debug.Log("Player1 HP: " + player1HP);
        Debug.Log("Player2 HP: " + player2HP);
    }

    public void IncreaseHP(int playerID, int value) {
        if (playerID == 1) {
            player1HP += value;
        } else {
            player2HP += value;
        }
    }
}
