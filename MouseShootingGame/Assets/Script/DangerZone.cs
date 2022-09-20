using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    public float duration;
    public System.Action OnDestroyAction;
    private void Update()
    {
        if (duration <= 0)
        {
            OnDestroyAction.Invoke();
            Destroy(gameObject);
        }
        duration -= Time.deltaTime;
    }
}
