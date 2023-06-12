using Panda;

public class CombatEnemyInput : CombatInput
{
    [Task]
    public void Attack() {
        OnPress?.Invoke();
    }
}
