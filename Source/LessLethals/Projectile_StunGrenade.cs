using Verse;
using RimWorld;
using UnityEngine;

namespace LessLethals
{
    /// <summary>
    /// Overrides and Executes Explode() from Projectile_Explosive Class adding a custom Mote.
    /// </summary>

    //  Projectile_Explosive Source: "\RIMWORLD_FOLDER\Source\Verse\Thing\Projectile_Explosive.cs"
    public class Projectile_StunGrenade : Projectile_Explosive
    {
        /// <summary>
        /// Overrides Projectile_Explosive.Explode() adding a custom Mote.
        /// </summary>

        protected override void Explode()
        {
            // Store the custom XML def AS one with the custom <scale> tag
            LL_ThingDef mote_stunGrenade = LL_ThingDefOf.Mote_StunGrenade as LL_ThingDef;

            // Save map and (Unity) position before base method destroys the Projectile.
            var map = Map;
            Vector3 position = ExactPosition;

            // Execute DoExplosion and Destroy from base
            base.Explode();

            // Generate the custom Mote effect
            MoteMaker.MakeStaticMote(position, map, mote_stunGrenade, mote_stunGrenade.scale);
        }
    }
}
