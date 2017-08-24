using FluentApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentApiModel
{
    public class Destination
    {
        public Guid DestinationId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
        public string Country
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public byte[] Photo
        {
            get;
            set;
        }
        public List<Lodging> Lodgings
        {
            get;
            set;
        }

        public Address Address
        {
            get;
            set;
        }

        public PersonalInfo Info
        {
            get;
            set;
        }
    }
}
