using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    // Base variables
    [SerializeField] private GameInput gameInput;
    private Rigidbody rb;

    // Raycast
    [Space(2)]
    [SerializeField]
    private float raycastDistance = 4f;
    private Ray ray;
    private GameObject eyes;
    private int collisionLayers = (1 << 8);

    // Item
    public Transform itemHolder;
    private ItemEntity currentItem { get; set; } 
    // Block Interaction
    private Block selectedBlock;

    public ItemEntity CurrentItem
    { 
        get 
        { 
            return currentItem; 
        } 
        set 
        {
            currentItem = value;
            if (currentItem == null) return;
            currentItem.transform.parent = itemHolder.transform;
            currentItem.transform.position = itemHolder.transform.position;
            currentItem = value;
        } 
    }
    public void RemoveItem()
    {
        if (currentItem != null)
        {
            Destroy(currentItem.gameObject);
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameInput.OnInteractAction += PlayerInteract;
        ray = new Ray();
        eyes = transform.Find("Eyes").gameObject;
        itemHolder = transform.Find("ItemHolder");
    }

    private void PlayerInteract(object sender, System.EventArgs e)
    {
        if (selectedBlock != null)
        {
            Events.OnKitchenBlockInteract(new BlockArgs(this, selectedBlock));
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
        ray = new Ray(eyes.transform.position, Vector3.down);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, raycastDistance, collisionLayers))
        {
            Block rayBlock;
            if (raycastHit.transform.parent != null)
            {
                rayBlock = raycastHit.transform.parent.gameObject.GetComponent<Block>();
            }
            else
            {
                rayBlock = raycastHit.transform.gameObject.GetComponent<Block>();
            }
            // If current block is not the same, remove focus from this block and set focus to another
            if (selectedBlock == null)
            {
                selectedBlock = rayBlock;
                Events.OnKitchenBlockSelected(new BlockArgs(this, rayBlock));
            }
            else if (rayBlock != selectedBlock) 
            {
                Events.OnKitchenBlockUnselected(new BlockArgs(this, selectedBlock));
                Events.OnKitchenBlockSelected(new BlockArgs(this, rayBlock));
                selectedBlock = rayBlock;
            }
        } else if (selectedBlock != null)
        {
            Events.OnKitchenBlockUnselected(new BlockArgs(this, selectedBlock));
            selectedBlock = null;
        }
    }

    private void UpdateMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 direction = new Vector3(inputVector.x, 0, inputVector.y);

        Vector3 position = transform.position + direction * Time.deltaTime * 5f;
        rb.MovePosition(position);
        transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * 7.5f);
    }

    public void OnDrawGizmos()
    {
        GameObject gm = transform.GetChild(1).gameObject;
        Gizmos.DrawSphere(gm.transform.position, 0.25f);
        Gizmos.color = Color.red;
    }
}
