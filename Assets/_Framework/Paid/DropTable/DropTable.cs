// using System;
using System.Collections.Generic;
using UnityEngine;

// TODO: Custom editor para aparecer a quantia somente quando o bool for true
// TODO: Criar IntRange
// TODO: Vincular droptable com pool manager

// TODO: mudar o label do item para "prefab" ou "poolName" no custom editor


public enum DropTableKind
{
    WithReposition,
    WithoutReposition
}

[CreateAssetMenu(fileName = "Drop Table (PooledObject)", menuName = "Framework/Drop Table/Default")]
public class DropTable : ScriptableObject
{
    public List<DropTableItem> loot = new List<DropTableItem>();
    
    public DropTableKind sampleKind;

    public bool isEmpty
    {
        get => loot.Count > 0 ? true : false;
    }

    // public abstract (Pool pool, int amount) DropSingle();

    public void CalculatePercent()
    {
        var totalWeight = 0;

        if (loot.Count == 0)
            return;

        foreach (var item in loot)
            totalWeight += item.weight;

        foreach (var item in loot)
        {
            if (item.weight == 0)
            {
                item.percent = -1f;
                continue;
            }

            item.percent = (float)item.weight / totalWeight;
        }
    }
}