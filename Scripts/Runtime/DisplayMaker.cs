using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Displayable
{
    /// <summary>
    /// <see cref="MonoBehaviour"/> that makes and manages <typeparamref name="DisplayType"/>s.
    /// </summary>
    public abstract class DisplayMaker<ObjectType, DisplayType> : MonoBehaviour where ObjectType : class where DisplayType : Display<ObjectType>
    {
        private static Action<UnityEngine.Object> ContextDestroy => Application.isPlaying ? Destroy : DestroyImmediate;

        public IEnumerable<DisplayType> Displays => displayInstances.Where(display => display.DisplayObject != null);

        /// <summary>
        /// <see cref="Comparison{T}"/> used to sort <typeparamref name="DisplayType"/>s.
        /// </summary>
        protected virtual Comparison<DisplayType> DisplayComparison { get => null; }

        [SerializeField] private Transform displayParent;
        [SerializeField] private DisplayType displayPrefab;

        private readonly List<DisplayType> displayInstances = new();

        protected virtual void Awake()
        {
            Assert.IsNotNull(displayParent);
            Assert.IsNotNull(displayPrefab);
        }

        protected virtual void OnDestroy()
        {
            DestroyDisplays(display => display != null);
        }

        /// <summary>
        /// Gets or makes <typeparamref name="DisplayType"/>s for all <paramref name="displayObjects"/>.
        /// </summary>
        /// <remarks>Overrides existing display objects.</remarks>
        public void SetObjects(IEnumerable<ObjectType> displayObjects)
        {
            if (displayObjects == null) throw new ArgumentNullException(nameof(displayObjects));

            foreach (DisplayType display in displayInstances) display.DisplayObject = null;

            AddObjects(displayObjects);
        }

        #region Add
        /// <summary>
        /// Makes <typeparamref name="DisplayType"/> for <paramref name="displayObject"/>.
        /// </summary>
        /// <returns><typeparamref name="DisplayType"/> displaying <paramref name="displayObject"/>.</returns>
        public DisplayType AddObject(ObjectType displayObject)
        {
            if (displayObject == null) throw new ArgumentNullException(nameof(displayObject));

            DisplayType display = displayInstances.FirstOrDefault(display => display.DisplayObject == null) ?? MakeDisplay();
            display.DisplayObject = displayObject;

            UpdateDisplays();

            return display;
        }

        /// <summary>
        /// Makes <typeparamref name="DisplayType"/>s for <paramref name="displayObjects"/>.
        /// </summary>
        /// <returns><typeparamref name="DisplayType"/>s displaying <paramref name="displayObjects"/>.</returns>
        public IEnumerable<DisplayType> AddObjects(IEnumerable<ObjectType> displayObjects)
        {
            if (displayObjects == null) throw new ArgumentNullException(nameof(displayObjects));

            if (!displayObjects.Any()) return Enumerable.Empty<DisplayType>();

            Queue<DisplayType> availableDisplays = new(displayInstances.Where(display => display.DisplayObject == null));
            List<DisplayType> displays = new();
            foreach (ObjectType displayObject in displayObjects)
            {
                DisplayType display = availableDisplays.Any() ? availableDisplays.Dequeue() : MakeDisplay();
                display.DisplayObject = displayObject;
                displays.Add(display);
            }

            UpdateDisplays();

            return displays;
        }

        private DisplayType MakeDisplay()
        {
            DisplayType display = Instantiate(displayPrefab, displayParent);
            displayInstances.Add(display);

            return display;
        }
        #endregion

        #region Remove
        /// <summary>
        /// Removes reference(s) to <paramref name="displayObject"/> up to <paramref name="max"/> times.
        /// </summary>
        /// <returns>Number of references removed.</returns>
        public int RemoveObject(ObjectType displayObject, int max = int.MaxValue)
        {
            if (displayObject == null) throw new ArgumentNullException(nameof(displayObject));

            if (max <= 0) return 0;

            IEnumerable<DisplayType> displays = displayInstances.Where(display => display.DisplayObject == displayObject).Take(max);
            foreach (DisplayType display in displays)
            {
                display.DisplayObject = null;
            }

            UpdateDisplays();

            return displays.Count();
        }

        /// <summary>
        /// Removes reference(s) to <paramref name="displayObjects"/> up to <paramref name="max"/> times each.
        /// </summary>
        /// <returns>Number of references removed.</returns>
        public int RemoveObjects(IEnumerable<ObjectType> displayObjects, int max = int.MaxValue)
        {
            if (displayObjects.Any(displayObject => displayObject == null)) throw new ArgumentNullException(nameof(displayObjects), $"Can't remove null objects.");

            if (max <= 0) return 0;

            HashSet<ObjectType> displayObjectSet = displayObjects.ToHashSet();
            IEnumerable<DisplayType> displays = displayInstances
                .Where(display => displayObjectSet.Contains(display.DisplayObject))
                .GroupBy(display => display.DisplayObject)
                .SelectMany(group => group.Take(max));
            foreach (DisplayType display in displays)
            {
                display.DisplayObject = null;
            }

            UpdateDisplays();

            return displays.Count();
        }

        /// <summary>
        /// Destroys <typeparamref name="DisplayType"/>s that match <paramref name="predicate"/>.
        /// </summary>
        protected void DestroyDisplays(Predicate<DisplayType> predicate = null)
        {
            foreach (var display in displayInstances.ToArray())
            {
                if (predicate != null && !predicate(display)) continue;

                displayInstances.Remove(display);
                ContextDestroy(display.gameObject);
            }
        }
        #endregion

        /// <summary>
        /// Sorts <typeparamref name="DisplayType"/>s by sibling index and calls <see cref="Display{ObjectType}.UpdateVisuals"/>.
        /// </summary>
        /// <remarks>Only sorts if <see cref="DisplayComparison"/> is not null.</remarks>
        public void UpdateDisplays()
        {
            if (DisplayComparison != null) displayInstances.Sort(DisplayComparison);
            int extraChildCount = displayParent.childCount - displayInstances.Count;
            for (int i = 0; i < displayInstances.Count; i++) displayInstances[i].transform.SetSiblingIndex(extraChildCount + i);

            foreach (var display in displayInstances) display.UpdateVisuals();
        }
    }
}