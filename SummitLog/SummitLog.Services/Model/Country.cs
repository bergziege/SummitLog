using System;
using System.Collections.Generic;

namespace SummitLog.Services.Model
{
    public class Country
    {
        private long _id;
        private string _name;

        public long Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}