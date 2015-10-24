using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BRSelector;
using EloBuddy;
using kTwitch2.Controller;
using kTwitch2.Helpers.DamageIndicator;
using SharpDX;
using SharpDX.Direct3D9;

namespace kTwitch2.Model
{

    class Twitch : Model
    {
        public static Font Font;
        public static DamageIndicator Indicator;
        public Twitch()
        {
            Font = new Font(
                Drawing.Direct3DDevice,
                new FontDescription
                {
                 FaceName   = "Segoi UI",
                 Height = 45,
                 OutputPrecision = FontPrecision.Default,
                 Quality = FontQuality.Default
                });
        }
        public void Init()
        {
            new Spells().Init();
            ModeManager.Initialize();
            ItemManager.Init();
            Selector.Init();
            Drawing.OnEndScene += OnDraw;
            Obj_AI_Base.OnProcessSpellCast += AiHeroClientOnOnProcessSpellCast;
            Chat.Print("KK2 passou por aqui");
            Indicator = new DamageIndicator();
        }

        private void AiHeroClientOnOnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe) return;
            if (args.Slot == SpellSlot.Q)
            {
                ItemManager.UseYomu();
            }
        }


        public static void OnDraw(EventArgs args)
        {
            if (_Player.IsDead) return;

            var stealthTime = GetRemainingTime();
            var mypos = Drawing.WorldToScreen(_Player.Position);
            var fancy = string.Format("{0:0}", stealthTime);
            if (stealthTime > 0)
            {

                Font.DrawText(null,
                    "" + fancy,
                    (int)mypos[0], (int)mypos[1] / 2, Color.DeepPink);

            }
        }

        private static float GetRemainingTime()
        {
            var buff = _Player.GetBuff("twitchhideinshadows");
            if (buff == null) return 0;
            return buff.EndTime - Game.Time;
        }

    }
}
