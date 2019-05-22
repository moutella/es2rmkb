using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pecaGameUI : MonoBehaviour
{
    public TextMeshProUGUI pecaUiTexto;
    public Peca pecaLogica;
    private Color32 corPeca;
    // Start is called before the first frame update
    void OnCreate() { 
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Peca getPeca()
    {
        return pecaLogica;
    }


    public void criaPeca(Peca p)
    {
        pecaLogica = p;
        if (pecaLogica.getCodigoCor() < 0)
        {
            pecaLogica.setCodigoCor(0);
        }
        corPeca = GameObject.FindGameObjectWithTag("GameController").
                GetComponent<ControladorJogo>().
                coresDoJogo[pecaLogica.getCodigoCor()];
        pecaUiTexto.SetText(pecaLogica.getValor().ToString());
        if (!pecaLogica.ehCoringa())
        {
            pecaUiTexto.color = corPeca;
            pecaUiTexto.SetText(pecaLogica.getValor().ToString());
        }
        else
        {
            pecaUiTexto.SetText("@");
            pecaUiTexto.color = corPeca;
        }
    }
}
