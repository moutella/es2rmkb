using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorJogo : MonoBehaviour
{
    private Tabuleiro tabuleiroAtual;
    private Deck deckAtual;
    private ArrayList tabuleirosValidos;

    void Start()
    {
        tabuleirosValidos = new ArrayList();
        StartCoroutine(GameStart());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    private IEnumerator GameStart()
    {
        deckAtual = new Deck();
        tabuleiroAtual = new Tabuleiro();
        Tabuleiro cloneBase = tabuleiroAtual.cloneTabuleiro();
        tabuleirosValidos.Add(cloneBase);
        yield return null;
    }
}
