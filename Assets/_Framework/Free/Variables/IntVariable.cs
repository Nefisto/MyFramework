using UnityEngine;

namespace Unidream
{
    [CreateAssetMenu(fileName = "IntVariable", menuName = "Framework/Variables/Int")]
    public class IntVariable : BaseVariable<int>
    {
        public void ApplyChange(int value)
            => Value += value;
    }
}