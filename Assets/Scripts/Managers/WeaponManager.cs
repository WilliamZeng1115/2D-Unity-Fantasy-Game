using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponManager : Manager
{
    public string animationName;
    public Texture icon;
    protected Animator anim;
    protected Dictionary<string, float> stats;

    public abstract void attack();
    
    public void drop()
    {
        Destroy(this);
    }

    public void applyStats(Dictionary<string, int> stats)
    {
        if (this.stats == null) this.stats = new Dictionary<string, float>();
        foreach (KeyValuePair<string, int> stat in stats)
        {
            if (this.stats.ContainsKey(stat.Key)) {
                applyStat(stat.Key, stat.Value);
            }
            else {
                this.stats.Add(stat.Key, (stat.Value / 10.0f));
            }
        }
    }

    public void applyStat(string id, int stat)
    {
        this.stats[id] = stat / 10.0f;
    }

    public Texture2D getIcon()
    {
        return (Texture2D)icon;
    }
}
