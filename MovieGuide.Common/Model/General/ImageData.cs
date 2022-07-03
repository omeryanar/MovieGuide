namespace MovieGuide.Common.Model.General
{
    public class ImageData
    {
        [JsonPropertyName("file_path")]
        public string FilePath { get; set; }

        [JsonPropertyName("aspect_ratio")]
        public double AspectRatio { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }
    }
}
