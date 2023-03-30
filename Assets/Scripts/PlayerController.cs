using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public static List<PlayerController> players = new List<PlayerController>();

    // Base variables
    private GameInput _gameInput;
    private Rigidbody _rigidBody;
    private CharacterController _characterController;
    private Animator _animator;
    // Raycast
    [Space(2)]
    [SerializeField]
    private float _raycastDistance = 4f;
    private Ray _ray;
    private GameObject _eyes;
    private int _collisionLayers = (1 << 8);

    // Item
    public Transform ItemHolder;
    private ItemEntity _currentItem { get; set; } 
    // Block Interaction
    private Block _selectedBlock;

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
        _eyes = transform.Find("Eyes").gameObject;
        ItemHolder = transform.Find("ItemHolder");

        players.Add(this);
    }

    private void PlayerInteract(object sender, System.EventArgs e)
    {
        if (_selectedBlock != null)
        {
            Events.OnKitchenBlockInteract(new BlockArgs(this, _selectedBlock));
        }
    }
    // Event Args sended by BoolEventArgs which transfer a press button condition
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
    }

    public void OnDrawGizmos()
    {
        GameObject gm = transform.GetChild(1).gameObject;
        Gizmos.DrawSphere(gm.transform.position, 0.25f);
        Gizmos.color = Color.red;
    }
}
