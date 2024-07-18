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
            HexagonDirectional hexagonDirectional = collision.attachedRigidbody.gameObject.GetComponent<HexagonDirectional>();
            if (hexagonDirectional is not null)
            {
                //If there is already a list of hexagons powering up
                if(generators.Count > 0)
                {
                    //Check if it is NOT already inside the list
                    if(!generators.Contains(hexagonDirectional))
                    {
                        //Add the directional hexagon and power
                        generators.Add(hexagonDirectional);
                        PowerUp();
                    }
                }
                //If list is empty, add directly new directional hexagon on list
                else
                {
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