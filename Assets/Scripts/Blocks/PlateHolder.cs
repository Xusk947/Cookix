using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class PlateHolder : Block
{
    [SerializeField]
    private PlateItem _plateToSpawn;
    private List<GameObject> _plates = new List<GameObject>();
    private int _maxPlates;
    [SerializeField]
    private bool _canSpawnPlates = false;
    [SerializeField]
    private float _plateSpawnDelay = 10f;
    private float _plateSpawnTimer;

    private GameObject _plateToSpawnHolder;

    protected override void Start()
    {
        base.Start();
        _plateSpawnTimer = _plateSpawnDelay;
        if (_canSpawnPlates)
        {
            _plateToSpawnHolder = new GameObject("PlatesToSpawn");
            _plateToSpawnHolder.transform.parent = transform;
        }
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject gameObject = transform.GetChild(i).gameObject;
            if (gameObject.name.StartsWith("plate"))
            {
                _plates.Add(gameObject);
            }
        }
        _maxPlates = _plates.Count;
    }

    private void Update()
    {
        if (!_canSpawnPlates) return;
        if (_plates.Count == _maxPlates) return;
        _plateSpawnTimer -= Time.deltaTime;
        if (_plateSpawnTimer < 0)
        {
            SpawnPlate();
            _plateSpawnTimer = _plateSpawnDelay;
        }
    }

    private void SpawnPlate()
    {
        GameObject plate = _plateToSpawnHolder.transform.GetChild(0).gameObject;
        plate.transform.parent = transform;
        plate.SetActive(true);
        _plates.Add(plate);
    }

    public override void Interact(ChefController player)
    {
        if (_plates.Count <= 0) return;
        if (player.CurrentItem == null)
        {
            TakePlate();
            player.CurrentItem = _plateToSpawn.Create();
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

        TakePlate();
    }

    private void TakePlate()
    {
        ArrayUtils.Shuffle(_plates);
        GameObject plate = _plates.First();
        plate.SetActive(false);
        _plates.Remove(plate);
        if (_canSpawnPlates)
        {
            plate.transform.parent = _plateToSpawnHolder.transform;
        } else
        {
            Destroy(plate);
        }
    }
}
