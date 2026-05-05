using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Displayable.Samples.UserDisplay
{
    /// <summary>
    /// Simple UI functions to interact with <see cref="UserDisplayMaker"/>.
    /// </summary>
    public class UserCreator : MonoBehaviour
    {
        [SerializeField] private Sprite profilePicture;
        [SerializeField] private InputField nameInput, usernameInput;
        [SerializeField] private UserDisplayMaker userDisplayMaker;

        private readonly Stack<User> addedUsers = new();

        private void Awake()
        {
            Assert.IsNotNull(profilePicture);
            Assert.IsNotNull(nameInput);
            Assert.IsNotNull(usernameInput);
        }

        private void Start()
        {
            foreach (UserDisplay display in userDisplayMaker.Displays)
            {
                addedUsers.Push(display.DisplayObject);
            }
        }

        public void Create()
        {
            User user = ScriptableObject.CreateInstance<User>();
            user.Tint = new Color(Random.value, Random.value, Random.value);
            user.ProfilePicture = profilePicture;
            user.name = nameInput.text;
            user.Username = usernameInput.text;

            userDisplayMaker.AddObject(user);
            addedUsers.Push(user);
        }

        public void Remove()
        {
            if (addedUsers.Count == 0) return;

            User user = addedUsers.Pop();
            userDisplayMaker.RemoveObject(user);
        }
    }
}
