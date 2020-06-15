using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PooledObject : MonoBehaviour
{
    public Pool originPool;

    public Action callbacksBeforeDespaw;

    public void Despawn()
    {
        if (callbacksBeforeDespaw != null)
            callbacksBeforeDespaw.Invoke();
            
        originPool.Despawn(gameObject);
    }
}
