using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    public enum UseType {Tap,Hold};
    public float holdNecessary;

    void Start()
    {
        
    }

    void Update()
    {
        OnUpdate();
    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnUsePress()
    {

    }

    public virtual void OnUseRelease()
    {

    }

    public virtual void OnUseHold(float holdDuration)
    {

    }

    public virtual bool IsInCooldown()
    {
        return false;
    }
}
