using System.Collections;
using UnityEngine;

public interface Attacking
{
    public IEnumerator PerformAttack(Vector2 direction);
}
