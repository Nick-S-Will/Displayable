using UnityEngine;

namespace Displayable
{
    /// <summary>
    /// <see cref="MonoBehaviour"/> that displays a(n) <typeparamref name="ObjectType"/>.
    /// </summary>
    public abstract class Display<ObjectType> : MonoBehaviour where ObjectType : class
    {
        protected delegate void ObjectChanging(ObjectType oldObject, ObjectType newObject);

        public ObjectType DisplayObject 
        {
            get => displayObject;
            set
            {
                if (displayObject != value)
                {
                    DisplayObjectChanging?.Invoke(displayObject, value);
                    displayObject = value;
                }

                SetVisible(displayObject != null);
                UpdateVisuals();
            }
        }

        protected event ObjectChanging DisplayObjectChanging;

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