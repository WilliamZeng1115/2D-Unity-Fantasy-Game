using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseClass {
    
    protected int str, ds, agi, dex;

    // different class use different combination of str, ds, agi, dex to determine attack
    public abstract void ultimate();
    public abstract void basicAttack();

    public void addStrength(int str)
    {
        this.str += str;
    }

    public void addDivineStrength(int ds)
    {
        this.ds += ds;
    }

    public void addAgility(int agi)
    {
        this.agi += agi;
    }

    public void addDex(int dex)
    {
        this.dex += dex;
    }

    public int getStr()
    {
        return str;
    }

    public int getDS()
    {
        return ds;
    }

    public int getAgi()
    {
        return agi;
    }

    public int getDex()
    {
        return dex;
    }
}
