using System.Text.RegularExpressions;
using System.Web.UI;

public class Validator : Page {

    //*****************************************
    //  Custom Validator Methods
    //*****************************************
    public static bool validateInt(string s) {
        int throwaway;
        return int.TryParse(s, out throwaway);
    }

    public static bool validateString(string s) {
        return true;
    }

    public static bool validateDouble(string s) {
        double throwaway;
        return double.TryParse(s, out throwaway);
    }

    public static bool validateURL(string s) {
        Regex objPattern = new Regex("((https?|ftp|gopher|http|telnet|file|notes|ms-help):((//)|(\\\\))+[\\w\\d:#@%/;$()~_?\\+-=\\\\.&]*)");
        return objPattern.IsMatch(s);
    }
    public static bool validateEmail(string s) {
        Regex objPattern = new Regex("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
        return objPattern.IsMatch(s);
    }

    public static bool validateLength(string s, int length) {
        return s.Length <= length;
    }
}