using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherFoot : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        StartCoroutine(Stomp());
    }
    IEnumerator Stomp()
    {
        transform.Translate(0, 10, 0);

        Vector3 targetVec = transform.position - new Vector3(0, 10, 0);
        while (transform.position != targetVec)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetVec, Time.deltaTime * 30);
            yield return null;
        }

        foreach (RaycastHit2D raycastHit2D in Physics2D.BoxCastAll(transform.position, boxCollider2D.size, 0, Vector3.forward))
        {
            if (raycastHit2D.transform.gameObject == Player.Instance.gameObject)
            {
                Player.Instance.OnHit();
            }
        }

        yield return new WaitForSeconds(0.3f);

        targetVec = transform.position + new Vector3(0, 10, 0);
        while (transform.position != targetVec)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetVec, Time.deltaTime * 30);
            yield return null;
        }

        Destroy(gameObject);
    }
}
