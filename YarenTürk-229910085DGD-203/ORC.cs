using System;

public class Orc : Enemy
{

    private const int orcHealth = 20;

    private const int orcMinDamage = 3;
    private const int orcMaxDamage = 10;

    /*
    public int Damage
    {
        get
        {
            return orcMaxDamage;
            //Random newRandom = new Random();
            //return newRandom.Next(orcMinDamage, orcMaxDamage
