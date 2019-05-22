﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConjuntoInterface : MonoBehaviour
{
    float distance;
    private static float tamanhoPeca = 2 / 3f, distanciaPecas = 0.7f;
    private Conjunto conjuntoLogico;
    public GameObject conjuntoPrefab, pecaGamePrefab;
    public ArrayList pecasObjFilho;
    private BoxCollider2D colisor;


    /// <summary>
    /// FUNÇÕES CHAMADAS PELO UNITY
    /// </summary>
    void Update()
    {
        distance = -Camera.main.transform.position.z;
    }
    public void LateUpdate()
    {
        if (gameObject.name.Equals("ConjuntoInterface(Clone)(Clone)"))
        {
            Destroy(gameObject);
        }
        //transform.localPosition = recalculaPosition();
        if (transform.childCount == 0)
        {
            GameObject tabuleiro = GameObject.FindGameObjectWithTag("Tabuleiro");
            tabuleiro.GetComponent<TabuleiroInterface>().removeConjInt(gameObject);
            Destroy(gameObject);
        }
    }


    private void OnMouseDown()

    {
        //Inserir código que vai ser executado quando um player clicar num conjunto[com a tecla shift]
    }
    private void OnMouseDrag()
    {

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnMouseUp()
    {
        mudaPosPecasFilho();
    }
    /// <summary>
    /// FIM DAS FUNÇÕES CHAMADAS PELO UNITY
    /// </summary>
    private void Start()
    {
        //Debug.Break();
    }
    public void inicializa()
    {
        this.colisor = GetComponent<BoxCollider2D>();
        this.conjuntoLogico = new Conjunto();
        this.pecasObjFilho = new ArrayList();
        GameObject tabuleiro = GameObject.FindGameObjectWithTag("Tabuleiro");
        Tabuleiro tabAtual = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorJogo>().getTabuleiroAtual();
        tabAtual.insereConjunto(conjuntoLogico);
        tabuleiro.GetComponent<TabuleiroInterface>().desativaColisores();
//        conjuntoLogico.setPos(transform.localPosition);
    }
    public void addPecaInterface(GameObject peca)
    {
        pecasObjFilho.Add(peca);
        peca.transform.parent = transform;
        if (pecasObjFilho.Count > 1)
        {
            transform.localPosition += new Vector3(0.35f, 0, 0);
        }
        mudaPosPecasFilho();
        colisor.size = new Vector2(tamanhoPeca * transform.childCount, 1);
    }
    public void inserePeca(GameObject peca)
    {
        Peca p = peca.GetComponent<PecaGame>().getPecaLogica();
        if (!conjuntoLogico.getPecas().Contains(p)) {
            //Debug.Log(tamanhoPeca);
            if (!pecasObjFilho.Contains(peca)) {
                pecasObjFilho.Add(peca);
            }
            //Debug.Log(pecasObjFilho.Count);
            conjuntoLogico.inserePeca(peca.GetComponent<PecaGame>().getPecaLogica());
            //Debug.Log(tamanhoPeca * pecasObjFilho.Count);
            if (pecasObjFilho.Count > 1) {
                transform.localPosition += new Vector3(0.35f, 0, 0);
            }
            peca.transform.parent = transform;
            mudaPosPecasFilho();
            colisor.size = new Vector2(tamanhoPeca * transform.childCount, 1);
        }
    }
    public void inserePecaAntes(GameObject peca)
    {
        Peca p = peca.GetComponent<PecaGame>().getPecaLogica();
        if (!conjuntoLogico.getPecas().Contains(p))
        {
            if (!pecasObjFilho.Contains(peca))
            {
                pecasObjFilho.Insert(0, peca);
            }
            conjuntoLogico.inserePecaAntes(peca.GetComponent<PecaGame>().getPecaLogica());
            if (pecasObjFilho.Count > 1)
            {
                transform.localPosition -= new Vector3(0.35f, 0, 0);
            }
            peca.transform.parent = transform;

            mudaPosPecasFilho();
            colisor.size = new Vector2(tamanhoPeca * transform.childCount, 1);
        }
    }
    
    public void removePeca(GameObject peca)
    {
        //Debug.ClearDeveloperConsole();
        Debug.Log("--------------COMECOU UM SPLIT----------");
        int x = pecasObjFilho.Count;
        Peca p = peca.GetComponent<PecaGame>().getPecaLogica();
        Conjunto extra = conjuntoLogico.divide(p);
        int index = pecasObjFilho.IndexOf(peca);
        pecasObjFilho.Remove(peca);
        if (extra != null)
        {

            GameObject tab = GameObject.FindGameObjectWithTag("Tabuleiro");
            ConjuntoInterfaceCreator inicializador = tab.GetComponent<ConjuntoInterfaceCreator>();

            Debug.Log("TOTAL LENGHT: " + (pecasObjFilho.Count + 1));

            ArrayList pecasEsq = pecasObjFilho.GetRange(0, conjuntoLogico.getNumPecas());
            Debug.Log("pecas esq length: " + pecasEsq.Count);

            ArrayList pecasDir = pecasObjFilho.GetRange(index, extra.getNumPecas());
            Debug.Log("pecas dir length: " + pecasDir.Count);

            inicializador.inicializaDeConjuntoLogico(conjuntoLogico, pecasEsq);
            inicializador.inicializaDeConjuntoLogico(extra, pecasDir);
            Tabuleiro tabAtual = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorJogo>().getTabuleiroAtual();
            tabAtual.removeConjunto(conjuntoLogico);
            //Debug.Break();
            Destroy(gameObject);
            //Debug.Break();
        }
        else
        {
            GameObject tabuleiro = GameObject.FindGameObjectWithTag("Tabuleiro");
            tabuleiro.GetComponent<TabuleiroInterface>().ativaColisores();

            if (transform.childCount == 0)
            {
                Destroy(gameObject);
            }
            if (conjuntoLogico.getPecas().Count == 0)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.localPosition = recalculaPosition();
                conjuntoLogico.setPos(transform.localPosition);
                mudaPosPecasFilho();
            }
            colisor.size = new Vector2((tamanhoPeca * (x - 1)), 1);
        }
    }

    public Vector3 recalculaPosition() //Quando retirar vai setar a nova raiz do conjunto no meio do conjunto restante
    {
        Vector3 posicao = Vector3.zero;
        //Debug.Log("NUMERO DE ELEMENTOS: " + pecasObjFilho.Count + "NUMERO PECAS: " + conjuntoLogico.getPecas().Count);
        foreach (GameObject p in pecasObjFilho)
        {
            //Debug.Log(p.transform.position);
            posicao += p.transform.position;
            
        }
        Debug.Log(posicao);
        posicao = posicao / pecasObjFilho.Count;
        return posicao;
    }
    
    
    public void setaConjLogico(Conjunto conj)
    {

        Tabuleiro tabAtual = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorJogo>().getTabuleiroAtual();
        tabAtual.removeConjunto(conjuntoLogico);
        this.conjuntoLogico = conj;
        tabAtual.insereConjunto(conjuntoLogico);
    }
    public void mudaPosPecasFilho()    //Após a inserção ou remoção de uma peça, relativo a posição do conjunto
    {
        //Debug.Log("QUANTIDADE DE PECAS: " + pecasObjFilho.Count);
        //conjuntoLogico.printaPecas();
            if (conjuntoLogico.getNumPecas() % 2 == 0) //numero par de peças
            {
                int numeroPecasExtrasPorlado = pecasObjFilho.Count / 2 - 1;
            float offset = -0.35f - numeroPecasExtrasPorlado * distanciaPecas;
                foreach(GameObject peca in pecasObjFilho)
                {
                    //Debug.Log(offset);
                    peca.GetComponent<PecaGame>().setaPosicao(offset);
                    offset += distanciaPecas;
                    
                }
            }
            else //numero impar de peças
            {

                float offset = - ((pecasObjFilho.Count-1)/2) * distanciaPecas;
                
                foreach (GameObject peca in pecasObjFilho)
                {
                    //Debug.Log(offset);
                    peca.GetComponent<PecaGame>().setaPosicao(offset);
                    offset += distanciaPecas;
                }
            }
    }
    
}