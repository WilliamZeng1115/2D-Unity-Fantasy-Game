using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseClass {
    
    protected int str, ds, agi, dex;

    // different class use different combination of str, ds, agi, dex to determine attack
    public abstract void ultimate();
    public abstract void basicAttack();

    protected void addStrength(int str)
    {
        this.str += str;
    }

    protected void addDivineStrength(int ds)
    {
        this.ds += ds;
    }

    protected void addAgility(int agi)
    {
        this.agi += agi;
    }

    protected void addDex(int dex)
    {
        this.dex += dex;
    }

    protected int getStr()
    {
        return str;
    }

    protected int getDS()
    {
        return ds;
    }

    protected int getAgi()
    {
        return agi;
    }

    protected int getDex()
    {
        return dex;
    }
}
