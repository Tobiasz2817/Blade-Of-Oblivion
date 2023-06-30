using UnityEngine;
using Utilities;

public class PlayerSingleton : Singleton<PlayerSingleton>
{
    public Transform GetPosition() => transform;
    public Health playerHealth;
}
