﻿using wServer.realm;
using wServer.realm.entities;

namespace wServer.logic.transitions
{
    public class DamageTakenTransition : Transition
    {
        //State storage: none

        private int _damage;

        public DamageTakenTransition(int damage, string targetState)
            : base(targetState)
        {
            _damage = damage;
        }

        protected override bool TickCore(Entity host, RealmTime time, ref object state)
        {
            int damageSoFar = 0;

            foreach (var i in (host as Enemy).DamageCounter.GetPlayerData())
                damageSoFar += i.Item2;

            if (damageSoFar >= _damage)
                return true;
            return false;
        }
    }
}
