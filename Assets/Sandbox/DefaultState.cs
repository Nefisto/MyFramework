using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : MonoBehaviour
{
    public bool defaultState = true;

    [ContextMenu("Config state")]
    public void ConfigState()
    {
        var childrenState = GetComponentsInChildren<DefaultState>(true);

        foreach (var state in childrenState)
            state.defaultState = state.gameObject.activeInHierarchy;
    }

    [ContextMenu("Reset state")]
    public void ResetState()
    {
        var childrenState = GetComponentsInChildren<DefaultState>(true);

        foreach (var state in childrenState)
            state.gameObject.SetActive(state.defaultState);
    }
}
