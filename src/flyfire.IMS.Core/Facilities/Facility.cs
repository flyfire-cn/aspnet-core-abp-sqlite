using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Text;

namespace flyfire.IMS.Facilities
{
    public class Facility
    {
        public int FacilityId { get; set; }

        public string FacilityName { get; set; }

        public Geometry Location { get; set; }

        public string  LocationDescription { get; set; }

    }
}
