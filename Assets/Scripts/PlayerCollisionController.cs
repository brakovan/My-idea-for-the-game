using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer rbSprite;

    [SerializeField]
    private float badGroundDamage = 5f;
    [SerializeField]
    private float badGroundDamageForce = 5f;

    private float axisY;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rbSprite = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        switch (collision.gameObject.layer)
        {
            case 3:

                break;

            case 6:

                gameObject.GetComponent<PlayerController>().playerHP -= badGroundDamage;
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * badGroundDamageForce, ForceMode2D.Impulse);

                rbSprite.color = Color.red;
                

                break;

            case 7:

                break;

            case 8:

                break;

            case 9:

                break;

            default:

                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 3:

                break;

            case 6:

                break;

            case 7:

                break;

            case 8:

                break;

            case 9:

                break;

            default:

                break;
        }
    }

}
