using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Player player => GetComponent<Player>();

    private void AnimationTrigger() => player.AnimationTrigger();


}
