namespace MovieGuide.Common.Helper
{
    public class SignHelper
    {
        public static string GetSignByBirthday(DateTime? birthday)
        {
            if (birthday == null)
                return String.Empty;

            DateTime birthDate = birthday.Value;
            switch (birthDate.Month)
            {
                case 1: return birthDate.Day < 20 ? Properties.Resources.Capricorn : Properties.Resources.Aquarius;
                case 2: return birthDate.Day < 19 ? Properties.Resources.Aquarius : Properties.Resources.Pisces;
                case 3: return birthDate.Day < 21 ? Properties.Resources.Pisces : Properties.Resources.Aries;
                case 4: return birthDate.Day < 20 ? Properties.Resources.Aries : Properties.Resources.Taurus;
                case 5: return birthDate.Day < 21 ? Properties.Resources.Taurus : Properties.Resources.Gemini;
                case 6: return birthDate.Day < 21 ? Properties.Resources.Gemini : Properties.Resources.Cancer;
                case 7: return birthDate.Day < 23 ? Properties.Resources.Cancer : Properties.Resources.Leo;
                case 8: return birthDate.Day < 23 ? Properties.Resources.Leo : Properties.Resources.Virgo;
                case 9: return birthDate.Day < 23 ? Properties.Resources.Virgo : Properties.Resources.Libra;
                case 10: return birthDate.Day < 23 ? Properties.Resources.Libra : Properties.Resources.Scorpio;
                case 11: return birthDate.Day < 22 ? Properties.Resources.Scorpio : Properties.Resources.Sagittarius;
                case 12: return birthDate.Day < 22 ? Properties.Resources.Sagittarius : Properties.Resources.Capricorn;
                default: return String.Empty;
            }
        }
    }
}
