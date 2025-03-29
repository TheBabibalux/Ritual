using System.Collections;
using UnityEngine;

public class Crossbow : Tool
{
    public bool isInCooldown;
    public float cooldownDuration;

    

    public override void OnUpdate()
    {

    }

    public override void OnUse()
    {
        Debug.Log("Crossbow used");
    }

    public override bool IsInCooldown()
    {
        return isInCooldown;
    }

    public void StartCooldown()
    {
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        isInCooldown = true;
        yield return new WaitForSeconds(cooldownDuration);
        isInCooldown = false;
    }
}
