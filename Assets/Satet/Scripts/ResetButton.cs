using UnityEngine;
using UnityEngine.Events;
using SSR.Player;

public class ResetButton : MonoBehaviour
{


    public bool onceOnly = false;
    public Transform targetCenter;

    public Transform baseTransform;
    public Transform fallenDownTransform;
    public float fallTime = 0.5f;

    const float targetRadius = 0.25f;

    private bool targetEnabled = true;
    // Use this for initialization
    private void ApplyDamage()
    {
        PlayerEntity.Instance.ReloadGame();
    }

}

