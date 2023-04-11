using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlateHolder : Block
{
    [SerializeField]
    private PlateItem _plateToSpawn;
    [SerializeField, Range(1, 4)]
    private int _maxPlates = 4;
    [SerializeField, Range(0, 4)]
    private int _plateCount = 3;
    [SerializeField]
    private bool _infinityPlates = false;
    [SerializeField]
    private float _plateSpawnDelay = 10f;
    private float _plateSpawnTimer;

    protected override void Start()
    {
        base.Start();
        _plateSpawnTimer = _plateSpawnDelay;
    }

    private void Update()
    {
        if (_plateCount == _maxPlates) return;
        _plateSpawnTimer -= Time.deltaTime;
        if (_plateSpawnTimer < 0)
        {
            SpawnPlate();
            _plateSpawnTimer = _plateSpawnDelay;
        }
    }

    private void SpawnPlate()
    {
        _plateCount++;
    }

    public override void Interact(ChefController player)
    {
        if (_plateCount == 0) return;
        if (player.CurrentItem == null)
        {
            player.CurrentItem = _plateToSpawn.Create();
            _plateCount -= 1;
        }
        else if (player.CurrentItem is FoodEntity)
        {
            InteractWithFoodEntity(player);
        }
    }

    private void InteractWithFoodEntity(ChefController player)
    {
        FoodEntity foodEntity = player.CurrentItem as FoodEntity;
        if (!foodEntity.foodItem.canBePlacedOnPlate) return;

        PlateEntity plateEntity = _plateToSpawn.Create();
        if (!plateEntity.TryToAddItemOn(foodEntity))
        {
            Destroy(plateEntity);
        }
        player.CurrentItem = plateEntity;
        _plateCount -= 1;
    }
}
