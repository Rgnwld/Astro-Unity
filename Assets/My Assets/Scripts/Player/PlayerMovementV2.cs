using UnityEngine;

public class PlayerMovementV2 : MonoBehaviour
{
    public static PlayerMovementV2 instance;

    public float moveSpeed = 1f;
    public float defaultDistanceLimmit = 0.5f;
    public bool canInput = true;

    public SpriteRenderer spriteR;


    public LayerMask visibleLayers;

    Vector2 targetPoint;
    Vector2 lastInput = Vector2.zero;

    public delegate void PlayerActions();
    public delegate void PlayerState();
    public delegate void PlayerActionsWithDirections(Vector2 axis);

    public PlayerActions reachEndPointAction;
    public PlayerActionsWithDirections moveAction;
    public PlayerState movingState;
    void Awake() { instance = this; }

    private void Start()
    {
        spriteR = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (canInput)
            MoveToPoint();
        else
            PerformMovement();
    }

    void MoveToPoint()
    {
        Vector2 axis = CheckController();

        if (axis != Vector2.zero)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, axis, Mathf.Infinity, visibleLayers);
            if (hit)
            {
                targetPoint = hit.point;
                lastInput = axis;
                canInput = false;

                if (moveAction != null)
                    moveAction(axis);
            }
        }
    }


    void PerformMovement()
    {
        Vector2 direction = targetPoint - DevTools.Vec3toVec2(transform.position);
        float distance = direction.magnitude;

        if (distance <= defaultDistanceLimmit + moveSpeed * Time.deltaTime)
        {
            canInput = true;
            transform.position = new Vector2(targetPoint.x + defaultDistanceLimmit * -lastInput.x, targetPoint.y + defaultDistanceLimmit * -lastInput.y);
            lastInput = Vector2.zero;
            if (reachEndPointAction != null)
                reachEndPointAction();
        }
        else
        {
            transform.Translate(lastInput * Time.deltaTime * moveSpeed);
            if(movingState != null)
            movingState();
        }
    }

    Vector2 CheckController()
    {
        float xMov, yMov = 0;

        //Priority to X axis;
        xMov = Input.GetAxisRaw("Horizontal");
        if (xMov == 0) yMov = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(xMov, yMov);

        return direction;
    }
}
