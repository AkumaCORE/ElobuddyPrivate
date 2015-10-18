using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace eggVia.Core
{
    internal class MenuX : Model
    {
        public static Slider SkinHax;
        public static void getMenu()
        {
            AniviaMenu = MainMenu.AddMenu("Anivia", "Anivia", null);
            AniviaMenu.AddGroupLabel("Anivia ueh");
            AniviaMenu.AddSeparator();
            SkinHax = AniviaMenu.Add("skinHax", new Slider("Choose your destiny: ", 0, 0, 6));
            SkinHax.OnValueChange += delegate
            {
                _Player.SetSkinId(SkinHax.CurrentValue);
            };
        }
    }
}