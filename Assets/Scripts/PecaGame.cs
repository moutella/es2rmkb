using System;
using System.Collections;
using UnityEngine;
public class PecaGame : MonoBehaviour
{
    private Peca pecaLogica;
    public SpriteRenderer imagemCarta;
    public TMPro.TextMeshPro textoPeca;
    private Color32 corPeca;
    
    public void criaPeca(Peca peca)
    {

        pecaLogica = peca;
        if (pecaLogica.ehCoringa())
        {
            corPeca = GameObject.FindGameObjectWithTag("GameController").
                GetComponent<ControladorJogo>().
                coresDoJogo[-pecaLogica.getCodigoCor()];
            textoPeca.SetText("@");
            textoPeca.color = corPeca;
        }
        else
        {
            corPeca = GameObject.FindGameObjectWithTag("GameController").
                GetComponent<ControladorJogo>().
                coresDoJogo[pecaLogica.getCodigoCor()];
            textoPeca.SetText(pecaLogica.getValor().ToString());
            textoPeca.color = corPeca;
        }
    }
    public void setInvisivel()
    {
        imagemCarta.enabled = false;
        textoPeca.enabled = false;
    }
    public void setVisivel()
    {
        imagemCarta.enabled = true;
        textoPeca.enabled = true;
    }
}