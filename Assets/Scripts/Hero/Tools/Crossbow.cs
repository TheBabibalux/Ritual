using System.Collections;
using System.Net;
using UnityEngine;

public class Crossbow : Tool
{
    public enum State { Empty, Loading, Loaded}
    public State state;

    public GameObject projectileSpawnPoint;
    public float power;
    public GameObject loadedProjectilePos;
    GameObject projectileVisual;

    public bool isInCooldown;
    public float cooldownDuration;
    public float holdDelay;
    public GameObject projectile;

    private void Update()
    {
        OnUpdate();
    }

    public override void OnUpdate()
    {
        switch(state)
        {
            case State.Loading:
                if (HeroController.Instance._input.useHold == 0)
                {
                    state = State.Empty;
                    UI_Manager.Instance.UpdateHoldSlider(0);
                }
                break;
        }
    }

    public override void OnUsePress()
    {
        base.OnUsePress();

        switch (state)
        {
            case State.Loaded:
                if(HeroController.Instance._input.useHold < 0.1f)
                {
                    state = State.Empty;
                    Camera cam = Camera.main;

                    GameObject proj = Instantiate(projectile, projectileSpawnPoint.transform.position, projectileSpawnPoint.transform.rotation, null);
                    proj.GetComponent<Projectile>().SetupProjectile(cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 100)), power);
                    StartCooldown();

                    DisableLoadedVisual();
                }
                break;
        }
    }

    public override void OnUseHold(float holdDuration)
    {
        base.OnUseHold(holdDuration);

        switch (state)
        {
            case State.Empty:
                if(!IsInCooldown())
                {
                    state = State.Loading; 
                    UI_Manager.Instance.SetupHoldSlider(holdDelay);
                }
                break;
            case State.Loading:
                if (holdDuration < holdDelay)
                {
                    UI_Manager.Instance.UpdateHoldSlider(holdDuration);
                }
                else if (holdDuration > holdDelay)
                {
                    state = State.Loaded;

                    UI_Manager.Instance.UpdateHoldSlider(0);
                    EnableLoadedVisual();
                }

                break;
        }
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

    #region Visual
    public void EnableLoadedVisual()
    {
        projectileVisual = Instantiate(projectile, loadedProjectilePos.transform);
    }

    public void DisableLoadedVisual()
    {
        Destroy(projectileVisual);
    }


    #endregion
}
