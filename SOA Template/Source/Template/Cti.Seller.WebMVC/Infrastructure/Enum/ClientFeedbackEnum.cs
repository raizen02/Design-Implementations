using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ecrm.Infrastructure.Enum
{
    public enum ClientFeedbackEnum
    {
        ConfirmedSchedule = 1,
        ForBooking = 2,
        Interested = 3,
        NotDecided = 4,
        NotInterested = 5,
        OutOfTown = 6,
        RescheduledAppointment = 7,
        ReviewingPaymentTerms = 8,
        UnableToContact = 9
    }
}