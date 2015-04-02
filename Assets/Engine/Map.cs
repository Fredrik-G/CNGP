
namespace Engine
{
    class Map
    {
        public string Name { get; set; }
        private Size size;

        public enum Size
        {
            Small,
            Medium,
            Large   
        }

        public Size enumProperty
        {
            get { return size; }
            set { size = value; }

        }
    }
}
