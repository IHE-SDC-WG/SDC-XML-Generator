using System;
namespace SDC
{
    public interface IDisplayedType
    {
        PropertyType AddProperty(Boolean fillData = true);
        LinkType AddLink(Boolean fillData = true);
        BlobType AddBlob(Boolean fillData = true);
        ContactType AddContact(Boolean fillData = true);
        CodingType AddCoding(Boolean fillData = true);
        WatchedPropertyType AddActivateIf(Boolean fillData = true);
        WatchedPropertyType AddDeActivateIf(Boolean fillData = true);
        IfThenType AddOnEnter(Boolean fillData = true);
        IfThenType AddOnEvent(Boolean fillData = true);
        OnEventType AddOnExit(Boolean fillData = true);

    }
}
