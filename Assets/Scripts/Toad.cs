using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toad : MonoBehaviour
{

    public int player;

    [SerializeField]
    private Transform[] jumpPathPoints;
    [SerializeField]
    private float jumpSpeed;
    private bool grounded, reachedTargetPoint;
    [SerializeField]
    private Sprite[] toadSprites;
    private int positionIndex;
    private Vector2 jumpDirection;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        grounded = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            Debug.Log("Jump!");
            jumpDirection = positionIndex == 0 ? Vector2.right : Vector2.left;
            spriteRenderer.sprite = toadSprites[1];
            grounded = false;
        } else if (Input.GetButtonDown("Jump"))
        {
            StartCoroutine(Attack());
        }
        Move();
    }

    IEnumerator Attack()
    {
        Debug.Log("Attack Start");
        yield return new WaitForSeconds(0.7f);
        Debug.Log("Attack End");
    }

    private void Move()
    {
        if (!grounded)
        {
            reachedTargetPoint = Vector2.Distance(transform.position, jumpPathPoints[positionIndex].position) <= .1f;
            if (jumpDirection == Vector2.right)
            {
                if (reachedTargetPoint)
                {
                    positionIndex++;
                }
                if (ReachedLandingPad())
                {
                    positionIndex = 2;
                }

            }else
            {
                if (reachedTargetPoint)
                {
                    positionIndex--;
                }
                if (ReachedLandingPad())
                {
                    positionIndex = 0;
                }
            }
        }

        transform.position = Vector2.MoveTowards(transform.position,jumpPathPoints[positionIndex].position,jumpSpeed);
    }

    bool ReachedLandingPad()
    {
        if (jumpDirection == Vector2.right && positionIndex == jumpPathPoints.Length || jumpDirection ==  Vector2.left && positionIndex == -1)
        {
            grounded = true;

            transform.Rotate(Vector3.up, 180f);
            spriteRenderer.sprite = toadSprites[0];
            return true;
        }
        return false;
    }
}
