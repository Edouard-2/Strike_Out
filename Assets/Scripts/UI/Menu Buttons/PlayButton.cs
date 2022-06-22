using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MenuButton
{
    protected override void Interact()
    {
        base.Interact();
        SceneManager.Instance.GoToScene(3);
    }
}
