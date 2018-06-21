using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEntity : MonoBehaviour {

    public readonly int baseDamage, baseHealth, baseSpiritReward;

    public abstract void useBasicAtt();

    public abstract void useSkillAtt();
}
