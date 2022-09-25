using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Issac : MonoBehaviour
{
    public GameObject MotherFoot;
    public DangerZone dangerZone;
    private void Start()
    {
        StartCoroutine(MomFootRush());
    }

    IEnumerator MomFootRush()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            DangerZone zone = Instantiate(dangerZone, Player.Instance.transform.position, Quaternion.identity);
            zone.duration = 1f;
            zone.OnDestroyAction += () => Instantiate(MotherFoot, zone.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 10));
        }
    }
    IEnumerator PatternStart()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 4; i++)
        {
            DangerZone zone = Instantiate(dangerZone, Player.Instance.transform.position, Quaternion.identity);
            zone.duration = 1;
            zone.OnDestroyAction += () => Instantiate(MotherFoot, zone.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }

        //IssacAnimator.Play("IssacStart");
        yield return new WaitForSeconds(0.5f);


    }
}
