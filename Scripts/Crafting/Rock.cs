using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private float rockHealth;
    [SerializeField] private Animator anim;

    [SerializeField] private GameObject stonePrefab;
    [SerializeField] private int totalStone;

    private bool isMining;


    public void OnMining()
    {
        rockHealth--;

        anim.SetTrigger("isMining");

        if(rockHealth <= 0)
        {
            for(int i = 0; i < totalStone; i++)
            {
                Instantiate(stonePrefab, transform.position + new Vector3(Random.Range(-0.5f,0.5f), Random.Range(-0.5f,0.5f),0f), transform.rotation);
            }

            anim.SetTrigger("break");
            isMining = true;
        }
    }
     private void OnTriggerEnter2D(Collider2D collision)
     {
        if(collision.CompareTag("Pickaxe") && !isMining)
        {
            OnMining();
        }
     }
}
