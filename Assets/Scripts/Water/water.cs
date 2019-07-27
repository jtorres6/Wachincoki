using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{
    public float minY;
    public float maxY;
    
    private Animator waveAnimation;

    private enum Direction {
        UP = 0,
        DOWN = 1,
    }

    private Direction direction;

    // Start is called before the first frame update
    void Start()
    {
        waveAnimation = GetComponent<Animator>();
        waveAnimation.SetTrigger("Activate");
    }
}
