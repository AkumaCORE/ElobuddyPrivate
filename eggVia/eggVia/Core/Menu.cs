using EloBuddy.SDK.Menu;

namespace eggVia.Core
{
    internal class MenuX : Model
    {
        public static void getMenu()
        {
            AniviaMenu = MainMenu.AddMenu("Anivia", "Anivia", null);
            AniviaMenu.AddGroupLabel("Anivia ueh");
        }
    }
}