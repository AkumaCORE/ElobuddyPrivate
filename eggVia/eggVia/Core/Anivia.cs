﻿using System;
using System.Linq;
using eggVia.Modes;
using eggVia.Utils;
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
            Hacks.RenderWatermark = false;
            GameObject.OnCreate += OnCreate;
            GameObject.OnDelete += OnDelete;
            Interrupter.OnInterruptableSpell += InTerrupter;
            Gapcloser.OnGapcloser += OnGapCloser;
            Game.OnUpdate += LevelUP.Game_OnUpdate;
            Game.OnNotify += GameOnOnNotify;
            //Drawing.OnDraw += Casts.OnEndDraw;
            Game.OnTick += OnTick;
        }

        private static void GameOnOnNotify(GameNotifyEventArgs args)
        {
            if (args.EventId == GameEventId.OnChampionKill)
            {
                var killer = ObjectManager.GetUnitByNetworkId<Obj_AI_Base>(args.NetworkId);
                
                if (killer.IsMe)
                {
                    Chat.Say("/masterybadge"); // mostrar a maestria LEK
                    EloBuddy.SDK.Core.DelayAction(() => Chat.Say("/l"), 0x3e8);
                }
            }
        }

        private static void OnGapCloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (!sender.IsEnemy || sender.IsDead || sender.IsZombie || sender.IsAlly) return;
            if (sender.IsValidTarget(0x12c) && Q.IsReady())
            {
                Q.Cast(sender);
            }
            else if (W.IsReady() && sender.IsValidTarget(W.Range))
            {
                W.Cast(e.End);
            }
        }

        private static void InTerrupter(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (!sender.IsEnemy || sender.IsDead || sender.IsZombie || sender.IsAlly) return;
            if (W.IsReady() && sender.IsValidTarget(W.Range))
            {
                if (sender.HasBuff("Recall") || sender.HasBuff("Teleport"))
                {
                    W.Cast(sender.ServerPosition);
                }
                else if (e.DangerLevel == DangerLevel.High)
                {
                    W.Cast(sender.ServerPosition);
                }
            }
        }

        private static void OnTick(EventArgs args)
        {
            if (Q.IsReady() && QMissle != null && QMissle.Position.CountEnemiesInRange(0xb4) > 0) 
                Q.Cast(QMissle.Position);
            if (Orbwalker.ActiveModesFlags.Equals(Orbwalker.ActiveModes.Combo))
            {
                var target = TargetSelector.TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                if (target != null)
                {
                    if (R.IsReady() && RMissle == null)
                    {
                        R.Cast(target.ServerPosition);
                    }
                    if (R.IsReady() && RMissle != null && !target.IsDead && !target.IsZombie &&
                        RMissle.Position.CountEnemiesInRange(0x1c2) == 0x0)
                    {
                        Player.CastSpell(SpellSlot.R, RMissle.Position);
                    }

                    if (Q.IsReady())
                    {
                        var pred = Q.GetPrediction(target);
                        if (pred.HitChance >= HitChance.High && QMissle == null)
                            Q.Cast(pred.CastPosition);
                    }
                    if (W.IsReady())
                    {
                        if (_Player.CountEnemiesInRange(W.Range) <
                            EntityManager.Heroes.Allies.Where(i => i.Distance(_Player) <= W.Range).ToList().Count)
                            W.Cast(target.ServerPosition);

                        if (RMissle != null)
                        {
                            var pos = target.Position;
                            if (!target.IsFacing2(_Player))
                            {
                                Chat.Print("aeee");
                                W.Cast(pos.Extend(_Player.Position, -0x64).To3D());
                            }
                        }
                    }
                    if (target.HasBuffUntil("Chilled", _Player.Distance(target)/0x352) && E.IsReady() ||
                        _Player.GetSpellDamage(target, SpellSlot.E) >= target.Health)
                    {
                        E.Cast(target);
                    }
                    if (_Player.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Ignite) >= target.Health &&
                        _Player.Distance(target) <= 0x258)
                    {
                        _Player.Spellbook.CastSpell(Ignite, target);
                    }
                }
            }
            if (Orbwalker.ActiveModesFlags.Equals(Orbwalker.ActiveModes.Harass))
            {
                var target = TargetSelector.TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                if (target == null) return;
                if (Q.IsReady() && target.Distance(_Player.Position) <= E.Range && E.IsLearned)
                {
                    var pred = Q.GetPrediction(target);
                    if (pred.HitChance >= HitChance.High && QMissle == null)
                        Q.Cast(pred.CastPosition);
                }
                if (E.IsReady() && target.HasBuffUntil("Chilled", _Player.Distance(target)/0x352) ||
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
            if (Orbwalker.ActiveModesFlags.Equals(Orbwalker.ActiveModes.LastHit))
            {
                LastHit.useLH();
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