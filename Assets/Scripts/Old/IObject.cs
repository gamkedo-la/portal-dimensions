using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObject
{
    string name { get; set; }
    Type type { get; set; }
    float health { get; set; }

    void Damage();
    string GetName();
    Types GetType();
    float GetHealth();
    void SetName(string name);
    void SetType(Type type);
    void SetHealth(float health);


}
