using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics {

    public CookingStatistics cookingStatistics;
    public Statistics() {
        cookingStatistics = new CookingStatistics();
    }
}

public class CookingStatistics {

    public Dictionary<int, int> numberCooked;

    public CookingStatistics() {
        numberCooked = new Dictionary<int, int>();
    }

}


