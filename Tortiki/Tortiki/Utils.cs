using System.Linq;
using Tortiki.Cake;

namespace Tortiki
{
    public class Utils
    {
        public static string CakeOptionToRussian(CakeOption cakeOption)
        {
            return cakeOption switch {
                CakeOption.FORM => "Форма",
                CakeOption.SIZE => "Размер",
                CakeOption.TASTE => "Вкус",
                CakeOption.AMOUNT => "Количество",
                CakeOption.GLAZE => "Глазурь",
                CakeOption.DECOR => "Декор"
            };
        }

        // узнать цену коржей
        public static int GetCakesPrice(int oneCakePrice, int cakesNum, int percentDiscount)
        {
            return oneCakePrice * cakesNum - (oneCakePrice * cakesNum / 100 * percentDiscount);
        }

        public static int GetCakeOptionValuePrice(CakeOptionValue cakeOptionValue)
        {
            return cakeOptionValue switch {
                CakeOptionValue.FORM_CIRCLE => 150,
                CakeOptionValue.FORM_SQUARE => 200,
                CakeOptionValue.FORM_RECTANGLE => 250,
                CakeOptionValue.FORM_OVAL => 225,

                CakeOptionValue.SIZE_SMALL => 150,
                CakeOptionValue.SIZE_MEDIUM => 200,
                CakeOptionValue.SIZE_BIG => 250,

                CakeOptionValue.TASTE_VANILLA => 100,
                CakeOptionValue.TASTE_CHOCOLATE => 150,
                CakeOptionValue.TASTE_CARAMEL => 175,
                CakeOptionValue.TASTE_BERRIES => 150,
                CakeOptionValue.TASTE_COCONUT => 200,

                CakeOptionValue.ONE_CAKE => 100,
                CakeOptionValue.TWO_CAKES => GetCakesPrice(100, 2, 10),
                CakeOptionValue.THREE_CAKES => GetCakesPrice(100, 3, 10),
                CakeOptionValue.FOUR_CAKES => GetCakesPrice(100, 4, 10),

                CakeOptionValue.GLAZE_CHOCOLATE => 200,
                CakeOptionValue.GLAZE_CREAM => 225,
                CakeOptionValue.GLAZE_BIZET => 250,
                CakeOptionValue.GLAZE_DRAGEE => 200,
                CakeOptionValue.GLAZE_BERRIES => 175,

                CakeOptionValue.DECOR_CHOCOLATE => 200,
                CakeOptionValue.DECOR_BERRIES => 150,
                CakeOptionValue.DECOR_CREAM => 175,

                CakeOptionValue.NULL_VALUE => 0
            };
        }

        public static string CakeOptionValueToRussian(CakeOptionValue cakeOptionValue)
        {
            if(cakeOptionValue == null) {
                return "";
            }
            return cakeOptionValue switch {
                CakeOptionValue.FORM_CIRCLE => "Круг",
                CakeOptionValue.FORM_SQUARE => "Квадрат",
                CakeOptionValue.FORM_RECTANGLE => "Прямоугольник",
                CakeOptionValue.FORM_OVAL => "Овал",

                CakeOptionValue.SIZE_SMALL => "Маленький",
                CakeOptionValue.SIZE_MEDIUM => "Средний",
                CakeOptionValue.SIZE_BIG => "Большой",

                CakeOptionValue.TASTE_VANILLA => "Ванильный",
                CakeOptionValue.TASTE_CHOCOLATE => "Шоколадный",
                CakeOptionValue.TASTE_CARAMEL => "Карамельный",
                CakeOptionValue.TASTE_BERRIES => "Ягодный",
                CakeOptionValue.TASTE_COCONUT => "Кокосовый",

                CakeOptionValue.ONE_CAKE => "1 корж",
                CakeOptionValue.TWO_CAKES => "2 коржа",
                CakeOptionValue.THREE_CAKES => "3 коржа",
                CakeOptionValue.FOUR_CAKES => "4 коржа",

                CakeOptionValue.GLAZE_CHOCOLATE => "Шоколад",
                CakeOptionValue.GLAZE_CREAM => "Крем",
                CakeOptionValue.GLAZE_BIZET => "Бизе",
                CakeOptionValue.GLAZE_DRAGEE => "Драже",
                CakeOptionValue.GLAZE_BERRIES => "Ягоды",

                CakeOptionValue.DECOR_CHOCOLATE => "Шоколадная",
                CakeOptionValue.DECOR_BERRIES => "Ягодная",
                CakeOptionValue.DECOR_CREAM => "Кремовая",

                CakeOptionValue.NULL_VALUE => ""
            };
        }
    }
}
