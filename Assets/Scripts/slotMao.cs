using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slotMao : MonoBehaviour
{
    public GameObject pecaNoLocal;
    public bool vazioBool;
    private void Start()
    {
    }
    public void FixedUpdate()
    {
        vazioBool = vazio();
    }
    public void libera()
    {
        pecaNoLocal = null;
    }
    public void preenche(GameObject peca)
    {
        pecaNoLocal = peca;
    }
    public bool vazio()
    {
        if(pecaNoLocal == null)
        {
            return true;
        }
        return false;
    }
}
