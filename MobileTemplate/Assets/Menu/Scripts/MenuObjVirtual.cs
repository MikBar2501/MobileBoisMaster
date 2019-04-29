using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuObjVirtual : MonoBehaviour
{
    public virtual void DoStuff()
    {
        print("do");
        transform.position += new Vector3(0, 1, 0);
    }
}
