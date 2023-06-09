﻿namespace Space.Client.Forms.Attributes
{
    public class QueryAttribute : Attribute
    {
        private string _name;

        public QueryAttribute(string name)
        {
            _name = name.ToLower();
        }

        public string Name { get { return _name; } }
    }
}
