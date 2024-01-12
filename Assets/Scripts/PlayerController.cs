using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Collider2D[] hitColls;

    [SerializeField]
    private TMP_Text playerHPText;

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private RaycastHit2D[] upHit, rightHit, leftHit, downHit;

    private Vector3 downRayPositionCenter, downRayPositionRight, downRayPositionLeft;
    private RaycastHit2D[] groundHitsCenter, groundHitsRight, groundHitsLeft;
    private RaycastHit2D[][] allRaycastHits = new RaycastHit2D[3][];

    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float jumpForce = 5f;

    [SerializeField]
    private float rayLength = 5f;
    [SerializeField]
    private float downRayLength = 5f;
    [SerializeField]
    private float circleRadius = 5f;

    [SerializeField]
    public float playerHP = 5f;

    private float axisX;
    private float axisY;

    public bool isMoving;
    private bool canJump;


    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        rb.freezeRotation = true;
        isMoving = true;
    }

    private void FixedUpdate()
    {
        PlayerOverlapCircle();
        PlayerRaycastHits();
        Movement();
    }

    private void Update()
    {
        GetInputAxis();
        Jump();


        playerHPText.text = "HP :" + playerHP;

    }

    private void GetInputAxis()
    {
        axisX = Input.GetAxisRaw("Horizontal");
        axisY = Input.GetAxisRaw("Vertical");
    }

    private void PlayerRaycastHits()
    {
        upHit = Physics2D.RaycastAll(new Vector2(coll.bounds.center.x, coll.bounds.max.y + .01f), Vector2.up, rayLength);
        rightHit = Physics2D.RaycastAll(new Vector2(coll.bounds.max.x + .01f, coll.bounds.center.y), Vector2.right, rayLength);
        leftHit = Physics2D.RaycastAll(new Vector2(coll.bounds.min.x - .01f, coll.bounds.center.y), Vector2.left, rayLength);
        downHit = Physics2D.RaycastAll(new Vector2(coll.bounds.center.x, coll.bounds.min.y - .01f), Vector2.left, rayLength);
    }

    private void PlayerOverlapCircle()
    {
        hitColls = Physics2D.OverlapCircleAll(transform.position, circleRadius);
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, circleRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(new Vector2(GetComponent<Collider2D>().bounds.center.x, GetComponent<Collider2D>().bounds.max.y + .01f), Vector2.up * rayLength);
        Gizmos.DrawRay(new Vector2(GetComponent<Collider2D>().bounds.max.x + .01f, GetComponent<Collider2D>().bounds.center.y), Vector2.right * rayLength);
        Gizmos.DrawRay(new Vector2(GetComponent<Collider2D>().bounds.min.x - .01f, GetComponent<Collider2D>().bounds.center.y), Vector2.left * rayLength);
        Gizmos.DrawRay(new Vector2(GetComponent<Collider2D>().bounds.center.x, GetComponent<Collider2D>().bounds.min.y - .01f), Vector2.down * rayLength);


        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector2(GetComponent<Collider2D>().bounds.center.x, GetComponent<Collider2D>().bounds.min.y - .01f), Vector2.down * downRayLength);
        Gizmos.DrawRay(new Vector2(GetComponent<Collider2D>().bounds.max.x, GetComponent<Collider2D>().bounds.min.y - .01f), Vector2.down * downRayLength);
        Gizmos.DrawRay(new Vector2(GetComponent<Collider2D>().bounds.min.x, GetComponent<Collider2D>().bounds.min.y - .01f), Vector2.down * downRayLength);

    }

    private void Jump()
    {
        downRayPositionCenter = new Vector2(coll.bounds.center.x, coll.bounds.min.y - .1f);
        downRayPositionRight = new Vector2(coll.bounds.max.x, coll.bounds.min.y - .1f);
        downRayPositionLeft = new Vector2(coll.bounds.min.x, coll.bounds.min.y - .1f);

        groundHitsCenter = Physics2D.RaycastAll(downRayPositionCenter, Vector2.down, downRayLength);
        groundHitsRight = Physics2D.RaycastAll(downRayPositionRight, Vector2.down, downRayLength);
        groundHitsLeft = Physics2D.RaycastAll(downRayPositionLeft, Vector2.down, downRayLength);

        allRaycastHits[0] = groundHitsCenter;
        allRaycastHits[1] = groundHitsRight;
        allRaycastHits[2] = groundHitsLeft;

        canJump = GroundCheck(allRaycastHits);

        if (isMoving)
        {
            if (axisY > 0 && canJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

    }

    private bool GroundCheck(RaycastHit2D[][] groundHits)
     {
         foreach (RaycastHit2D[] hitList in groundHits)
         {
            foreach (RaycastHit2D hit in hitList)
            {
                if (hit.collider != null)
                {
                    return true;
                }
            }
         }

     return false;

     }

    private void Movement()
    {
        if (isMoving)
        {
            rb.velocity = new Vector2(axisX * moveSpeed, rb.velocity.y);
        }
    }
}
