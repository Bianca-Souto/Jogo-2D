using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;

    private Player player;
    private Animator anim;
    private Casting cast;

    private bool isHitting;
    public float timeCount;
    public float recoveryTime = 1.5f;



    void Start()
    {
        player = GetComponent<Player>();
        player = GetComponentInParent<Player>();
        anim = GetComponent<Animator>();
        cast = FindObjectOfType<Casting>();

    }

    void Update()
    {
        OnMove();
        OnRolling();
        OnRun();
        OnCutting();
        OnDigging();
        OnWatering();
        OnAttacking();
        OnMining();

        if(isHitting)
        {
            timeCount += Time.deltaTime;

            if(timeCount >= recoveryTime)
            {
                isHitting = false;
                timeCount = 0f;
            }
        }
    }

    #region Movement
    void OnMove()
    {
        if (player.GetDirection().x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        if (player.GetDirection().x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    void OnRolling()
    {
        if (player.GetDirection().sqrMagnitude > 0)
        {
            if (player.IsRolling())
            {
                anim.SetTrigger("isRoll");
            }
            else
            {
                anim.SetInteger("transition", 1);
            }
        }
        else
        {
            anim.SetInteger("transition", 0);
        }
    }

    void OnRun()
    {
        if (player.IsRunning())
        {
            anim.SetInteger("transition", 2);
        }
    }

    public void OnCutting()
    {
        if (player.IsCutting())
        {
            anim.SetTrigger("isCutting");
            player.isPaused = true;
        }
    }
    
    void OnDigging()
    {
        if (player.IsDigging())
        {
            anim.SetInteger("transition", 4);
        }
    }
    void OnWatering()
    {
        if (player.IsWatering())
        {
            anim.SetInteger("transition", 5);
        }
    }
    void OnMining()
    {
        if (player.IsMining())
        {
            anim.SetInteger("transition", 6);
        }
    }
    void OnAttacking()
    {
        if (player.IsAttack())
        {
            anim.SetTrigger("isAttack");
        }
    }
    #endregion

    public void OnCastingStarted()
    {
        anim.SetTrigger("isCasting");
        player.isPaused = true;
    }

    public void OnCastingEnded()
    {
        cast.OnCasting();
        player.isPaused = false;
    }

    public void OnHammeringStarted()
    {
        anim.SetBool("hammering", true);
    }
    public void OnHammeringEnded()
    {
        anim.SetBool("hammering", false);
    }
    
    #region Attack
    public void OnHit()
    {
        if(!isHitting)
        {
            anim.SetTrigger("hurt");
            isHitting = true;

            player.currentLife--;
            
            player.lifeBar.fillAmount = player.currentLife / player.lifePlayer;
        }

    }
    public void OnAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, enemyLayer);
                
        if (hit != null)
        {
            AnimationControl animationControl = hit.GetComponentInChildren<AnimationControl>();
            
            if (animationControl != null)
            {
                animationControl.OnHit();
            }
        }            
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
    #endregion
  
}