using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class modoFlipper : MonoBehaviour
{
    public List<Sprite> imagensModo;
    private ControladorJogo Controlador;
    public Image imagemModo;
    void Start()
    {
        Controlador = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorJogo>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            botaoFlipa();
        }
    }
    // Update is called once per frame
    public void botaoFlipa()
    {
        Controlador.flipaModo();
        if (Controlador.modoConjunto)
        {
            imagemModo.sprite = imagensModo[1];
        }
        else
        {
            imagemModo.sprite = imagensModo[0];
        }
    }
}
