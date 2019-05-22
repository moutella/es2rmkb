using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabuleiroInterface : MonoBehaviour
{
    public ArrayList conjuntosInterfaces;
    //public GameObject[] conjuntosInterfaces;
    // Start is called before the first frame update
    void Start()
    {
      conjuntosInterfaces = new ArrayList();
      
    }

    // Update is called once per frame
    
    public void insereConjInt(GameObject conj)
    {
        conjuntosInterfaces.Add(conj);
    }
    public void removeConjInt(GameObject conj)
    {
        conjuntosInterfaces.Remove(conj);
    }
    public void desativaColisores()
    {
        List<GameObject> desativaEm = new List<GameObject>();
        {
            foreach (GameObject conj in conjuntosInterfaces)
            {
                if (conj != null)
                {
                    desativaEm.Add(conj);
                }
            }
        }
        foreach(GameObject go in desativaEm)
        {
            go.GetComponent<Collider2D>().enabled = false;
        }
    }
    public void ativaColisores()
    {
        List<GameObject> desativaEm = new List<GameObject>();
        {
            foreach (GameObject conj in conjuntosInterfaces)
            {
                if (conj != null)
                {
                    desativaEm.Add(conj);
                }
            }
        }
        foreach (GameObject go in desativaEm)
        {
            go.GetComponent<Collider2D>().enabled = true;
        }
    }
}
