using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    public KeyCode activationKey;

    public abstract void Activate();
}
