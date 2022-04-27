using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class Guard
{
    public List<Flags> flags;

    public bool isSatisfied(List<Flags> state) {
        return flags.Select(  f => state.Contains(f)  ).ToList()

                    .Aggregate(  true, (fold, next) => fold && next  );
    }
}
