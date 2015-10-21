using System;
using System.Collections.Generic;
using System.Linq;
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
    class Fiora : PluginModel, IChampion
    {
        public static Spell.Skillshot Q;
        public static Spell.Skillshot W;
        public static Spell.Active E;
        public static Spell.Targeted R;

        public static List<Tuple<float, AIHeroClient>> ListTupleFloatHero = new List<Tuple<float, AIHeroClient>>();

        public Fiora()
        {
            Init();
        }

        public void Init()
        {
            InitVariables();
        }

        public void InitVariables()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 400, SkillShotType.Linear, 1750, 1600, 420);
            W = new Spell.Skillshot(SpellSlot.W, 750, SkillShotType.Linear, 500, 3500, 360);
            E = new Spell.Active(SpellSlot.E, 200);
            R = new Spell.Targeted(SpellSlot.R, 500);
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
            Notification.DrawNotification(new NotificationModel(Game.Time, 10f, 1f, " Addon By Vector", System.Drawing.Color.Green));
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
            DrawMenu.Add("drawVP", new CheckBox("Draw Vital Points", true));

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
            MiscMenu.Add("autoRiposte", new CheckBox("Riposte annoying spell's", true));
            MiscMenu.Add("ksOn", new CheckBox("Try to KS", true));
            MiscMenu.Add("miscAntiGapW", new CheckBox("Anti Gap Closer Q", true));
            MiscMenu.Add("miscInterrupterQ", new CheckBox("Anti Interrupter Q", true));

        }

        public void OnCombo()
        {
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);

            if (target == null || !target.IsValidTarget(Q.Range)) return;

            if (Misc.IsChecked(ComboMenu, "comboQ") && Q.IsReady())
            {
                foreach (var vitalPos in from tuple in ListTupleFloatHero where tuple.Item2 == target select tuple.Item1 <= 150 ? tuple.Item2.Position.Shorten(tuple.Item2.Position.To2D().Rotated(tuple.Item1).To3D(), 150) : tuple.Item2.Position.Extend(tuple.Item2.Position.To2D().Rotated(tuple.Item1), 150).To3D())
                {
                    if(vitalPos == Vector3.Zero) return;

                    if (Q.IsInRange(vitalPos))
                    {
                        Q.Cast(vitalPos);
                    }
                }
            }

            if (Misc.IsChecked(ComboMenu, "comboW") && W.IsReady())
            {
                if (!Q.IsReady() && target.Distance(_Player) > _Player.GetAutoAttackRange())
                {
                    W.Cast(target);
                }
            }
        }

        public void OnHarass()
        {
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);

            if (target == null || !target.IsValidTarget(Q.Range)) return;

        }

        public void OnLaneClear()
        {
            var minions = EntityManager.MinionsAndMonsters.EnemyMinions;

            if (minions == null || !minions.Any()) return;

        }

        public void OnFlee()
        {

        }

        public void OnGameUpdate(EventArgs args)
        {
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

            if(ListTupleFloatHero.Count == 0) return;

            if (!EntityManager.Heroes.Enemies.Any(t => t.Distance(_Player) < 1500 && !t.IsDead))
            {
                ListTupleFloatHero = new List<Tuple<float, AIHeroClient>>();
            }
        }

        public void OnDraw(EventArgs args)
        {

            if (Misc.IsChecked(DrawMenu, "drawVP"))
                foreach (var circlePos in from tuple in ListTupleFloatHero where !tuple.Item2.IsDead select tuple.Item1 <= 150 ? tuple.Item2.Position.Shorten(tuple.Item2.Position.To2D().Rotated(tuple.Item1).To3D(), 100) : tuple.Item2.Position.Extend(tuple.Item2.Position.To2D().Rotated(tuple.Item1), 100).To3D())
                {
                    Circle.Draw(Color.DeepPink, 30, circlePos);
                }

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
            if ((Misc.IsChecked(ComboMenu, "comboE") || Misc.IsChecked(ComboMenu, "hsE")) && E.IsReady() && (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo || Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Harass))
            {
                E.Cast();
            }
        }

        public void OnPossibleToInterrupt(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs interruptableSpellEventArgs)
        {
            if (!sender.IsEnemy) return;
        }

        public void OnGapCloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (!sender.IsEnemy || e.End.Distance(_Player) > 30) return;

            if (W.IsReady() && sender.IsValidTarget(W.Range) && Misc.IsChecked(MiscMenu, "miscAntiGapW"))
            {
                W.Cast(e.End);
            }
        }

        public void OnProcessSpell(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsEnemy || args.End.Distance(_Player) > 30 || !Misc.IsChecked(MiscMenu, "autoRiposte") || !W.IsReady() || !W.IsInRange(args.End)) return;

            if (Spells.Where(spell => spell == args.SData.Name).Any(spell => sender is AIHeroClient && sender.IsEnemy && args.Target.IsMe))
            {
                Player.CastSpell(SpellSlot.W, sender);
            }
        }

        public void GameObjectOnCreate(GameObject obj, EventArgs args)
        {
            if (!obj.IsValid || !obj.Name.ToLower().Contains("fiora")) return;

            if (obj.Name.Contains("_Warning.troy"))
            {
                var Angle = obj.Name.Contains("NE") ? -15 : obj.Name.Contains("NW") ? 90 : obj.Name.Contains("SE") ? 315 : obj.Name.Contains("SW") ? 225 : 0;

                ListTupleFloatHero.Add(new Tuple<float, AIHeroClient>(Angle,
                    EntityManager.Heroes.Enemies.Aggregate(
                        (curMin, x) =>
                            (curMin == null || x.Distance(obj.Position) < curMin.Distance(obj.Position) ? x : curMin))));
            }

        }

        public void GameObjectOnDelete(GameObject obj, EventArgs args)
        {
            if (!obj.IsValid || !obj.Name.ToLower().Contains("fiora")) return;

            if (!obj.Name.Contains("_Warning.troy") || obj.Name.Contains("_Timeout.troy"))
            {
                var Angle = obj.Name.Contains("NE") ? -15 : obj.Name.Contains("NW") ? 90 : obj.Name.Contains("SE") ? 315 : obj.Name.Contains("SW") ? 225 : 0;

                ListTupleFloatHero.Remove(new Tuple<float, AIHeroClient>(Angle, EntityManager.Heroes.Enemies.Aggregate((curMin, x) => (curMin == null || x.Distance(obj.Position) < curMin.Distance(obj.Position) ? x : curMin))));
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

        #region Stuff
        static readonly string[] Spells = {"AhriSeduce"
                                          , "InfernalGuardian"
                                          , "EnchantedCrystalArrow"
                                          , "InfernalGuardian"
                                          , "EnchantedCrystalArrow"
                                          , "RocketGrab"
                                          , "BraumQ"
                                          , "CassiopeiaPetrifyingGaze"
                                          , "DariusAxeGrabCone"
                                          , "DravenDoubleShot"
                                          , "DravenRCast"
                                          , "EzrealTrueshotBarrage"
                                          , "FizzMarinerDoom"
                                          , "GnarBigW"
                                          , "GnarR"
                                          , "GragasR"
                                          , "GravesChargeShot"
                                          , "GravesClusterShot"
                                          , "JarvanIVDemacianStandard"
                                          , "JinxW"
                                          , "JinxR"
                                          , "KarmaQ"
                                          , "KogMawLivingArtillery"
                                          , "LeblancSlide"
                                          , "LeblancSoulShackle"
                                          , "LeonaSolarFlare"
                                          , "LuxLightBinding"
                                          , "LuxLightStrikeKugel"
                                          , "LuxMaliceCannon"
                                          , "UFSlash"
                                          , "DarkBindingMissile"
                                          , "NamiQ"
                                          , "NamiR"
                                          , "OrianaDetonateCommand"
                                          , "RengarE"
                                          , "rivenizunablade"
                                          , "RumbleCarpetBombM"
                                          , "SejuaniGlacialPrisonStart"
                                          , "SionR"
                                          , "ShenShadowDash"
                                          , "SonaR"
                                          , "ThreshQ"
                                          , "ThreshEFlay"
                                          , "VarusQMissilee"
                                          , "VarusR"
                                          , "VeigarBalefulStrike"
                                          , "VelkozQ"
                                          , "Vi-q"
                                          , "Laser"
                                          , "xeratharcanopulse2"
                                          , "XerathArcaneBarrage2"
                                          , "XerathMageSpear"
                                          , "xerathrmissilewrapper"
                                          , "yasuoq3w"
                                          , "ZacQ"
                                          , "ZedShuriken"
                                          , "ZiggsQ"
                                          , "ZiggsW"
                                          , "ZiggsE"
                                          , "ZiggsR"
                                          , "ZileanQ"
                                          , "ZyraQFissure"
                                          , "ZyraGraspingRoots"
                                      };
        #endregion
    }
}
