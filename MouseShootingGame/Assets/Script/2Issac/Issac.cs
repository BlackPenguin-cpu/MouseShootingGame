using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Issac : MonoBehaviour
{
    public GameObject MotherFoot;
    private DangerZone dangerZone;
    private Animator IssacAnimator;
    private void Start()
    {
        dangerZone = Resources.Load<DangerZone>("DangerZone");
        StartCoroutine(PatternStart());
    }
    IEnumerator PatternStart()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 4; i++)
        {
            DangerZone zone = Instantiate(dangerZone, Player.Instance.transform.position, Quaternion.identity);
            zone.duration = 1;
            zone.OnDestroyAction += () => Instantiate(MotherFoot, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }

        IssacAnimator.Play("IssacStart");
        yield return new WaitForSeconds(0.5f);



    }
}
