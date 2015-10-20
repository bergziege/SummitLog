using System;
using System.Collections.Generic;

namespace SummitLog.Services.Model
{
    public class SummitGroup
    {
        private Guid _id = Guid.NewGuid();
        private string _name;

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}