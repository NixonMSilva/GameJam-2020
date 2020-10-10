using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float detectionRange = 5f;

    [SerializeField] private LayerMask opaqueObjects;

    [SerializeField] private GameObject moveLimitLeft;
    [SerializeField] private GameObject moveLimitRight;
    private float limitLeft;
    private float limitRight;

    private Rigidbody2D enemyBody;

    private GameObject player;

    private Vector2 playerDirection;

    private float distanceToPlayer;
    private float distanceToOpaqueObject;

    private void Awake ()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        limitLeft = moveLimitLeft.transform.position.x;
        limitRight = moveLimitRight.transform.position.x;
    }

    private void Update ()
    {
        // Melee enemy attack routine
        if (CanSeePlayer())
        {
           
        }
    }

    IEnumerator Start ()
    {
        yield return new WaitForSeconds(1f);
        Sequence s = DOTween.Sequence();
        s.Append(enemyBody.DOMoveX(4f, 3f).SetRelative());
        s.Append(enemyBody.transform.DOScaleX(-1f, 0f));
        s.Append(enemyBody.DOMoveX(-4f, 3f).SetRelative());
        s.Append(enemyBody.transform.DOScaleX(1f, 0f));
        s.SetLoops(-1, LoopType.Restart);
    }

    protected bool CanSeePlayer ()
    {
        distanceToPlayer = Vector2.Distance(this.gameObject.transform.position, player.transform.position);
        if (distanceToPlayer <= detectionRange)
        {
            RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, player.transform.position - this.gameObject.transform.position, Mathf.Infinity, opaqueObjects);
            Debug.DrawRay(this.gameObject.transform.position, player.transform.position - this.gameObject.transform.position);
            distanceToOpaqueObject = Vector2.Distance(this.gameObject.transform.position, hit.collider.transform.position);    
            if (hit.collider.CompareTag("Block"))
            {
                // Debug.Log("Vision obstructed!");
                return false;
            }
            else
            {
                // Debug.Log("Vision not obstructed!");
                return true;
            }
        }
        return false;
    }

    private void OnDestroy ()
    {
        enemyBody.DOKill();
    }

    /*
    protected void TweenMove ()
    {
        Debug.Log("Tween");
        Vector2 movement = new Vector2(limitRight, 0f);
        enemyBody.MovePosition(enemyBody.position + movement * Time.deltaTime * 2f);
    } */
}
