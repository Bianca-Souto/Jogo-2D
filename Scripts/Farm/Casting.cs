using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour
{
    [SerializeField] private bool detectingPlayer;
    [SerializeField] private int percentage; //porcentagem de chance de pescar um peixe
    [SerializeField] private GameObject fishPrefab;

    private PlayerItems player;
    private PlayerAnim playerAnim;
    private Player Player;

    void Start()
    {
        player = FindObjectOfType<PlayerItems>();
        playerAnim = player.GetComponent<PlayerAnim>();
        Player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if(Player.handlingObj == 6)
        {
            if(detectingPlayer && Input.GetMouseButtonDown(0))
            {
                playerAnim.OnCastingStarted();
            }
        }
    }

    public void OnCasting()
    {
        int randomValue = Random.Range(1, 100);

        if(randomValue <= percentage)
        {
            //Conseguiu pescar um peixe
            Instantiate(fishPrefab, player.transform.position + new Vector3(Random.Range(-0.5f, 1f), 1f, Random.Range(-0.5f, 1f)), Quaternion.identity);
            Debug.Log("Pescou");
        }
        else
        {
            //Não conseguiu pescar um peixe
            Debug.Log("Não Pescou");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            detectingPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            detectingPlayer = false;
        }
    }
}
