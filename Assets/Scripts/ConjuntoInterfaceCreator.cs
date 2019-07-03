using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConjuntoInterfaceCreator : MonoBehaviour
{
    public GameObject conjuntoPrefab, pecaGamePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void inicializaDeConjuntoLogico(Conjunto conjLogico, ArrayList pecasInterface)
    {
        GameObject tabuleiro = GameObject.FindGameObjectWithTag("Tabuleiro");
        GameObject conjunto = Instantiate(conjuntoPrefab, tabuleiro.transform);
        ConjuntoInterface conjInt = conjunto.GetComponent<ConjuntoInterface>();
        conjInt.inicializa(false);
        conjInt.setaConjLogico(conjLogico);
        conjInt.transform.position = conjLogico.calculaPosPorPecas();
        //conjLogico.printaPecas();
        foreach(GameObject p in pecasInterface)
        {
            p.transform.parent = conjunto.transform;
            conjInt.addPecaInterface(p, false);
        }
        //foreach (Peca p in conjLogico.getPecas())
        //{
        //    GameObject pecaGame = Instantiate(pecaGamePrefab);
        //    PecaGame criadorPecaGame = pecaGame.GetComponent<PecaGame>();
        //    criadorPecaGame.criaPeca(p);
        //    conjInt.addPecaInterface(pecaGame);
        //}
        //conjInt.inserePeca(gameObjet);
        //conjuntoDono = conj;
        conjInt.mudaColisorSize();
        tabuleiro.GetComponent<TabuleiroInterface>().insereConjInt(conjunto);
        tabuleiro.GetComponent<TabuleiroInterface>().ativaColisores();
    }
    public void inicializaParaRollback(Conjunto conjLogico)
    {
        GameObject tabuleiro = GameObject.FindGameObjectWithTag("Tabuleiro");
        GameObject conjunto = Instantiate(conjuntoPrefab, tabuleiro.transform);
        ConjuntoInterface conjInt = conjunto.GetComponent<ConjuntoInterface>();
        conjInt.transform.position = conjLogico.getPos();
        conjInt.inicializa(true);
        conjInt.setaConjLogicoBkp(conjLogico);
        //conjInt.transform.position = conjLogico.calculaPosPorPecas();
        //conjLogico.printaPecas();
        foreach (Peca p in conjLogico.getPecas())
        {
            GameObject peca = Instantiate(pecaGamePrefab);
            
            PecaGame pecaControl = peca.GetComponent<PecaGame>();
            pecaControl.criaPeca(p);
            print("Peca inserida no rollback: " + p.getValor() + "Coringa: " + p.ehCoringa() + "Cor: " + p.getCodigoCor());
            conjInt.addPecaInterface(peca, false);
        }
        //foreach (Peca p in conjLogico.getPecas())
        //{
        //    GameObject pecaGame = Instantiate(pecaGamePrefab);
        //    PecaGame criadorPecaGame = pecaGame.GetComponent<PecaGame>();
        //    criadorPecaGame.criaPeca(p);
        //    conjInt.addPecaInterface(pecaGame);
        //}
        //conjInt.inserePeca(gameObjet);
        //conjuntoDono = conj;
        conjInt.mudaColisorSize();
        tabuleiro.GetComponent<TabuleiroInterface>().insereConjInt(conjunto);
        tabuleiro.GetComponent<TabuleiroInterface>().ativaColisores();
    }


    public void inicializaParaRollbackInteligente(Conjunto conjLogico, int i)
    {
        GameObject tabuleiro = GameObject.FindGameObjectWithTag("Tabuleiro");
        GameObject conjunto = Instantiate(conjuntoPrefab, tabuleiro.transform);
        ConjuntoInterface conjInt = conjunto.GetComponent<ConjuntoInterface>();
        //Vector3 v = new Vector3(-350 +(i%2)*350, 350-(i/2)*50, conjLogico.getPos().z);
        Vector3 v = new Vector3(-2+(i%2)*4, 2-(i/2)*3, 0);
        conjLogico.setPos(v);
        conjInt.transform.position = conjLogico.getPos();
        conjInt.inicializa(true);
        conjInt.setaConjLogicoBkp(conjLogico);
        //conjInt.transform.position = conjLogico.calculaPosPorPecas();
        //conjLogico.printaPecas();
        foreach (Peca p in conjLogico.getPecas())
        {
            GameObject peca = Instantiate(pecaGamePrefab);
            
            PecaGame pecaControl = peca.GetComponent<PecaGame>();
            pecaControl.criaPeca(p);
            print("Peca inserida no rollback: " + p.getValor() + "Coringa: " + p.ehCoringa() + "Cor: " + p.getCodigoCor());
            conjInt.addPecaInterface(peca, false);
        }
        //foreach (Peca p in conjLogico.getPecas())
        //{
        //    GameObject pecaGame = Instantiate(pecaGamePrefab);
        //    PecaGame criadorPecaGame = pecaGame.GetComponent<PecaGame>();
        //    criadorPecaGame.criaPeca(p);
        //    conjInt.addPecaInterface(pecaGame);
        //}
        //conjInt.inserePeca(gameObjet);
        //conjuntoDono = conj;
        conjInt.mudaColisorSize();
        tabuleiro.GetComponent<TabuleiroInterface>().insereConjInt(conjunto);
        tabuleiro.GetComponent<TabuleiroInterface>().ativaColisores();
    }
}
