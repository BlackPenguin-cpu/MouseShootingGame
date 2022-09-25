using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GayPinBalls : MonoBehaviour
{

    public GameObject gayBall;
    private int gayNum;
    [SerializeField] private Sprite[] gaySprites;

    [SerializeField] private float curCooldown;
    private readonly float cooldown = 20;

    private void Update()
    {
        curCooldown += Time.deltaTime;
        if (curCooldown > cooldown)
        {
            ThrowGayBall();
            curCooldown = 0;
        }
    }

    private void ThrowGayBall()
    {
        if (gayNum > 5) return;

        SoundManager.Instance.PlaySound("gay" + (gayNum + 1), SoundType.SE, 2f);
        GameObject obj = Instantiate(gayBall, transform.position, Quaternion.identity);
        obj.transform.parent = gameObject.transform;
        obj.GetComponent<SpriteRenderer>().sprite = gaySprites[gayNum];
        obj.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * Random.Range(15, 35), ForceMode2D.Impulse);
        gayNum++;
    }
}
