namespace MovieGuide.Common.Model.General
{
    public class SortType
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public override string ToString()
        {
            return Properties.Resources.ResourceManager.GetString(Name);
        }

    }
}
