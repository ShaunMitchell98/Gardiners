using System;

namespace Gardiners.Core.Models;

[Flags]
public enum AllowedDeliveryModes
{
    Unknown = 0,
    FastTrack = 1 << 0,
    Standard = 1 << 1,
    Express = 1 << 2,
    PremiumAllowedDeliveryModes = FastTrack | Express,
    StandardAllowedDeliveryModes = Standard
}
