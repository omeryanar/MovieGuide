using MovieGuide.Common.Properties;

namespace MovieGuide.Common.Helper
{
    public static class LocalizationHelper
    {
        public static string GetLocalizedText(this string text)
        {
            string result = Resources.ResourceManager.GetString(text);
            if (result != null)
                return result;

            return text;
        }

        public static string GetRuntimeDescription(this int? runtime)
        {
            if (runtime == null)
                return null;

            if (runtime < 60)
                return $"{runtime} {Resources.Minutes}";

            return runtime % 60 > 0 ? $"{runtime / 60} {Resources.Hours} {runtime % 60} {Resources.Minutes}" : $"{runtime / 60} {Resources.Hours}";
        }

        public static string GetRuntimeDescription(this List<int> runtimes)
        {
            return String.Join(" - ", runtimes?.Select(x => (x as int?).GetRuntimeDescription()));
        }

        public static string GetFormattedDate(this DateTime? releaseDate)
        {
            return releaseDate.HasValue ? releaseDate.Value.ToString("MMM d, yyyy") : String.Empty;
        }
    }
}
