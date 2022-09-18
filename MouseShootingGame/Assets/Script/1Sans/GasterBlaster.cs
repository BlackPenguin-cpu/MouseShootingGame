using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasterBlaster : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ReadyToShot());
    }
    IEnumerator ReadyToShot()
    {
        yield return new WaitForSeconds(1);



    }
}
