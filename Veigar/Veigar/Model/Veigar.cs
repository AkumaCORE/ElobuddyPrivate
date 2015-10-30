using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using Veigar.Controller;

namespace Veigar.Model
{
    class Veigar
    {
        static Veigar()
        {
            if (Player.Instance.Hero != Champion.Veigar) return;
            Spells.Initialize();
            ModeManager.Initialize();
            BRSelector.Selector.Init();
            Chat.Print("Loaded^>^", Color.DeepPink);
        }

        public static void Initialize() { }
    }
}
