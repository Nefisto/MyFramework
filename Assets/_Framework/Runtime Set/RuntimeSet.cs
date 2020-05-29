using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "RuntimeSet", menuName= "Framework/RuntimeSet")]
public class RuntimeSet : ScriptableObject//, IEnumerable<RuntimeItem>
{
    public List<RuntimeItem> Items = new List<RuntimeItem>();

    public void Add(RuntimeItem thing)
    {
        if (!Items.Contains(thing))
            Items.Add(thing);
    }

    public void Remove(RuntimeItem thing)
    {
        if (Items.Contains(thing))
            Items.Remove(thing);
    }

    // public IEnumerator<RuntimeItem> GetEnumerator()
    // {
    //     foreach (var item in Items)
    //         yield return item;
    // }

    // IEnumerator IEnumerable.GetEnumerator()
    // {
    //     return GetEnumerator();
    // }
}