using System;
using System.Collections;
using UnityEngine;
public class PecaGame : MonoBehaviour
{
    private Peca pecaLogica;
    public SpriteRenderer imagemCarta;
    public TMPro.TextMeshPro textoPeca;
    private Color32 corPeca;

    private void LateUpdate()
    {
        if(gameObject.GetComponent<controladorPeca>().enabled == false)
        {
            gameObject.GetComponent<controladorPeca>().enabled = true;
        }
    }
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
    public Peca getPecaLogica()
    {
        return pecaLogica;
    }
    public void setaPosicao(float pos)
    {
        Vector3 pos3d = new Vector3(pos, 0, 0);
        //Debug.Log("SETOU: " + pos3d + "  PECA: " + pecaLogica.getValor());
        transform.localPosition = pos3d;
        pecaLogica.setPosition(transform.position);
    }
}