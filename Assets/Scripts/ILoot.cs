using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ILoot
{
    void Take(Transform target);
    void Effect();
}
