using System;
using eggVia.Modes;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;

namespace eggVia.Core
{
    internal class Anivia : Model
    {
        public static void Init()
        {
            Spells.setSpells();
            MenuX.getMenu();
            GameObject.OnCreate += OnCreate;
            GameObject.OnDelete += OnDelete;
            Interrupter.OnInterruptableSpell += InTerrupter;
            Gapcloser.OnGapcloser += OnGapCloser;
            Game.OnTick += OnTick;
        }

        private static void OnGapCloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (!sender.IsEnemy || sender.IsDead || sender.IsZombie) return;
            if (sender.IsValidTarget(300) && Q.IsReady())
            {
                Q.Cast(sender);
            }
            else if (W.IsReady() && sender.IsValidTarget(W.Range))
            {
                W.Cast(sender.Position.Extend(_Player.Position, -50).To3D()); // facing? gapcloser ofc lel
            }
        }

        private static void InTerrupter(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (!sender.IsEnemy || sender.IsDead || sender.IsZombie) return;
            if (W.IsReady() && sender.IsValidTarget(W.Range))
            {
                W.Cast(sender);
            }
        }

        private static void OnTick(EventArgs args)
        {
            if (Q.IsReady() && QMissle != null && QMissle.Position.CountEnemiesInRange(200) > 0)
                Q.Cast(QMissle.Position);
            if (Orbwalker.ActiveModesFlags.Equals(Orbwalker.ActiveModes.Combo))
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                if (target != null)
                {
                    if (R.IsReady() && RMissle == null)
                    {
                        R.Cast(target.ServerPosition);
                    }
                    if (R.IsReady() && RMissle != null && !target.IsDead && !target.IsZombie &&
                        RMissle.Position.CountEnemiesInRange(450) == 0)
                    {
                        R.Cast(RMissle.Position);
                    }

                    if (Q.IsReady())
                    {
                        var pred = Q.GetPrediction(target);
                        if (pred.HitChance >= HitChance.High && QMissle == null)
                            Q.Cast(pred.CastPosition);
                    }
                    if (W.IsReady() && RMissle != null)
                    {
                        var pos = target.Position;
                        W.Cast(target.IsFacing(_Player)
                            ? pos.Extend(_Player.Position, -100).To3D()
                            : pos.Extend(_Player.Position, -150).To3D());
                    }
                    if (target.HasBuff("Chilled") && E.IsReady() ||
                        _Player.GetSpellDamage(target, SpellSlot.E) >= target.Health)
                    {
                        E.Cast(target);
                    }
                    if (_Player.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Ignite) >= target.Health &&
                        _Player.Distance(target) <= 600)
                    {
                        _Player.Spellbook.CastSpell(Ignite, target);
                    }
                }
            }
            if (Orbwalker.ActiveModesFlags.Equals(Orbwalker.ActiveModes.Harass))
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                if (target == null) return;
                if (Q.IsReady() && target.Distance(_Player.Position) <= E.Range && E.IsLearned)
                {
                    var pred = Q.GetPrediction(target);
                    if (pred.HitChance >= HitChance.High && QMissle == null)
                        Q.Cast(pred.CastPosition);
                }
                if (E.IsReady() && target.HasBuff("Chilled") ||
                    _Player.GetSpellDamage(target, SpellSlot.E) >= target.Health)
                {
                    E.Cast(target);
                }
            }
            if (Orbwalker.ActiveModesFlags.Equals(Orbwalker.ActiveModes.LaneClear))
            {
                LaneClear.useLC();
            }
            if (Orbwalker.ActiveModesFlags.Equals(Orbwalker.ActiveModes.Flee))
            {
                Flee.useFee();
            }
        }

        private static void OnDelete(GameObject obj, EventArgs args)
        {
            if (!obj.IsValid) return;
            if (obj.Name == "cryo_FlashFrost_Player_mis.troy")
                QMissle = null;
            if (obj.Name.Contains("cryo_storm"))
                RMissle = null;
        }

        private static void OnCreate(GameObject obj, EventArgs args)
        {
            if (!obj.IsValid) return;
            if (obj.Name == "cryo_FlashFrost_Player_mis.troy")
                QMissle = obj;
            if (obj.Name.Contains("cryo_storm"))
                RMissle = obj;
        }
    }
}