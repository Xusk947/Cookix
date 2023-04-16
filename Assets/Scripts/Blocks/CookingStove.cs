using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Cooking Stove a child of block which is used for cooking items with special kind of KitchenItem
/// </summary>
public class CookingStove : Table
{
    /// <summary>
    /// Which Item should spawn on this Block when a game is start
    /// </summary>
    public KitchenItem ItemToSpawn;
    /// <summary>
    /// is a GameObject of Cooking Process and Warning Sign Info
    /// </summary>
    private GameObject hud;
    private Animator warningHud;
    /// <summary>
    /// using to display current cook progress
    /// </summary>
    private Image progressBar;
    /// <summary>
    /// Run a particle system when something cook on this block
    /// </summary>
    private ParticleSystem _particleSystem;
    /// <summary>
    /// speed with in block will cook items on it
    /// </summary>
    [SerializeField]
    private float _cookSpeed = .5f;

    private AudioSource _audioSource;
    private bool _playing = false;

    protected new void Start()
    {
        base.Start();
        _particleSystem = transform.GetComponentInChildren<ParticleSystem>();
        // Spawn Kitchen Item on it
        if (ItemToSpawn != null)
        {
            SpawnKitchenItemEntity(ItemToSpawn);
        }
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.loop = true;
        _audioSource.playOnAwake = false;
        // Create a HUD instance
        SpawnHUD();
    }
    void Update()
    {
        // Check item for null and default class and later cast to a Kitchen ItemEntity
        KitchenItemEntity kitchenItemEntity = CheckAndGetKitchenItemEntity(_itemEntity);
        if (kitchenItemEntity == null || !kitchenItemEntity.CanCook())
        {
            warningHud.gameObject.SetActive(false);
            hud.SetActive(false);
            _particleSystem?.Stop();
            StopVFX();
            return;
        };

        _particleSystem?.Play();

        PlayVFX(kitchenItemEntity);
        if (kitchenItemEntity.ItemIsBurning())
        {
            UpdateBurntState(kitchenItemEntity);
        }
        else
        {
            UpdateCookState(kitchenItemEntity);
        }
        
        progressBar.fillAmount = kitchenItemEntity.CookingProgress;
        if (kitchenItemEntity.CookingProgress > 1.0f)
        {
            kitchenItemEntity.CookItemsInside();
            if (!kitchenItemEntity.IsBurnt)
            {
                IconHide iconHide = Instantiate(Content.Instance.IconAccept);
                iconHide.transform.localScale *= 0.8f;
                iconHide.transform.position = transform.position + Vector3.up;
            }
        }
    }

    private void UpdateBurntState(KitchenItemEntity kitchenItemEntity)
    {
        kitchenItemEntity.CookingProgress += Time.deltaTime * _cookSpeed * GameManager.Instance.rules.FoodItemBurningSpeed * GameManager.Instance.rules.CookingStoveCookSpeedMultiplayer;
        if (kitchenItemEntity.CookingProgress > 0.25f)
        {
            warningHud.gameObject.SetActive(true);
        }
        warningHud.SetFloat("BlinkSpeed", 1.25f + kitchenItemEntity.CookingProgress * 5f);
        hud.SetActive(false);
    }

    private void UpdateCookState(KitchenItemEntity kitchenItemEntity)
    {
        kitchenItemEntity.CookingProgress += Time.deltaTime * _cookSpeed * GameManager.Instance.rules.CookingStoveCookSpeedMultiplayer;
        hud.SetActive(true);
        warningHud.gameObject.SetActive(false);
    }
    /// <summary>
    /// Check Item Entity if it exist and is an Kitchen Item Entity and return casted Item Entity
    /// If object isn't KitchenItemEntity return null
    /// </summary>
    /// <param name="item">Item for check and cast to KitchenItemEntity</param>
    /// <returns></returns>
    private KitchenItemEntity CheckAndGetKitchenItemEntity(ItemEntity item)
    {
        if (_itemEntity == null) return null;
        if (_itemEntity is not KitchenItemEntity) return null;
        return _itemEntity as KitchenItemEntity;
    }
    /// <summary>
    /// Create an Item
    /// </summary>
    /// <param name="Instance"></param>
    private void SpawnKitchenItemEntity(KitchenItem Instance)
    {
        Item = Instance.Create();
    }
    /// <summary>
    /// Spawn HUD which is a part of cooking progress bar
    /// </summary>
    private void SpawnHUD()
    {
        hud = Instantiate(Content.Instance.CookProgressBar);
        hud.transform.SetParent(transform);
        hud.transform.localPosition = new Vector3(0, 1f, 0.02f);
        progressBar = hud.transform.GetChild(1).GetComponent<Image>();
        progressBar.fillAmount = 0;
        hud.SetActive(false);

        warningHud = Instantiate(Content.Instance.WarningIcon);
        warningHud.transform.SetParent(transform);
        warningHud.transform.localPosition = new Vector3(0, 1.2f, 0.02f);
        warningHud.gameObject.SetActive(false);
    }

    private void StopVFX()
    {
        _audioSource.Stop();
        _playing = false;
    }

    private void PlayVFX(KitchenItemEntity kitchenItemEntity)
    {
        if (_playing) return;
        AudioClip clip = Content.Instance.VFX_Fry;

        switch(kitchenItemEntity.CookingType)
        {
            case CookingType.Frying:
                clip = Content.Instance.VFX_Fry;
                break;
        }

        _audioSource.clip = clip;
        _audioSource.Play();
        _playing = true;
    }
}
