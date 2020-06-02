using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public DropTable loot;

    private Vector2 pos = new Vector2(-3, 1);

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            var drop = loot.Drop(3);

            var min = drop.Count;
            pos.x = (min * -.5f);

            foreach (var item in drop)
            {
                Instantiate(item.prefab, pos, Quaternion.identity);
                // Debug.Log(item.prefab.name); 

                pos.x += 1;
            }
            pos.y += 1;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            var drop = loot.Drop(1000);

            var min = drop.Count;

            foreach (var item in drop)
                Debug.Log(item.prefab.name);
        }
    }
}
