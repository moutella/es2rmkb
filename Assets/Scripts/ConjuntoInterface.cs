using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConjuntoInterface : MonoBehaviour
{
    private static float tamanhoPeca = 2 / 3f, distanciaPecas = 0.7f;
    private Conjunto conjuntoLogico;
    public ArrayList pecasObjFilho;
    private BoxCollider2D colisor;
    


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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(pecasObjFilho.Count);
        }
    }
    public void removePeca(GameObject peca)
    {
        int x = pecasObjFilho.Count;
        pecasObjFilho.Remove(peca);
        Peca p = peca.GetComponent<PecaGame>().getPecaLogica();
        Conjunto extra = conjuntoLogico.divide(p);
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
            //mudaPosPecasFilho();
        }
        colisor.size = new Vector2((tamanhoPeca * (x - 1)), 1);
        /*if (extra != null)
        {
            //TODO: fazer criador do conjuntointerface do conjunto que sobrou
        }*/
    }
    public void LateUpdate()
    {
        //transform.localPosition = recalculaPosition();
        if (transform.childCount == 0)
        {
            GameObject tabuleiro = GameObject.FindGameObjectWithTag("Tabuleiro");
            tabuleiro.GetComponent<TabuleiroInterface>().removeConjInt(gameObject);
            Destroy(gameObject);
        }
    }
    public Vector3 recalculaPosition()
    {
        Vector3 posicao = Vector3.zero;
        Debug.Log("NUMERO DE ELEMENTOS: " + pecasObjFilho.Count + "NUMERO PECAS: " + conjuntoLogico.getPecas().Count);
        foreach(GameObject p in pecasObjFilho)
        {
            Debug.Log(p.transform.position);
            posicao += p.transform.position;
        }
        Debug.Log(posicao);
        posicao = posicao / pecasObjFilho.Count;
        return posicao;
    }
    public void inicializa()
    {
        colisor = GetComponent<BoxCollider2D>();
        conjuntoLogico = new Conjunto();
        pecasObjFilho = new ArrayList();
        GameObject tabuleiro = GameObject.FindGameObjectWithTag("Tabuleiro");
        tabuleiro.GetComponent<TabuleiroInterface>().desativaColisores();
    }

    void mudaPosPecasFilho()
    {

        if (transform.childCount > 1) {
            if (conjuntoLogico.getNumPecas() % 2 == 0) //numero par de peças
            {
                float offset = -0.35f - distanciaPecas * (pecasObjFilho.Count/2 - 1);
                foreach(GameObject peca in pecasObjFilho)
                {
                    peca.GetComponent<PecaGame>().setaPosicao(offset);
                    offset += distanciaPecas;
                }
            }
            else //numero impar de peças
            {
                float offset = -((conjuntoLogico.getNumPecas()-1)/2) * distanciaPecas;
                foreach (GameObject peca in pecasObjFilho)
                {
                    peca.GetComponent<PecaGame>().setaPosicao(offset);
                    offset += distanciaPecas;
                }
            }
        }
        else
        {
            ((GameObject)pecasObjFilho[0]).transform.position = transform.position;
        }
    }
    
}
