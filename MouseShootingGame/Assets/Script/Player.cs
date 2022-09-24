using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerState
{
    Idle,
    CantMove
}

public class Player : MonoBehaviour
{
    public static Player Instance;

    public PlayerState state;
    public float invincible;
    [SerializeField] private float invincibleDelay;

    private void Awake()
    {
        if (Instance == null)
        {
            transform.position = Vector3.zero;
            Instance = this;
        }
    }
    private void Update()
    {
        invincible -= Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Obstruction"))
        {
            OnHit();
        }
    }
    public void OnHit()
    {
        if (invincible <= 0 && gameObject.tag == "Player")
        {
            Debug.Log("ÀÌµûÀÌ");
            invincible = invincibleDelay;
        }
    }
}
