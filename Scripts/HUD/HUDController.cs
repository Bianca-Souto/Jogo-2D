using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("Items")]
    [SerializeField] private Image waterUIBar;
    [SerializeField] private Image woodUIBar;
    [SerializeField] private Image carrotUIBar;
    [SerializeField] private Image fishUIBar;
    [SerializeField] private Image stoneUIBar;

    [Header("Tools")]
    // [SerializeField] private Image axeUI;
    // [SerializeField] private Image shovelUI;
    // [SerializeField] private Image bucketUI;
    public List<Image> toolsUI = new List<Image>();
    [SerializeField] private Color selectColor;
    [SerializeField] private Color alphaColor;

    private PlayerItems playerItems;
    private Player player;

    private void Awake()
    {
        playerItems = FindObjectOfType<PlayerItems>();
        player = playerItems.GetComponent<Player>();
    }

    void Start()
    {
        waterUIBar.fillAmount = 0f;
        woodUIBar.fillAmount = 0f;
        carrotUIBar.fillAmount = 0f;
        fishUIBar.fillAmount = 0f;
        stoneUIBar.fillAmount = 0f;
    }

    void Update()
    {
        waterUIBar.fillAmount = playerItems.currentwater / playerItems.waterLimit;  //Dividindo o tanto de água que eu tenho pelo tanto que eu posso ter
        carrotUIBar.fillAmount = playerItems.carrots / playerItems.carrotsLimit;
        woodUIBar.fillAmount = playerItems.totalWood / playerItems.woodLimit;
        fishUIBar.fillAmount = playerItems.fishes / playerItems.fishLimit;
        stoneUIBar.fillAmount = playerItems.totalStone / playerItems.stoneLimit;

        //toolsUI[player.handlingObj].color = selectColor;

        for (int i = 1; i < toolsUI.Count; i++)
        {
            if(i == player.handlingObj)
            {
                toolsUI[i].color = selectColor;
            }
            else{
                toolsUI[i].color = alphaColor;
            }
        }
    }
}
