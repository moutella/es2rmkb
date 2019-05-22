﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maoUI : MonoBehaviour
{
    public MaoUsuario maoLogica;
    private RectTransform transformProprio;
    public GameObject pecaPrefab;
    public GameObject[] slots;
    public ArrayList pecas;
    // Start is called before the first frame update
    void Start()
    {
        transformProprio = GetComponent<RectTransform>();
        pecas = new ArrayList();
        maoLogica = new MaoUsuario();
    }
    public GameObject getPrimeiroVazio()
    {

        foreach (GameObject slot in slots)
        {
            if (slot.GetComponent<slotMao>().vazio())
                {                
                    return slot;
            }
        }
        return null;
    }
    void Update()
    {
        
    }
    public void setMaoInicial(ArrayList mao)
    {
        maoLogica.insereMaoInicial(mao);
        arranjaPecas();
        
    }
    public void arranjaPecas()
    {
        foreach (Peca p in maoLogica.getPecas())
        {
            GameObject peca = Instantiate(pecaPrefab, this.transform);
            peca.GetComponent<pecaGameUI>().criaPeca(p);
            GameObject slot = getPrimeiroVazio();
            slot.GetComponent<slotMao>().preenche(peca);
            Debug.Log("Preencheu: " + slot.name);
            peca.GetComponent<pecaDragUI>().slotAtual = slot;
            peca.GetComponent<RectTransform>().SetPositionAndRotation(slot.transform.position, Quaternion.identity);
            pecas.Add(peca);
        }
    }
    public void liberaTodos()
    {
        foreach(GameObject p in pecas)
        {
            Destroy(p);
        }
        foreach(GameObject slot in slots)
        {
            slot.GetComponent<slotMao>().libera();
        }
    }
    public void sortMao()
    {
        liberaTodos();
        maoLogica.arrumaSequencial();
        arranjaPecas();
    }
    public void comprarPeca()
    {
        
    }
    

    //----------------------Métodos de Comunicação com MaoUsuario---------------------------------------------
    public void rollbackPecas() {
        maoLogica.rollbackPecas();
    }
    public int getPontosDaJogada(){
        return maoLogica.pontuacaoJogada();
    }
    public bool getPrimeiraJogada(){
        return maoLogica.getPrimeiraJogada();
    }
    public void setPrimeiraJogada(bool valor){
        maoLogica.setPrimeiraJogada(valor);
    }
    public void limpaJogada(){
        maoLogica.limpaJogada();
    }
    public bool jogouAlgumaPeca(){
        return maoLogica.jogouPeca();
    }
    public bool estavaNaMao(Peca p){
        return maoLogica.estavaNaMao();
    }
}