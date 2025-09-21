namespace Lab3
{
    class Master : Student
    {
        public Master(string name, double mark) : base(name, mark) { }

        public override string GetInfo()
        {
            return $"{Name} (магістр), бал: {Mark}";
        }
    }
}
