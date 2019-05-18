using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuBackgroundController : MonoBehaviour
{
    public GameObject pecaMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   void FixedUpdate()
    {
        
        float spawnChance = Random.Range(0, 30);
        print(spawnChance);
        if (spawnChance > 25){
            float spawnPos = Random.Range(-9.24f, 9.24f);
            GameObject.Instantiate(pecaMenu, new Vector3(spawnPos, 5.6f, 0), Quaternion.identity);
        }
    }
    
}
