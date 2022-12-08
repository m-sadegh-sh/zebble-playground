namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Category
    {
        public string Name { get; set; }
        public Subject[] Subjects { get; set; }

        public override string ToString() => Name;
    }
}
