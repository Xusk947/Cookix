using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public abstract class ChefController : Controller
{

    /// <summary>
    /// Use for animation state
    /// </summary>
    protected bool _isSlicing;

    // Raycast
    private float _raycastDistance = 4f;
    private Ray _ray;
    private GameObject _eyes;
    private int _collisionLayers = (1 << 8);

    // Items on Hands
    public Transform ItemHolder { get; private set; }
    /// <summary>
    /// Prefab of knife, use in Slice animation
    /// </summary>
    protected GameObject _knife;
    /// <summary>
    /// Item which player hold in current time
    /// </summary>
    [SerializeField, ReadOnly(true)]
    protected ItemEntity _currentItem { get; set; }
    /// <summary>
    /// When player take an Item play sound
    /// </summary>
    protected AudioSource _audioSource;
    /// <summary>
    /// Block which is hovered by player Eyes
    /// </summary>
    protected Block _selectedBlock;
    /// <summary>
    /// Return player item on they hand
    /// When set a new Item, remove old one and change animation state
    /// </summary>
    public ItemEntity CurrentItem
    {
        get
        {
            return _currentItem;
        }
        set
        {
            _currentItem = value;
            bool hasItem = _currentItem != null;
            _animator.SetBool("HasAnItem", hasItem);
            _audioSource.clip = Content.Instance.VFX_PlayerSelect[UnityEngine.Random.Range(0, Content.Instance.VFX_PlayerSelect.Count)];
            _audioSource.Play();
            if (hasItem)
            {
                _currentItem.transform.parent = ItemHolder.transform;
                _currentItem.transform.position = ItemHolder.transform.position;
            }
        }
    }

    /// <summary>
    /// Remove item from player hands if it exist
    /// </summary>
    public void RemoveItem()
    {
        if (_currentItem != null)
        {
            Destroy(_currentItem.gameObject);
            CurrentItem = null;
        }
    }

    public bool IsSlicing
    {
        get { return _isSlicing; }
        set
        {
            _isSlicing = value;
            _knife.SetActive(value);
            _animator.SetBool("IsSlicing", value);
        }
    }
    protected override void Start()
    {
        base.Start();
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.outputAudioMixerGroup = Content.Instance.AudioMixerFX.outputAudioMixerGroup;
        _audioSource.playOnAwake = false;
        // Create a ray for cheking blocks forward the Player
        _ray = new Ray();
        // Find another Children
        _eyes = transform.Find("eyes").gameObject;
        _knife = transform.Find("hand-left").Find("knife").gameObject;
        _knife.SetActive(false);
        ItemHolder = transform.Find("itemHolder");
    }


    private void Update()
    {
        UpdateInteraction();
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }
    protected abstract void UpdateMovement();
    /// <summary>
    /// Create a Ray on his Eyes and when it's collide with block set _selectedBlock to new one
    /// </summary>
    protected virtual void UpdateInteraction()
    {
        _ray = new Ray(_eyes.transform.position, Vector3.down);
        RaycastHit raycastHit;
        if (Physics.Raycast(_ray, out raycastHit, _raycastDistance, _collisionLayers))
        {
            Block rayBlock;
            if (raycastHit.transform.parent != null)
            {
                if (raycastHit.transform.parent.name == "Blocks")
                {
                    rayBlock = raycastHit.transform.gameObject.GetComponent<Block>();
                }
                else
                {
                    rayBlock = raycastHit.transform.parent.gameObject.GetComponent<Block>();
                }
            }
            else
            {
                rayBlock = raycastHit.transform.gameObject.GetComponent<Block>();
            }
            // If current block is not the same, remove focus from this block and set focus to another
            if (_selectedBlock == null)
            {
                _selectedBlock = rayBlock;
                Events.OnKitchenBlockSelected(new BlockArgs(this, rayBlock));
            }
            else if (rayBlock != _selectedBlock)
            {
                Events.OnKitchenBlockUnselected(new BlockArgs(this, _selectedBlock));
                Events.OnKitchenBlockSelected(new BlockArgs(this, rayBlock));
                _selectedBlock = rayBlock;
            }
        }
        else if (_selectedBlock != null)
        {
            Events.OnKitchenBlockUnselected(new BlockArgs(this, _selectedBlock));
            _selectedBlock = null;
        }
    }

    /// <summary>
    /// When player interact with block by first Interact Key, and send Event.OnKitchenBlockInteract
    /// </summary>
    /// <param name="sender">not used</param>
    /// <param name="e">is null</param>
    protected void ChefInteract(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsPaused) return;
        if (_selectedBlock != null)
        {
            Events.OnKitchenBlockInteract(new BlockArgs(this, _selectedBlock));
        }
    }
    /// <summary>
    /// When player interact with block by second Interact Key, send Event.OnKitchenBlockSecondInteract
    /// Event Args sended by BoolEventArgs which transfer a press button condition
    /// </summary>
    /// <param name="sender">not used</param>
    /// <param name="e">also send a holding condition when player press and release button</param>
    protected virtual void ChefSecondInteract(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsPaused) return;
        if (_selectedBlock != null)
        {
            Events.OnKitchenBlockSecondInteract(new BlockArgs(this, _selectedBlock, (e as BoolEventArgs).Condition));
        }
    }

    public void PlaySliceSound()
    {
        _audioSource.clip = Content.Instance.VFX_Slice[UnityEngine.Random.Range(0, Content.Instance.VFX_Slice.Count)];
        _audioSource.Play();
    }
}
