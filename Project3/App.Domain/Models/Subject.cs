namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Subject
    {
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public WordInfo[] Details { get; set; }

        public override string ToString() => Name;
    }
}
