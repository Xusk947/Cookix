using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTutorial : LevelController
{
    [SerializeField]
    private Canvas _textCanvas;
    private float _timer = 3f;
    private Step _currentStep = Step.Welcome;
    private TextMeshProUGUI _textWelcome, _textMovement, _textBlockInteraction, _textCookInteraction, _textWarningCook,
        _textItemMerge, _textClientWait, _textPlatePlacing, _textAngryClient, _textGoodLuck;

    // Block Interaction
    [Header("Block Interaction")]
    [SerializeField]
    private List<Block> _firstBlocks = new List<Block>();
    // Cook Interaction
    [Header("Cook Interaction")]
    [SerializeField]
    private List<Block> _secondBlocks = new List<Block>();
    [Header("Cook Warning")]
    [SerializeField]
    private TrashCan _trashCan;
    [SerializeField]
    private FoodItem _cutlet;
    [SerializeField]
    private FoodItem _burger;
    [SerializeField]
    private PlateHolder _plateHolder;
    [SerializeField]
    private FoodReciever _foodReciever;
    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < _textCanvas.transform.childCount; i++)
        {
            GameObject child = _textCanvas.transform.GetChild(i).gameObject;
            child.SetActive(false);
        }

        foreach (Block block in _firstBlocks)
        {
            block.gameObject.SetActive(false);
        }
        foreach (Block block in _secondBlocks)
        {
            block.gameObject.SetActive(false);
        }
        _trashCan.gameObject.SetActive(false);
        _plateHolder.gameObject.SetActive(false);
        _foodReciever.gameObject.SetActive(false);

        InitializeText();

    }

    private void InitializeText()
    {
        _textWelcome = _textCanvas.transform.Find("Welcome").GetComponent<TextMeshProUGUI>();
        _textWelcome.gameObject.SetActive(true);

        _textMovement = GetTextComponent("Movement");
        _textBlockInteraction = GetTextComponent("BlockInteraction");
        _textCookInteraction = GetTextComponent("CookInteraction");
        _textWarningCook = GetTextComponent("CookWarning");
        _textItemMerge = GetTextComponent("ItemMerge");
        _textClientWait = GetTextComponent("ClientWait");
        _textPlatePlacing = GetTextComponent("PlatePlacing");
        _textAngryClient = GetTextComponent("AngryClient");
        _textGoodLuck = GetTextComponent("GoodLuck");
    }
    private TextMeshProUGUI GetTextComponent(string name)
    {
        return _textCanvas.transform.Find(name).GetComponent<TextMeshProUGUI>();
    }

    protected override void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer > 0) return;
        switch(_currentStep)
        {
            case Step.Welcome:
                Welcome();
                break;
            case Step.Movement:
                Movement();
                break;
            case Step.BlockInteraction:
                BlockInteract();
                break;
            case Step.CookInteraction:
                CookInteract();
                break;
            case Step.WarningCook:
                WarningInteract();
                break;
            case Step.FoodMerge:
                FoodMerge();
                break;
            case Step.ClientInteraction:
                ClientInteraction();
                break;
            case Step.PlatePlacing:
                PlatePlacing();
                break;
            case Step.AngryClient:
                AngryClient();
                break;
            case Step.FinishLevel:
                SceneManager.LoadScene(1);
                break;
        }
    }

    private void Welcome()
    {
        // Welcome Text
        _textWelcome.gameObject.SetActive(false);
        _textMovement.gameObject.SetActive(true);
        _timer = 3f;
        _currentStep = Step.Movement;
    }

    private void Movement()
    {
        // Spawn blocks after players learn how to move
        _textMovement.gameObject.SetActive(false);
        _textBlockInteraction.gameObject.SetActive(true);
        foreach (Block block in _firstBlocks)
        {
            block.gameObject.SetActive(true);
        }
        _timer = 5f;
        _currentStep = Step.BlockInteraction;
    }

    private void BlockInteract()
    {
        // Spawn second blocks array and remove items on the old one
        _textBlockInteraction.gameObject.SetActive(false);
        _textCookInteraction.gameObject.SetActive(true);
        DisplayBlock(_firstBlocks, false);
        DisplayBlock(_secondBlocks);
        foreach (PlayerController player in PlayerController.players) 
        {
            player.RemoveItem();
        }
        _timer = 5f;
        _currentStep = Step.CookInteraction;
    }

    private void CookInteract()
    {
        // Show to player how food can burn
        _textCookInteraction.gameObject.SetActive(false);
        _textWarningCook.gameObject.SetActive(true);
        _trashCan.gameObject.SetActive(true);
        foreach (PlayerController player in PlayerController.players)
        {
            player.RemoveItem();
        }

        CookingStove stove = null;
        foreach(Block block in _secondBlocks)
        {
            if (block is CookingStove)
            {
                stove = (CookingStove)block;
                break;
            }
        }
        GameManager.Instance.rules.FoodItemBurningSpeed = 1.0f;

        if (stove.Item == null || stove.Item is not KitchenItemEntity)
        {
            if (stove.Item != null)
            {
                Destroy(stove.Item);
                stove.Item = null;
            }
            stove.Item = stove.ItemToSpawn.Create();
            (stove.Item as KitchenItemEntity).TryToAddItem(_cutlet.Create());

        } else if (stove.Item is KitchenItemEntity)
        {
            KitchenItemEntity kitchenItemEntity = stove.Item as KitchenItemEntity;
            kitchenItemEntity.RemoveItems();
            kitchenItemEntity.TryToAddItem(_cutlet.Create());
        }

        _timer = 4f;
        _currentStep = Step.WarningCook;
    }
    
    private void WarningInteract()
    {
        GameManager.Instance.rules.FoodItemBurningSpeed = 0.1f;
        // Enable all blocks while player merge an Item
        DisplayBlock(_firstBlocks);
        _textWarningCook.gameObject.SetActive(false);
        _textItemMerge.gameObject.SetActive(true);
        _timer = 1f;
        foreach(PlayerController player in PlayerController.players)
        {
            if (player.CurrentItem is FoodEntity)
            {
                FoodEntity food = player.CurrentItem as FoodEntity;
                if (food.addedItems.Count == 1 && food.addedItems[0].Input == _cutlet)
                {
                    _currentStep = Step.FoodMerge;
                    ClientSpawner.InstantlySpawn();
                    _foodReciever.gameObject.SetActive(true);
                }
            }
        }
    }

    private void FoodMerge()
    {
        _textItemMerge.gameObject.SetActive(false);
        _textClientWait.gameObject.SetActive(true);
        _timer = 1f;
        if (_foodReciever.Task != null)
        {
            _currentStep = Step.ClientInteraction;
        }
    }

    private void ClientInteraction()
    {
        _plateHolder.gameObject.SetActive(true);
        _textClientWait.gameObject.SetActive(false);
        _textPlatePlacing.gameObject.SetActive(true);
        if (_foodReciever.isEmpty)
        {
            _currentStep = Step.PlatePlacing;
            GameManager.Instance.rules.ClientWaitTimeMultiplayer = 0.1f;
            ClientSpawner.InstantlySpawn();
        }
        _timer = 5f;
    }

    private void PlatePlacing()
    {
        DisplayBlock(_firstBlocks, false);
        DisplayBlock(_secondBlocks, false);
        _trashCan.gameObject.SetActive(false);
        _plateHolder.gameObject.SetActive(false);
        _textPlatePlacing.gameObject.SetActive(false);
        _textAngryClient.gameObject.SetActive(true);
        _timer = 1f;
        if (_foodReciever.isEmpty)
        {
            _currentStep = Step.AngryClient;
        }
    }

    private void AngryClient()
    {
        _foodReciever.gameObject.SetActive(false);
        _textAngryClient.gameObject.SetActive(false);
        _textGoodLuck.gameObject.SetActive(true);
        _timer = 5f;
        GameManager.Instance.rules.ClientWaitTimeMultiplayer = 1.0f;
        _currentStep = Step.FinishLevel;
    }

    private void DisplayBlock(List<Block> blocks, bool show = true)
    {
        foreach(Block block in blocks)
        {
            block.gameObject.SetActive(show);
        }
    }

    private enum Step
    {
        Welcome, 
        Movement,
        BlockInteraction,
        CookInteraction,
        WarningCook,
        FoodMerge,
        ClientInteraction,
        PlatePlacing,
        AngryClient,
        FinishLevel,
    }
}
