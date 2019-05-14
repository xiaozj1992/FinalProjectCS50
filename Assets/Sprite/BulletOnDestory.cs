using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOnDestory : MonoBehaviour
{
    private void OnDestroy()
    {
        PlayerControl.bulletsInAir += 1;
    }
}
