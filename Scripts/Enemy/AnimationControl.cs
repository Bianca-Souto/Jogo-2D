using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;

    private Animator anim;
    private PlayerAnim player;
    private Skeleton skeleton;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerAnim>();
        skeleton = GetComponentInParent<Skeleton>();

    }

    public void PlayAnim(int value)
    {
        anim.SetInteger("transitionEnemy", value);
    }
    
    public void Attack()
    {
        if(!skeleton.isDead)
        {
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, playerLayer);  //criação de uma esfera invisivel

            if(hit !=null)
            {
                //detecta colisão com o player
                player.OnHit();
            }
        }
    }
    public void OnHit()
    {
        if(skeleton.currentHealth <= 0)
        {
            skeleton.isDead = true;
            anim.SetTrigger("death");

            Destroy(skeleton.gameObject, 3f);
        }
        else
        {
            anim.SetTrigger("hurt");
            skeleton.currentHealth--;
        // no fill amount o valor é entre 0 e 1, então por conta disso é necessario dividir a vida atual pela vida total
            skeleton.healthBar.fillAmount = skeleton.currentHealth / skeleton.totalHealth; 
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
