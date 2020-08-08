using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type
{
    public const int STAR = 0;
    public const int NEBULA = 1;
    public const int SUN = 2;
    public const int GRAVITY = 3;
    public const int LIGHT = 4;
    public const int DARK = 5;

    private const int length = 6;

    private float[,] attackEffect;
    private float[,] defenseEffect;

    private Type instance;

    public Type Instance
    {
        get
        {
            if (instance == null) instance = new Type();
            return instance;
        }
    }


    private Type()
    {
        attackEffect = new float[length,length];
        defenseEffect = new float[length, length];
        for (int i = 0; i < length; i++)
            for (int j = 0;j< length; j++)
                attackEffect[i,j] = 1f;
        for (int i = 0; i < length; i++)
            for (int j = 0; j < length; j++)
                defenseEffect[i, j] = 1f;
        //Aggiungere debolezze/resistenze agli altri tipi
    }

    private float attack(int attacker,int defender)
    {
        return attackEffect[attacker, defender];
    }

    private float defense(int attacker, int defender)
    {
        return defenseEffect[attacker, defender];
    }

    public float effect(int attacker,int defenser)
    {
        return attack(attacker, defenser) * defense(attacker, defenser);
    }

}
