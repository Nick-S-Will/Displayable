using System;
using UnityEngine;

namespace Displayable.Samples.UserDisplay
{
    /// <summary>
    /// Sample <see cref="DisplayMaker{ObjectType, DisplayType}"/> for <see cref="User"/>.
    /// </summary>
    public class UserDisplayMaker : DisplayMaker<User, UserDisplay>
    {
        [Space]
        [SerializeField] private User[] awakeUsers;

        protected override void Awake()
        {
            base.Awake();

            SetObjects(awakeUsers);
        }

        protected override Comparison<UserDisplay> DisplayComparison => CompareNamesAlphabetically;

        private int CompareNamesAlphabetically(UserDisplay userDisplay1, UserDisplay userDisplay2)
        {
            string name1 = userDisplay1.DisplayObject != null ? userDisplay1.DisplayObject.name : string.Empty;
            string name2 = userDisplay2.DisplayObject != null ? userDisplay2.DisplayObject.name : string.Empty;
            return string.Compare(name1, name2, StringComparison.OrdinalIgnoreCase);
        }
    }
}
