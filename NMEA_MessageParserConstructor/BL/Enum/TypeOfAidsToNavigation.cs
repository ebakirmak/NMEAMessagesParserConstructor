using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.AnnexClasses
{
    public enum TypeOfAidsToNavigation
    {
        Default  = 0,
        ReferencePoint = 1,
        RACON = 2,
        OffShore = 3,
        Emergency = 4,
        LightWithoutSectors = 5,
        LightWithSectors = 6,
        LeadingLightFront = 7,
        LeadingLightRear = 8,
        BeaconCardinalN = 9,
        BeaconCardinalE = 10,
        BeaconCardinalS = 11,
        BeaconCardinalW = 12,
        BeaconPortHand = 13,
        BeaconStarboardHand = 14,
        BeaconPreferredChannelPortHand = 15,
        BeaconPreferredChannelStarboardHand = 16,
        BeaconIsolatedDanger = 17,
        BeaconSafeWater = 18,
        BeaconSpeacilMark = 19,
        CardinalMarkN = 20,
        CardinalMarkE = 21,
        CardinalMarkS = 22,
        CArdinalMarkW = 23,
        PortHandMark = 24,
        StarboardHandMark = 25,
        PreferredChannelPortHand = 26,
        PreferredChannelStarboardHand = 27,
        IsolatedDanger = 28,
        SafeWater = 29,
        SpecialMark = 30,
        LightVessel = 31


    }
}
