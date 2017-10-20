using System;
namespace SDC
{
    public interface IDisplayedType
    {
        PropertyType AddFillProperty(Boolean fillData = true);
        LinkType AddFillLink(Boolean fillData = true);
        BlobType AddFillBlob(Boolean fillData = true);
        ContactType AddFillContact(Boolean fillData = true);
        CodingType AddFillCoding(Boolean fillData = true);
        WatchedPropertyType AddFillActivateIf(Boolean fillData = true);
        WatchedPropertyType AddFillDeActivateIf(Boolean fillData = true);
        IfThenType AddFillOnEnter(Boolean fillData = true);
        IfThenType AddFillOnEvent(Boolean fillData = true);
        OnEventType AddFillOnExit(Boolean fillData = true);

    }
}
