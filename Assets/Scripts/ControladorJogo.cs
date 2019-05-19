using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorJogo : MonoBehaviour
{
    private Tabuleiro atual;
    private ArrayList tabuleirosValidos;

    void Start()
    {
        atual = new Tabuleiro();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
