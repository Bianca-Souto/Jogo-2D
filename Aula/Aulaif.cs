using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AS CLASSES VAO AGRUPAR VARIAVEIS E METODOS
public class Aulaif : MonoBehaviour
{
    public bool isAlive;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Pressionou");
        }
        
    }

}