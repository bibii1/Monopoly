using System;
namespace Monopoly
{
    public abstract class Case
    {
        string name;

        protected Case(string name)
        {
            this.name = name;
        }

        public string Name { get => name; set => name = value; }
    }
}
