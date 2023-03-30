using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public static List<PlayerController> players = new List<PlayerController>();

    // Base variables
    private bool _isSlicing;
    /// <summary>
    /// Use for animation state
    /// </summary>
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

    private GameInput _gameInput;
    private Rigidbody _rigidBody;
    private CharacterController _characterController;
    private Animator _animator;
    // Raycast
    private float _raycastDistance = 4f;
    private Ray _ray;
    private GameObject _eyes;
    private int _collisionLayers = (1 << 8);

    // Children
    public Transform ItemHolder { get; private set; }
    /// <summary>
    /// Prefab of knife, use in Slice animation
    /// </summary>
    private GameObject _knife;
    /// <summary>
    /// Item which player hold in current time
    /// </summary>
    private ItemEntity _currentItem { get; set; } 
    /// <summary>
    /// Block which is hovered by player Eyes
    /// </summary>
    private Block _selectedBlock;
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
            _animator.SetBool("HasAnItem", false);
            if (_currentItem == null) return;
            _animator.SetBool("HasAnItem", true);
            _currentItem.transform.parent = ItemHolder.transform;
            _currentItem.transform.position = ItemHolder.transform.position;
            _currentItem = value;
        } 
    }
    
    /// <summary>
    /// Remove item from player hands if it exist
    /// </summary>
    public void RemoveItem()
    {
        if (_currentItem != null)
        {
            _animator.SetBool("HasAnItem", false);
            Destroy(_currentItem.gameObject);
        }
    }
    private void Start()
    {
        // Get base components
        _rigidBody = GetComponent<Rigidbody>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        // Connect Input
        _gameInput = GameInput.Instance;
        _gameInput.OnInteractAction += PlayerInteract;
        _gameInput.OnSecondInteractAction += PlayerSecondInteract;
        // Create a ray for cheking blocks forward the Player
        _ray = new Ray();
        // Find another Children
        _eyes = transform.Find("eyes").gameObject;
        print(transform.Find("hand-left"));
        print(transform.Find("hand-left").Find("knife"));
        _knife = transform.Find("hand-left").Find("knife").gameObject;
        _knife.SetActive(false);
        ItemHolder = transform.Find("itemHolder");

        players.Add(this);
    }
    /// <summary>
    /// When player interact with block by first Interact Key, and send Event.OnKitchenBlockInteract
    /// </summary>
    /// <param name="sender">not used</param>
    /// <param name="e">is null</param>
    private void PlayerInteract(object sender, EventArgs e)
    {
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
    private void PlayerSecondInteract(object sender, EventArgs e)
    {
        if (_selectedBlock != null)
        {
            Events.OnKitchenBlockSecondInteract(new BlockArgs(this, _selectedBlock, (e as BoolEventArgs).Condition));
        }
    }


    private void Update()
    {
        UpdateInteraction();
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }
    /// <summary>
    /// Create a Ray on his Eyes and when it's collide with block set _selectedBlock to new one
    /// </summary>
    private void UpdateInteraction()
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
                } else
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
        } else if (_selectedBlock != null)
        {
            Events.OnKitchenBlockUnselected(new BlockArgs(this, _selectedBlock));
            _selectedBlock = null;
        }
    }

    private void UpdateMovement()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        Vector3 direction = new Vector3(inputVector.x, 0, inputVector.y);

        Vector3 position = transform.position + direction * Time.deltaTime * 5f;
        transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * 7.5f);
        _characterController.SimpleMove(direction * 5f);
        _animator.SetBool("IsMoving", _characterController.velocity.magnitude > 0.1f);
    }
}
