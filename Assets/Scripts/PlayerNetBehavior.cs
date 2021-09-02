using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerNetBehavior : NetworkBehaviour
{
   public string[] path;
   public void UnShowObj(GameObject gameObject)
   {
      if (isClient == false && isServer == false)
      {
         gameObject.SetActive(false);
      }
   }
}
