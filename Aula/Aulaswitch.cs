using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AS CLASSES VAO AGRUPAR VARIAVEIS E METODOS
public class Aulaawitch : MonoBehaviour
{
    public int diaSemana;

    //USAR SWITCH AO INVES DO IF E ELSE (BREAK É O FECHAMENTO DO SWITCH) (COM O DEFAUlT ELE FAZ A LEITURA DOS NUMEROS QUE NÃO ESTAO NO CASE)
    void Start()
    {
        switch(diaSemana)
        {
            case 1:
                Debug.Log("Segunda");
                break;
            case 2:
                Debug.Log("Terça");
                break;
            case 3:
                Debug.Log("Quarta");
                break;
            case 4:
                Debug.Log("Quinta");
                break;
            default:
                Debug.Log("Dia nao encontrado");
                break;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Pressionou");
        }
        
    }

}