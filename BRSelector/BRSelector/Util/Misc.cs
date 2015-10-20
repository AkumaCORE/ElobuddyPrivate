using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace BRSelector.Util
{
    public static class Misc
    {
        public static bool IsChecked(Menu obj, string value)
        {
            return obj[value].Cast<CheckBox>().CurrentValue;
        }

        public static int GetSliderValue(Menu obj, string value)
        {
            return obj[value].Cast<Slider>().CurrentValue;
        }
    }
}
