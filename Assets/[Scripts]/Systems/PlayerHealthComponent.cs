using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthComponent : HealthComponent
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        PlayerEvents.Invoke_OnHealthInitialized(this);
    }

    public override void Destroy()
    {

        //load new game over scene when at 0 HP
        //could also fade something in like a black UI canvas and play some death music?
        //play a death animation
        SceneManager.LoadScene("GameOver");
        base.Destroy();
    }
}
