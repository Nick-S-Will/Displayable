using UnityEngine;

namespace Displayable.Samples.UserDisplay
{
    /// <summary>
    /// Sample object.
    /// </summary>
    [CreateAssetMenu(fileName = "User", menuName = "Scriptable Objects/Displayable/Samples/User Display/User")]
    public class User : ScriptableObject
    {
        [field: SerializeField] public Color Tint { get; set; } = Color.white;
        [field: SerializeField] public Sprite ProfilePicture { get; set; }
        [field: SerializeField] public string Username { get; set; }
    }
}