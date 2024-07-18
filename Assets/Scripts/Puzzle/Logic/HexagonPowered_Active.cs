using System.Collections.Generic;
using UnityEngine;
using Glasshouse.Puzzles.UI;

namespace Glasshouse.Puzzles.Logic
{
    public class HexagonPowered_Active : HexagonPowered
    {
        [SerializeField] protected List<HexagonDirectional> generators;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log($"{gameObject.name} collided with {collision.attachedRigidbody.gameObject.name}");
            HexagonDirectional hexagonDirectional = collision.attachedRigidbody.gameObject.GetComponent<HexagonDirectional>();
            if (hexagonDirectional is not null)
            {
                //If there is already a list of hexagons powering up
                if(generators.Count > 0)
                {
                    Debug.Log("List is not empty");
                    //Check if it is NOT already inside the list
                    if(!generators.Contains(hexagonDirectional))
                    {
                        Debug.Log($"List does NOT contain {hexagonDirectional.name}");
                        //Add the directional hexagon and power
                        generators.Add(hexagonDirectional);
                        PowerUp();
                    }
                    else
                    {
                        Debug.Log($"List DOES contain {hexagonDirectional.name}");
                    }
                }
                //If list is empty, add directly new directional hexagon on list
                else
                {
                    Debug.Log($"List is empty. Add directly {hexagonDirectional.name}");
                    //Add the directional hexagon and power
                    generators.Add(hexagonDirectional);
                    PowerUp();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            HexagonDirectional hexagonDirectional = collision.attachedRigidbody.gameObject.GetComponent<HexagonDirectional>();
            if (hexagonDirectional)
            {
                //If there is a list of hexagons powering up
                if (generators.Count > 0)
                {
                    //Check if it IS already inside the list
                    if (generators.Contains(hexagonDirectional))
                    {
                        //Remove the directional hexagon and power
                        generators.Remove(hexagonDirectional);
                        PowerDown();
                    }
                }
            }
        }
    }
}