using UnityEngine;
using UnityEngine.UI;

namespace Unidream.VariableExample
{
    public class HUD_Life : LazyBehavior
    {
        public IntReference currentLife;
        public IntReference maxLife;

        private void Update()
        {
            var percent = (float)currentLife / maxLife;

            image.color = Color.Lerp(Color.red, Color.green, percent);
            image.fillAmount = percent;
        }
    }
}
