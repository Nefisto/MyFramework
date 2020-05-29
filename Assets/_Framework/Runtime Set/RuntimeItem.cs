using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeItem : MonoBehaviour
{
    public RuntimeSet runtimeSet;

    private void OnEnable()
        => runtimeSet.Add(this);

    private void OnDisable()
        => runtimeSet.Remove(this);
}
