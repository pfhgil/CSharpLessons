using static Tortiki.Utils;

namespace Tortiki.Cake
{
    public class CakeOrder
    {
        public CakeOptionValue form = CakeOptionValue.NULL_VALUE;
        public CakeOptionValue size = CakeOptionValue.NULL_VALUE;
        public CakeOptionValue taste = CakeOptionValue.NULL_VALUE;
        public CakeOptionValue cakesNum = CakeOptionValue.NULL_VALUE;
        public CakeOptionValue glaze = CakeOptionValue.NULL_VALUE;
        public CakeOptionValue decor = CakeOptionValue.NULL_VALUE;

        public int GetPrice()
        {
            return
                GetCakeOptionValuePrice(form) +
                GetCakeOptionValuePrice(size) +
                GetCakeOptionValuePrice(taste) +
                GetCakeOptionValuePrice(cakesNum) +
                GetCakeOptionValuePrice(glaze) +
                GetCakeOptionValuePrice(decor);
        }

        public string ToString()
        {
            string res = "";
            if(form != CakeOptionValue.NULL_VALUE) {
                res += CakeOptionValueToRussian(form) + " - " + GetCakeOptionValuePrice(form) + "; ";
            }
            if (size != CakeOptionValue.NULL_VALUE) {
                res += CakeOptionValueToRussian(size) + " - " + GetCakeOptionValuePrice(size) + "; ";
            }
            if (taste != CakeOptionValue.NULL_VALUE) {
                res += CakeOptionValueToRussian(taste) + " - " + GetCakeOptionValuePrice(taste) + "; ";
            }
            if (cakesNum != CakeOptionValue.NULL_VALUE) {
                res += CakeOptionValueToRussian(cakesNum) + " - " + GetCakeOptionValuePrice(cakesNum) + "; ";
            }
            if (glaze != CakeOptionValue.NULL_VALUE) {
                res += CakeOptionValueToRussian(glaze) + " - " + GetCakeOptionValuePrice(glaze) + "; ";
            }
            if (decor != CakeOptionValue.NULL_VALUE) {
                res += CakeOptionValueToRussian(decor) + " - " + GetCakeOptionValuePrice(decor) + "; ";
            }


            return res;
        }

        public void Clear()
        {
            form = CakeOptionValue.NULL_VALUE;
            size = CakeOptionValue.NULL_VALUE;
            taste = CakeOptionValue.NULL_VALUE;
            cakesNum = CakeOptionValue.NULL_VALUE;
            glaze = CakeOptionValue.NULL_VALUE;
            decor = CakeOptionValue.NULL_VALUE;
        }
    }
}
