using UnityEngine;

public class Test : MonoBehaviour {
    public int houses;

    [ContextMenu("Method")]
    public void Method() {
        print(houses - 1 <= 0);
    }

}