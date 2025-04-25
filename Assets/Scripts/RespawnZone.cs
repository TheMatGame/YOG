using System;
using UnityEngine;

public class RespawnZone : MonoBehaviour
{
    [SerializeField] LevelController levelController;
    void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.transform.position = levelController.GetLastCheckPoint();
        RespawnZone.Instance.PlayerDied();
    }

    public static RespawnZone Instance;

    public event Action OnPlayerDeath;

    public void PlayerDied()
    {
        if (OnPlayerDeath != null)
            OnPlayerDeath.Invoke();
    }

    void Awake()
    {
        Instance = this;
    }
}
