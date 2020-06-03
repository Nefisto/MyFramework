using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Drop Table", menuName = "Framework/Drop Table/Default")]
public class DropTable : ScriptableObject
{
    private enum DropTableKind
    {
        WithReposition,
        WithoutReposition
    }

    public List<DropTableItem> table = new List<DropTableItem>();

    // public DropTableKind sampleKind;

    private int TotalWeight
    {
        get => table.Sum(item => item.weight);
    }

    public bool isEmpty
    {
        get => table.Count > 0 ? true : false;
    }

    public List<DropItem> Drop(int nDrops = 1)
    {
        List<DropItem> droppedLoot = new List<DropItem>();

        AddUniqueItem(droppedLoot, nDrops);
        AddGuarantedItems(droppedLoot);

        return droppedLoot;
    }

    private void AddUniqueItem(List<DropItem> droppedLoot, int nDrop = 1)
    {
        List<DropTableItem> possibleDrops = new List<DropTableItem>(table);
        possibleDrops.RemoveAll((item) => item.isGuaranted || item.weight == 0 || !item.prefab);

        while (nDrop-- > 0)
        {
            int x = Random.Range(1, TotalWeight + 1);

            foreach (var item in possibleDrops)
            {
                x -= item.weight;

                if (x <= 0)
                {
                    droppedLoot.Add(item);
                    break;
                }
            }
        }
    }

    private void AddGuarantedItems(List<DropItem> droppedLoot)
    {
        foreach (var item in table)
        {
            if (item.isGuaranted)
                droppedLoot.Add(item);
        }
    }

    // TODO: Learn reflection to turn it private
    public void UpdatePercent()
    {
        var totalWeight = 0;

        if (table.Count == 0)
            return;

        foreach (var item in table)
            totalWeight += item.weight;

        foreach (var item in table)
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