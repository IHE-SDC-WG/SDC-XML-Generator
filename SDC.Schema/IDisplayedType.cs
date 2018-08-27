using System;
namespace SDC.Schema
{
    public interface IDisplayedType
    {
        PropertyType AddProperty(Boolean fillData = true);
        LinkType AddLink(Boolean fillData = true);
        BlobType AddBlob(Boolean fillData = true);
        ContactType AddContact(Boolean fillData = true);
        CodingType AddCoding(Boolean fillData = true);
        GuardType AddActivateIf(Boolean fillData = true);
        GuardType AddDeActivateIf(Boolean fillData = true);
        EventType AddOnEnter(Boolean fillData = true);
        OnEventType AddOnEvent(Boolean fillData = true);
        EventType AddOnExit(Boolean fillData = true);

    }
}
