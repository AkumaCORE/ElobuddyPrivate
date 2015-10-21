using System;
using System.Linq;
using BRSelector;
using BRSelector.Model;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using OneForWeek.Draw.Notifications;
using OneForWeek.Model.Notification;
using OneForWeek.Util.Misc;
using SharpDX;

namespace OneForWeek.Plugin.Hero
{
    class Malzahar : PluginModel, IChampion
    {
        public static Spell.Skillshot Q;
        public static Spell.Skillshot W;
        public static Spell.Targeted E;
        public static Spell.Targeted R;
        public static GameObject WMissle;

        private bool ultimateON = false;

        public Malzahar()
        {
            Init();
        }

        public void Init()
        {
            InitVariables();
            Selector.Init();
        }

        public void InitVariables()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 900, SkillShotType.Linear, 500, 1600, 450);
            W = new Spell.Skillshot(SpellSlot.W, 800, SkillShotType.Circular, 500, 20000, 450);
            E = new Spell.Targeted(SpellSlot.E, 650);
            R = new Spell.Targeted(SpellSlot.R, 700);
            InitMenu();

            Orbwalker.OnPostAttack += OnAfterAttack;
            Gapcloser.OnGapcloser += OnGapCloser;
            Interrupter.OnInterruptableSpell += OnPossibleToInterrupt;
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpell;

            GameObject.OnCreate += GameObjectOnCreate;
            GameObject.OnDelete += GameObjectOnDelete;
            Game.OnUpdate += OnGameUpdate;
            Drawing.OnDraw += OnDraw;

            Notification.DrawNotification(new NotificationModel(Game.Time, 10f, 1f, ObjectManager.Player.ChampionName + " Injected.", System.Drawing.Color.Purple));
            Notification.DrawNotification(new NotificationModel(Game.Time, 10f, 1f," Addon By Vector", System.Drawing.Color.Green));
        }

        public void InitMenu()
        {
            Menu = MainMenu.AddMenu(GCharname, GCharname);

            Menu.AddLabel("Version: " + GVersion);
            Menu.AddSeparator();
            Menu.AddLabel("By Vector");

            DrawMenu = Menu.AddSubMenu("Draw - " + GCharname, GCharname + "Draw");
            DrawMenu.AddGroupLabel("Draw");
            DrawMenu.Add("drawDisable", new CheckBox("Turn off all drawings", true));
            DrawMenu.Add("drawQ", new CheckBox("Draw Q Range", true));
            DrawMenu.Add("drawW", new CheckBox("Draw W Range", true));
            DrawMenu.Add("drawE", new CheckBox("Draw E Range", true));
            DrawMenu.Add("drawR", new CheckBox("Draw R Range", true));

            ComboMenu = Menu.AddSubMenu("Combo - " + GCharname, GCharname + "Combo");
            ComboMenu.AddGroupLabel("Combo");
            ComboMenu.Add("comboQ", new CheckBox("Use Q", true));
            ComboMenu.Add("comboW", new CheckBox("Use W", true));
            ComboMenu.Add("comboE", new CheckBox("Use E", true));
            ComboMenu.Add("comboR", new CheckBox("Use R", true));
            ComboMenu.AddGroupLabel("Misc Combo");
            ComboMenu.Add("flashCombo", new CheckBox("Flash Combo", true));

            HarassMenu = Menu.AddSubMenu("Harass - " + GCharname, GCharname + "Harass");
            HarassMenu.AddGroupLabel("Harass");
            HarassMenu.Add("hsQ", new CheckBox("Use Q", true));
            HarassMenu.Add("hsW", new CheckBox("Use W", true));
            HarassMenu.Add("hsE", new CheckBox("Use E", true));

            LaneClearMenu = Menu.AddSubMenu("Lane Clear - " + GCharname, GCharname + "LaneClear");
            LaneClearMenu.AddGroupLabel("Lane Clear");
            LaneClearMenu.Add("lcQ", new CheckBox("Use Q", true));
            LaneClearMenu.Add("lcW", new CheckBox("Use W", true));
            LaneClearMenu.Add("lcE", new CheckBox("Use E", true));

            MiscMenu = Menu.AddSubMenu("Misc - " + GCharname, GCharname + "Misc");
            MiscMenu.Add("ksOn", new CheckBox("Try to KS", true));
            MiscMenu.Add("miscAntiGapQ", new CheckBox("Anti Gap Closer Q", true));
            MiscMenu.Add("miscInterrupterQ", new CheckBox("Anti Interrupter Q", true));

        }

        public void OnCombo()
        {
            var target = AdvancedTargetSelector.GetTarget(1000, DamageType.Magical);

            if (target == null || !target.IsValidTarget(Q.Range)) return;

            var flash = Player.Spells.FirstOrDefault(a => a.SData.Name == "summonerflash");

            if (Misc.IsChecked(ComboMenu, "comboQ") && Q.IsReady() && target.IsValidTarget(Q.Range))
            {
                var predict = Q.GetPrediction(target);

                if (predict.HitChancePercent >= 70)
                {
                    Q.Cast(predict.CastPosition);
                }
            }

            if (Misc.IsChecked(ComboMenu, "comboE") && E.IsReady() && target.IsValidTarget(E.Range))
            {
                E.Cast(target);
            }

            if (Misc.IsChecked(ComboMenu, "comboW") && W.IsReady() && target.IsValidTarget(W.Range))
            {
                var predict = W.GetPrediction(target);

                if (predict.HitChancePercent >= 70)
                {
                    W.Cast(predict.CastPosition);
                }
            }

            if (Misc.IsChecked(ComboMenu, "comboR") && R.IsReady())
            {
                if (WMissle != null && WMissle.Position.Distance(target) < W.Radius && target.IsValidTarget(R.Range))
                {
                    Orbwalker.DisableAttacking = true;
                    Orbwalker.DisableMovement = true;
                    ultimateON = true;
                    R.Cast(target);
                }
            }

        }

        public void IsUlting()
        {
            var hasBuff = false;

            foreach (var buff in _Player.Buffs.Where(buff => buff.IsValid).Where(buff => buff.DisplayName == "AlZaharNetherGraspSound"))
            {
                hasBuff = true;
            }

            ultimateON = hasBuff;
        }

        public void OnHarass()
        {
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);

            if (target == null || !target.IsValidTarget(Q.Range)) return;

            if (Misc.IsChecked(ComboMenu, "hsQ") && Q.IsReady() && target.IsValidTarget(Q.Range))
            {
                var predict = Q.GetPrediction(target);

                if (predict.HitChancePercent >= 70)
                {
                    Q.Cast(predict.CastPosition);
                }
            }

            if (Misc.IsChecked(ComboMenu, "hsE") && E.IsReady() && target.IsValidTarget(E.Range))
            {
                E.Cast(target);
            }

            if (Misc.IsChecked(ComboMenu, "hsW") && W.IsReady() && target.IsValidTarget(W.Range))
            {
                var predict = W.GetPrediction(target);

                if (predict.HitChancePercent >= 70)
                {
                    W.Cast(predict.CastPosition);
                }
            }

        }

        public void OnLaneClear()
        {
            var minions = EntityManager.MinionsAndMonsters.EnemyMinions;

            if (minions == null || !minions.Any()) return;


            if (Misc.IsChecked(LaneClearMenu, "lcQ") && Q.IsReady())
            {
                var bestFarmQ =
                Misc.GetBestCircularFarmLocation(
                    EntityManager.MinionsAndMonsters.EnemyMinions.Where(x => x.Distance(_Player) <= Q.Range)
                        .Select(xm => xm.ServerPosition.To2D())
                        .ToList(), Q.Width, Q.Range);
                if (bestFarmQ.MinionsHit > 0)
                {
                    Q.Cast(bestFarmQ.Position.To3D());
                }

            }

            if (Misc.IsChecked(LaneClearMenu, "lcW") && W.IsReady())
            {
                var bestFarmW =
                Misc.GetBestCircularFarmLocation(
                    EntityManager.MinionsAndMonsters.EnemyMinions.Where(x => x.Distance(_Player) <= W.Range)
                        .Select(xm => xm.ServerPosition.To2D())
                        .ToList(), W.Width, W.Range);

                if (bestFarmW.MinionsHit > 0)
                {
                    W.Cast(bestFarmW.Position.To3D());
                }
            }

            if (Misc.IsChecked(LaneClearMenu, "lcE") && E.IsReady())
            {
                var lowHpMinion =
                    EntityManager.MinionsAndMonsters.EnemyMinions.FirstOrDefault(
                        t => _Player.GetAutoAttackDamage(t) > t.Health && t.IsValidTarget(E.Range));

                if (lowHpMinion != null && !lowHpMinion.IsDead)
                {
                    E.Cast(lowHpMinion);
                }
            }

        }

        public void OnFlee()
        {

        }

        public void OnGameUpdate(EventArgs args)
        {
            IsUlting();

            if (ultimateON)
            {
                Orbwalker.DisableAttacking = true;
                Orbwalker.DisableMovement = true;
                return;
            }
            if (Orbwalker.DisableAttacking || Orbwalker.DisableMovement)
            {
                Orbwalker.DisableAttacking = false;
                Orbwalker.DisableMovement = false;
            }

            switch (Orbwalker.ActiveModesFlags)
            {
                case Orbwalker.ActiveModes.Combo:
                    OnCombo();
                    break;
                case Orbwalker.ActiveModes.Flee:
                    OnFlee();
                    break;
                case Orbwalker.ActiveModes.Harass:
                    OnHarass();
                    break;
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
                OnLaneClear();

            if (Misc.IsChecked(MiscMenu, "ksOn"))
                KS();
        }

        public void OnDraw(EventArgs args)
        {
            if (Misc.IsChecked(DrawMenu, "drawDisable"))
                return;

            if (Misc.IsChecked(DrawMenu, "drawQ"))
                Circle.Draw(Q.IsReady() ? Color.Blue : Color.Red, Q.Range, Player.Instance.Position);

            if (Misc.IsChecked(DrawMenu, "drawW"))
                Circle.Draw(W.IsReady() ? Color.Blue : Color.Red, W.Range, Player.Instance.Position);

            if (Misc.IsChecked(DrawMenu, "drawE"))
                Circle.Draw(E.IsReady() ? Color.Blue : Color.Red, E.Range, Player.Instance.Position);

            if (Misc.IsChecked(DrawMenu, "drawR"))
                Circle.Draw(R.IsReady() ? Color.Blue : Color.Red, R.Range, Player.Instance.Position);

        }

        public void OnAfterAttack(AttackableUnit target, EventArgs args)
        {

        }

        public void OnPossibleToInterrupt(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs interruptableSpellEventArgs)
        {
            if (!sender.IsEnemy) return;

            if (Q.IsReady() && sender.IsValidTarget(Q.Range) && Misc.IsChecked(MiscMenu, "miscInterrupterQ"))
            {
                var predict = Q.GetPrediction(sender);

                if (predict.HitChancePercent >= 70)
                {
                    Q.Cast(predict.CastPosition);
                }
            }
        }

        public void OnGapCloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (!sender.IsEnemy) return;

            if (Q.IsReady() && sender.IsValidTarget(Q.Range) && Misc.IsChecked(MiscMenu, "miscAntiGapQ"))
            {
                Q.Cast(e.End);
            }
        }

        public void OnProcessSpell(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
        }

        public void GameObjectOnCreate(GameObject obj, EventArgs args)
        {
            if (!obj.IsValid || !obj.Name.Contains("Malzahar")) return;

            if (obj.Name == "Malzahar_Base_W_flash.troy")
            {
                WMissle = obj;
            }
        }

        public void GameObjectOnDelete(GameObject obj, EventArgs args)
        {
            if (!obj.IsValid || !obj.Name.Contains("Malzahar")) return;

            if (obj.Name == "Malzahar_Base_W_aoe_green.troy")
            {
                WMissle = obj;
            }
        }

        private static void KS()
        {

        }

        private static float PossibleDamage(Obj_AI_Base target)
        {
            var damage = 0f;
            if (R.IsReady())
                damage = _Player.GetSpellDamage(target, SpellSlot.R);
            if (E.IsReady())
                damage = _Player.GetSpellDamage(target, SpellSlot.E);
            if (W.IsReady())
                damage = _Player.GetSpellDamage(target, SpellSlot.W);
            if (Q.IsReady())
                damage = _Player.GetSpellDamage(target, SpellSlot.Q);

            return damage;
        }
    }
}
