using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum PlayerState
{
    Idle,
    CantMove
}

public class Player : MonoBehaviour
{
    public static Player Instance;
    private int Hp = 0;
    public int _Hp
    {
        get { return Hp; }
        set
        {
            if (value <= 0)
            {
                StartCoroutine(DeadAnim());
            }
            Hp = value;
        }
    }
    public Image[] hpImage;
    public PlayerState state;
    public float invincible;

    [SerializeField] private Sprite[] PlayerSprite;
    [SerializeField] private float invincibleDelay;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (Instance == null)
        {
            transform.position = Vector3.zero;
            Instance = this;
        }
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        invincible -= Time.deltaTime;
        if (gameObject == Instance.gameObject)
            for (int i = 0; i < 3; i++)
            {
                hpImage[i].gameObject.SetActive(Hp > i);
            }
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
        if (invincible <= 0 && gameObject == Instance.gameObject)
        {
            invincible = invincibleDelay;
            StartCoroutine(hitCoroutine());
        }
    }
    IEnumerator hitCoroutine()
    {
        SoundManager.Instance.PlaySound("PlayerHit");
        _Hp--;
        spriteRenderer.sprite = PlayerSprite[1];
        if (Hp <= 0) yield break;
        Camera.main.DOShakePosition(invincibleDelay);
        yield return new WaitForSeconds(invincibleDelay);
        spriteRenderer.sprite = PlayerSprite[0];
    }
    IEnumerator DeadAnim()
    {
        SoundManager.Instance.PlaySound("PlayerDead");
        Camera.main.DOKill();
        Time.timeScale = 0.3f;
        GameManager.Instance.GameOver();
        for (int i = 50; i > 5; i--)
        {
            Camera.main.transform.position = transform.position + new Vector3(0, 0, -10);
            Camera.main.orthographicSize = (float)i / 10;
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
