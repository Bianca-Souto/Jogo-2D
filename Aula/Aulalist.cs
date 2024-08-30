using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aulalist : MonoBehaviour
{
    //LIST É PARECIDO COM O ARRAY, POREM, NOS DÁ ESSAS POSSIBILIDADDES DE ADCIONAR, REMOVER, LIMPAR.
    //ESTRUTURA DE UMA LISTA (LIST<TIPO DA LIST> NOME = NEW LIST<TIPO DA LIST>() )
    public List<int> idade = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        idade.Add(1);
        idade.Add(2);
        idade.Add(3);

        idade.Remove(1);

        idade.Clear();
        
        foreach (int item in idade)
        {
            Debug.Log(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
