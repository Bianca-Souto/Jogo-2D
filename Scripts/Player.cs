using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
  public bool isPaused;

  [SerializeField] private float speed;
  [SerializeField] private float runSpeed;
  
  private float initialSpeed;
  private float lastRollTime;

  [Header("Life Player")]
  public float lifePlayer;
  public Image lifeBar;
  public float currentLife;

  private Rigidbody2D rig;
  private PlayerItems playerItems;
  private PlayerAnim playerAnim;
  private Vector2 direction;

  private bool isRunning;
  private bool isRolling;
  private bool isCutting;
  private bool isDigging;
  private bool isWatering;
  private bool isMining;
  private bool isAttack;
  [SerializeField] private float rollCooldown = 2f; // Tempo de cooldown em segundos
  [HideInInspector] public int handlingObj; // fica ocultado na Unity, mas é publica para ta acessando
    
  #region Returns
  public Vector2 GetDirection() 
  {
    return direction;
  }
  public bool IsRunning() 
  {
    return isRunning;
  }
  public bool IsRolling() 
  {
    return isRolling;
  }
  public bool IsCutting() 
  {
    return isCutting;
  }
  public bool IsDigging() 
  {
    return isDigging;
  }
  public bool IsWatering() 
  {
    return isWatering;
  }
  public bool IsMining() 
  {
    return isMining;
  }
  public bool IsAttack() 
  {
    return isAttack;
  }
  public float GetRollCooldown()
  {
    return rollCooldown;
  }
  public float GetLastRollTime()
  {
    return lastRollTime;
  }

  #endregion
  private void Start()
  {
    rig = GetComponent<Rigidbody2D>();
    playerItems = GetComponent<PlayerItems>();
    playerAnim = GetComponent<PlayerAnim>();
    initialSpeed = speed;
    currentLife = lifePlayer;
  }

  //USADO PARA CAPTURAR INPUTS
  private void Update()
  {
    if(!isPaused)
    {
      if(Input.GetKeyDown(KeyCode.Alpha1))
      {
        handlingObj = 1;
      }
      if(Input.GetKeyDown(KeyCode.Alpha2))
      {
        handlingObj = 2;
      }
      if(Input.GetKeyDown(KeyCode.Alpha3))
      {
        handlingObj = 3;
      }
      if(Input.GetKeyDown(KeyCode.Alpha4))
      {
        handlingObj = 4;
      }
      if(Input.GetKeyDown(KeyCode.Alpha5))
      {
        handlingObj = 5;
      }
      if(Input.GetKeyDown(KeyCode.Alpha6))
      {
        handlingObj = 6;
      }


    OnInput();
    OnRun();
    OnRoll();
    OnCutting();
    OnDigging();
    OnWatering();
    OnAttack();
    OnMining();

    }
  }

  //USADO PARA COISAS RELACIONADAS A FISICA
  private void FixedUpdate()
  {
    if(!isPaused)
    {
      OnMove();
    }
  }

  #region Movement
  
  void OnInput()
  {
   direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
  }
  void OnMove()
  {
    rig.MovePosition(rig.position + direction.normalized * speed * Time.fixedDeltaTime);
  }
  void OnRun()
  {
    if(Input.GetKeyDown(KeyCode.LeftControl))
    {
      speed = runSpeed;
      isRunning = true;
    }

    if(Input.GetKeyUp(KeyCode.LeftControl))
    {
      speed = initialSpeed;
      isRunning = false;
    }
  }
  void OnRoll()
  {
    if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time - lastRollTime > rollCooldown)
    {
      isRolling = true;
      lastRollTime = Time.time;
    }
    else{
      isRolling = false;
    }
  }
  void OnAttack()
  {
    if(handlingObj == 1)
    {
      if(Input.GetMouseButtonDown(0))
      {
        isAttack = true;
        speed = 0f;
      }
      else
      {
        isAttack = false;
        speed = initialSpeed;
      }
    }
  }
  void OnCutting()
  {
    if(handlingObj == 3)
    {
      if(Input.GetMouseButtonDown(0))
      {
        isCutting = true;
        speed = 0f;
      }
      if(Input.GetMouseButtonUp(0))
      {
        isCutting = false;
        speed = initialSpeed;
      }
    }
  }
    void OnMining()
  {
    if(handlingObj == 2)
    {
      if(Input.GetMouseButtonDown(0))
      {
        isMining = true;
        speed = 0f;
      }
      if(Input.GetMouseButtonUp(0))
      {
        isMining = false;
        speed = initialSpeed;
      }
    }
  }

  void OnDigging()
  {
    if(handlingObj == 4)
    {
      if(Input.GetMouseButtonDown(0))
      {
        isDigging = true;
        speed = 0f;
      }
      if(Input.GetMouseButtonUp(0))
      {
        isDigging = false;
        speed = initialSpeed;
      }
    }
  }
  void OnWatering()
  {
    if(handlingObj == 5)
    {
      if(Input.GetMouseButtonDown(0) && playerItems.currentwater > 0)
      {
        isWatering = true;
        speed = 0f;
      }
      if(Input.GetMouseButtonUp(0) || playerItems.currentwater < 0)
      {
        isWatering = false;
        speed = initialSpeed;
      }
      if(isWatering)
      {
        playerItems.currentwater -= 0.01f;
      }
    }
  }
  #endregion
}