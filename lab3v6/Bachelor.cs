namespace Lab3
{
    class Bachelor : Student
    {
        public Bachelor(string name, double mark) : base(name, mark) { }

        public override string GetInfo()
        {
            return $"{Name} (бакалавр), бал: {Mark}";
        }
    }
}
