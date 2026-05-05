using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Displayable.Samples.UserDisplay
{
    /// <summary>
    /// Sample <see cref="Display{ObjectType}"/> for <see cref="User"/>.
    /// </summary>
    public class UserDisplay : Display<User>
    {
        [SerializeField] private Image panel, profilePicture;
        [SerializeField] private Text nameText, usernameText;

        private void Awake()
        {
            Assert.IsNotNull(panel);
            Assert.IsNotNull(profilePicture);
            Assert.IsNotNull(nameText);
            Assert.IsNotNull(usernameText);
        }

        public override void UpdateVisuals()
        {
            if (DisplayObject == null) return;

            panel.color = DisplayObject.Tint;
            profilePicture.sprite = DisplayObject.ProfilePicture;
            nameText.text = DisplayObject.name;
            usernameText.text = DisplayObject.Username;
        }
    }
}