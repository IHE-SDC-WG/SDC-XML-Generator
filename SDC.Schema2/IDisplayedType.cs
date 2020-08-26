using System;
namespace SDC.Schema2
{
    public interface IDisplayedType
    {
        LinkType AddLink();
        BlobType AddBlob();
        ContactType AddContact();
        CodingType AddCoding();
        PredGuardType AddActivateIf();
        PredGuardType AddDeActivateIf();
        EventType AddOnEnter();
        OnEventType AddOnEvent();
        EventType AddOnExit();

        //Convert to LI (if inside a List)
        //Convert to LIR (if inside a List)
        //Move (to ChildItems or List node, index for postition)
        //Can Move
        //under ChildItems - Convert to: QS/QM/QR, S
        //under List: Convert to LI/LIR

    }
}
