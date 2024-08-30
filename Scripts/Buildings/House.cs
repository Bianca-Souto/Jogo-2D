using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class House : MonoBehaviour
{
    [Header("Amounts")]
    [SerializeField] private int woodAmount;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [SerializeField] private float timeAmount;

    [Header("Components")]
    [SerializeField] private GameObject houseColl;
    [SerializeField] private SpriteRenderer houseSprite;
    [SerializeField] private Transform point;

    private bool detectingPlayer;
    private Player player;
    private PlayerAnim playerAnim;
    private PlayerItems playerItens;
    private float timeCount;
    private bool isBegining;

    void Start()
    {
        player = FindObjectOfType<Player>();
        playerAnim = player.GetComponent<PlayerAnim>();
        playerItens = player.GetComponent<PlayerItems>();
    }

    void Update()
    {
        if(detectingPlayer && Input.GetKeyDown(KeyCode.E) && playerItens.totalWood >= woodAmount)
        {
            //casa iniciada
            isBegining = true;
            playerAnim.OnHammeringStarted();
            houseSprite.color = startColor;
            player.transform.position = point.position;
            player.isPaused = true;
            playerItens.totalWood -= woodAmount;
        }

        if(isBegining)
        {
            timeCount += Time.deltaTime;

            if(timeCount >= timeAmount)
            {
                //casa finalizada
                playerAnim.OnHammeringEnded();
                houseSprite.color = endColor;
                player.isPaused = false;
                houseColl.SetActive(true);
            }
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
