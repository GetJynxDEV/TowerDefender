using UnityEngine;

public class FlyingEnemy : Enemy
{
    public override void Attack()
    {
        Debug.Log("Flying enemy swoops down on the base!");
    }
}
