﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentApiModel
{
    public class Lodging
    {
        public Guid LodgingId
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string Owner
        {
            get;
            set;
        }
        public bool IsResort
        {
            get;
            set;
        }
        public Destination Destination
        {
            get;
            set;
        }
    }
}
