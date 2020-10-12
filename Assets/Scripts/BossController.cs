using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossController : MonoBehaviour
{

    [SerializeField] private GameObject partLeft;
    [SerializeField] private GameObject partRight;

    [SerializeField] private GameObject victoryPanel;

    BossPartsController leftController;
    BossPartsController rightController;

    private Rigidbody2D bossBody;

    private bool hasFinishedEntrance;

    private Sequence s;

    private void Awake ()
    {
        bossBody = GetComponent<Rigidbody2D>();

        leftController = partLeft.GetComponent<BossPartsController>();
        rightController = partRight.GetComponent<BossPartsController>();
    }

    IEnumerator Start ()
    {
        yield return new WaitForSeconds(1f);
        bossBody.DOMoveY(8.4f, 5f).SetRelative();
        /* s = DOTween.Sequence();
        s.Append(bossBody.DOMoveX(4f, 5f).SetRelative());
        s.Append(bossBody.DOMoveX(-8f, 10f).SetRelative());
        s.Append(bossBody.DOMoveX(4f, 5f).SetRelative());
        s.SetLoops(-1, LoopType.Restart); */
        
    }

    private void Update ()
    {
        if (transform.position.y >= 1.28f && !hasFinishedEntrance)
        {
            Debug.Log("Reached");
            StartCoroutine(SetTween());
        }

        if (partLeft == null && partRight == null)
        {
            Die();
        }
    }

    private void Die ()
    {
        victoryPanel.SetActive(true);
        GameObject.Find("Player").GetComponent<InputController>().enabled = false;
        Destroy(this.gameObject, 0.2f);
    }

    IEnumerator SetTween ()
    {
        hasFinishedEntrance = true;
        yield return new WaitForSeconds(2f);
        leftController.Activate();
        rightController.Activate();
        /* s.Append(bossBody.DOMoveX(4f, 3f).SetRelative());
        s.Append(bossBody.DOMoveX(-4f, 3f).SetRelative());
        s.Append(bossBody.DOMoveX(-4f, 3f).SetRelative());
        s.Append(bossBody.DOMoveX(4f, 3f).SetRelative());
        s.SetLoops(-1, LoopType.Yoyo); */
        Debug.Log("Done");
    }

}
