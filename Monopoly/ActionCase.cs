using System;
namespace Monopoly
{
    public class ActionCase : Case
    {
        string action;

        public ActionCase(string name, string action): base(name)
        {
            this.action = action;
        }

        public string Action { get => action; set => action = value; }
    }
}
