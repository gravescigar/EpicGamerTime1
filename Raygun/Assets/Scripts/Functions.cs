using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functions : MonoBehaviour
{
    public static float Cooldown(float cooldown)
    {
        if (cooldown > 0)
        {
            return cooldown - Time.deltaTime;
        }

        else
        {
            return 0;
        }
    }
}
