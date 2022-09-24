using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerObj;

    public float Speed;
    public bool isShading;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        InputFunc();
        OnMouseMove();
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
            || PlayerObj.transform.position.x <= -8f && pos.x < 0)
        {
            pos.x = 0;
        }
        if (PlayerObj.transform.position.y >= 4f && pos.y > 0
            || PlayerObj.transform.position.y <= -4f && pos.y < 0)
        {
            pos.y = 0;
        }
    }
}
