using System;
using Verse;
using RimWorld;

namespace LessLethals
{
    public class Projectile_StunGrenade : Projectile
    {
        private int ticksToDetonation = 0;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.LookValue(ref ticksToDetonation, "ticksToDetonation");
        }


        public override void Tick()
        {
            base.Tick();

            if (ticksToDetonation > 0)
            {
                ticksToDetonation--;

                if (ticksToDetonation <= 0)
                    Explode();
            }
        }

        protected override void Impact(Thing hitThing)
        {
            if (def.projectile.explosionDelay == 0)
            {
                Explode();
                return;
            }
            else
            {
                landed = true;
                ticksToDetonation = def.projectile.explosionDelay;
                GenExplosion.NotifyNearbyPawnsOfDangerousExplosive(this, def.projectile.damageDef, launcher.Faction);
            }
        }

        protected virtual void Explode()
        {
            var map = Map; // before Destroy()!

            Destroy();

            GenExplosion.DoExplosion(Position, map, def.projectile.explosionRadius, def.projectile.damageDef, launcher,
                explosionSound: def.projectile.soundExplode,
                projectile: def,
                source: equipmentDef,
                postExplosionSpawnThingDef: def.projectile.postExplosionSpawnThingDef,
                postExplosionSpawnChance: def.projectile.explosionSpawnChance,
                applyDamageToExplosionCellsNeighbors: false,
                preExplosionSpawnThingDef: def.projectile.preExplosionSpawnThingDef,
                preExplosionSpawnChance: def.projectile.explosionSpawnChance);
            //Adds a bright flash to the stun grenade explosion
            MoteMaker.ThrowLightningGlow(base.Position.ToVector3Shifted(), map, 10F);
        }
    }
}
