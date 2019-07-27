using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private int player1HP = 100;
    private int player2HP = 100;

    private bool isRunning = false;
    private bool gameOver = false;

    private int winner = 0;

    void Start()
    {
        winner = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (isRunning){

            // instanciamos objetos, marea etc

            // si la marea elimina un determinado objeto, resta vida del jugador de ese objeto

            // si el jugador tira el tarro al otro, pasa el propietario al otro

            // si el jugador tira el objeto al camion, sumale la mitad del valor de la vida

            if (player1HP <= 0){
                winner = 2;
                gameOver = true;
            }
            else if (player2HP <= 0){
                winner = 1;
                gameOver = true;
            }

            if (gameOver == false){

            }
            else{
                //muestra por la UI lo de quien ha ganado y despues isrunning = false
            }
        }
        
    }
}
