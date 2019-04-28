using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class controladorPecaMenu : MonoBehaviour
{
    // Start is called before the first frame update
    float distance = 10;
    public int valor, tipo;
    public TextMeshPro texto;
    
    void Start()
    {
        
        valor = Random.Range(1, 13);
        tipo = Random.Range(0, 3);
        texto.SetText(valor.ToString());
        texto.color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }
    }
    private void LateUpdate()
    {
        
    }
}
