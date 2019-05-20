using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorJogo : MonoBehaviour
{
    public Color32[] coresDoJogo;
    public maoUI maoInterface;
    private MaoUsuario maoLogica;
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
        maoLogica = new MaoUsuario();
        Tabuleiro cloneBase = tabuleiroAtual.cloneTabuleiro();
        maoLogica.insereMaoInicial(deckAtual.pegaCartasIniciais());
        tabuleirosValidos.Add(cloneBase);
        maoInterface.setMaoLogica(maoLogica);
        yield return null;
    }
}
