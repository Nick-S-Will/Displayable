using System;
using UnityEngine;

namespace Displayable
{
    /// <summary>
    /// <see cref="MonoBehaviour"/> that displays an <typeparamref name="ObjectType"/>.
    /// </summary>
    public abstract class Display<ObjectType> : MonoBehaviour where ObjectType : class
    {
        protected event Action DisplayObjectChanged;

        public ObjectType DisplayObject 
        {
            get => displayObject;
            set
            {
                displayObject = value;
                DisplayObjectChanged?.Invoke();

                SetVisible(displayObject != null);
                UpdateVisuals();
            }
        }

        private ObjectType displayObject;
       
        /// <summary>
        /// Sets visibility to <paramref name="visible"/>.
        /// </summary>
        /// <remarks>Defaults to <see cref="GameObject.SetActive(bool)"/>.</remarks>
        protected virtual void SetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }

        /// <summary>
        /// Updates visuals to reflect <see cref="DisplayObject"/>.
        /// </summary>
        public abstract void UpdateVisuals();
    }
}