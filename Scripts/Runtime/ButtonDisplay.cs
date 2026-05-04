using UnityEngine;
using UnityEngine.UI;

namespace Displayable
{
    /// <summary>
    /// <see cref="Display{ObjectType}"/> for <see cref="UnityEngine.UI.Button"/>s.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public abstract class ButtonDisplay<ObjectType> : Display<ObjectType> where ObjectType : class
    {
        public Button Button
        {
            get
            {
                if (button == null) button = GetComponent<Button>();
                return button;
            }
        }

        private Button button;
    }
}