using System;

namespace InterchangeWFA.Objects
{
    public interface IGenericObject
    {
        byte[] Bytes { get; }
        int DeviceID { get; }
    }
}
