using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour
{
    [Header("Stats")]
    public float totalHealth;
    public float currentHealth;
    public Image healthBar;
    public bool isDead;

    [Header("Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AnimationControl animControl;

    [SerializeField]
    private float vision;
    private Transform target;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;
        player = FindObjectOfType<Player>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            SearchPlayer();
            if(target != null)
            {
                agent.SetDestination(player.transform.position);

                if(Vector2.Distance(transform.position, player.transform.position) <= agent.stoppingDistance)
                {
                    //limite da distancia
                    animControl.PlayAnim(2);
                }
                else
                {
                    //seguindo o player
                    animControl.PlayAnim(1);
                }

                float posX = player.transform.position.x - transform.position.x;

                if(posX > 0)
                {
                    //direita
                    transform.eulerAngles = new Vector2(0,0);
                }
                else
                {
                    //esquerda
                    transform.eulerAngles = new Vector2(0,180);
                }
            }
            else
            {
                animControl.PlayAnim(0);
                agent.velocity = Vector3.zero;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(this.transform.position, this.vision);
    }
    private void SearchPlayer()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, vision);

        foreach (Collider2D collider in colliders)
        {
            // Verifica se o collider pertence à camada do jogador
            if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                target = collider.transform;
                return; // Sai do loop assim que encontrar o jogador
            }
        }
        target = null;
    }
}
