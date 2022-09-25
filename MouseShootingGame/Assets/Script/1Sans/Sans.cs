using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sans : MonoBehaviour
{
    [SerializeField] private GameObject wavePatternObj;
    [SerializeField] private GameObject warningObj;
    [SerializeField] private GameObject boneWall;
    [SerializeField] private GameObject gasterBlasterPattern;
    private Player player;
    void Start()
    {
        player = Player.Instance;
        StartCoroutine(PatternProcessing());
    }

    IEnumerator PatternProcessing()
    {
        yield return new WaitForSeconds(1);
        //고중력으로 떨어지는 높이 -4.8f
        warningObj.SetActive(true);
        Vector3 targetPos = new Vector2(player.transform.position.x, -4.8f);
        player.state = PlayerState.CantMove;
        while (player.transform.position.y > -4.8f)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, targetPos, Time.deltaTime * 20);
            yield return null;
        }
        yield return new WaitForSeconds(0.4f);
        player.state = PlayerState.Idle;
        warningObj.SetActive(false);
        boneWall.SetActive(true);
        while (boneWall.transform.localScale.y < 1)
        {
            boneWall.transform.localScale = new Vector3(boneWall.transform.localScale.x, boneWall.transform.localScale.y + Time.deltaTime * 4);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        boneWall.SetActive(false);


        yield return new WaitForSeconds(1);
        while (wavePatternObj.transform.position.x < 40)
        {
            wavePatternObj.transform.Translate(Time.deltaTime * 12, 0, 0);
            yield return null;
        }


        List<Transform> gasterBlasters = new List<Transform>();
        for (int i = 0; i < gasterBlasterPattern.transform.childCount; i++)
        {
            gasterBlasters.Add(gasterBlasterPattern.transform.GetChild(i));
        }
        foreach (Transform gaster in gasterBlasters)
        {
            gaster.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.3f);
        }

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("End");

        Destroy(gameObject);
    }
}
