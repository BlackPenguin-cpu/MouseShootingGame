using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject PlayerObj;
    public GameObject sans;
    public GameObject isaac;
    public GameObject imGay;
    public GameObject gameStartText;
    public GameObject OptionUI;

    public Slider MoveSpeedGauge;
    public bool isStart;
    public float Speed
    {
        get { return MoveSpeedGauge.value * 2 + 0.05f; }
    }
    public bool isShading;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        MoveSpeedGauge.value = PlayerPrefs.GetFloat("moveSpeed", MoveSpeedGauge.value);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isStart && !OptionUI.activeSelf)
        {
            GameStart();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !isStart)
        {
            if (OptionUI.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
                OptionUI.SetActive(false);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
                Time.timeScale = 0;
                OptionUI.SetActive(true);
            }
        }
        if (isStart == false) return;

        InputFunc();
        OnMouseMove();
    }
    void GameStart()
    {
        Player.Instance._Hp = 3;
        SoundManager.Instance.PlaySound("BGM", SoundType.BGM, 0.7f);
        isStart = true;
        gameStartText.SetActive(false);
        StartCoroutine(GameProcessing());
    }
    void InputFunc()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnShading();
            isShading = true;
            Player.Instance.GetComponent<TrailRenderer>().enabled = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isShading = false;
            Player.Instance.transform.position = PlayerObj.transform.position;
            Destroy(PlayerObj);
            PlayerObj = Player.Instance.gameObject;
            Player.Instance.GetComponent<TrailRenderer>().enabled = true;
        }
    }
    void OnShading()
    {
        PlayerObj = Instantiate(PlayerObj, PlayerObj.transform.position, Quaternion.identity);
        PlayerObj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
        PlayerObj.GetComponent<Player>().invincible = 50000;
    }
    void OnMouseMove()
    {
        if (Player.Instance.state == PlayerState.CantMove) return;
        Vector3 vec = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0) * Speed;
        PlayerMoveLimit(ref vec);

        PlayerObj.transform.Translate(vec);
    }
    void PlayerMoveLimit(ref Vector3 pos)
    {
        if (PlayerObj.transform.position.x >= 8f && pos.x > 0
            || PlayerObj.transform.position.x <= -8.5f && pos.x < 0)
        {
            pos.x = 0;
        }
        if (PlayerObj.transform.position.y >= 4.5f && pos.y > 0
            || PlayerObj.transform.position.y <= -4.5f && pos.y < 0)
        {
            pos.y = 0;
        }
    }
    IEnumerator GameProcessing()
    {
        yield return null;

        imGay.SetActive(true);
        yield return new WaitForSeconds(60);
        SoundManager.Instance.PlaySound("mom hahaha");
        isaac.SetActive(true);
        yield return new WaitForSeconds(60);
        imGay.SetActive(false);

        sans.SetActive(true);
    }
    public void GameOver()
    {
        PlayerPrefs.SetFloat("moveSpeed", MoveSpeedGauge.value);
    }
}
