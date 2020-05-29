using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t3 : MonoBehaviour
{
    public IntRange intRange;

    [Space(10)]
    
    [MinMaxRange(0, 10)]
    public IntRange rangeAttribute;
}
