using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eggVia.Core;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using SharpDX;
using Color = System.Drawing.Color;

namespace eggVia.Modes
{
    class Casts : Model
    {
        public static int[] WLargura = {400, 500, 600, 700, 800};
        // TODO auto desativar ult, quando não tiver heroi/minion sem hotkey pressionada
        public static void OnEndDraw(EventArgs args)
        {
            var mPos = Game.CursorPos;

                Drawing.DrawLine(new Vector2(mPos.X, mPos.Y),
                    new Vector2(mPos.X - WLargura[W.Level -1], mPos.Y), 2f,
                    Color.White);

            
        }
    }
}
