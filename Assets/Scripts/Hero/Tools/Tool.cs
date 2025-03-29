using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    public enum UseType {Tap,Hold};

    void Start()
    {
        
    }

    void Update()
    {
        OnUpdate();
    }

    public abstract void OnUpdate();
    public abstract void OnUse();
    public abstract bool IsInCooldown();
}
