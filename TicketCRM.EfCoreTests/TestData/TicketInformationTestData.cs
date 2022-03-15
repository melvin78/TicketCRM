using System.Collections;
using System.Collections.Generic;

namespace TicketCRM.EfCoreTests.TestData;

public class TicketInformationTestData:IEnumerable<object[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
        /*
         *TicketNo
         *FirstMessage
         *CareTaker
         *TicketStatusId
         *CustomerId
         *EnquiryCategoryId
         *EnquiryId
         *SaccoId
         *Attachments
         *Remarks
         *ClosedOn
         *ResolvedOn
         *PriorityLevel
         *ReopenedOn
         *CancelledOn
         *TransferredOn
         *AssignedOn
         *PastDueDate
         *ExpectedDueDate
         */
        yield return new object?[]
        {
            "CRM-2FC4-040257-174E",
            "Hello,I am just checking to see if the tests are running :)",
            "debfbd47-ffc8-478c-8f2e-4e98af9ba944",
            "0e06e836-471c-ec11-b063-14cb19ba19a9",
            "6aa1b670-2941-4f26-955a-7f6c236c8b3e",
            "b7020354-0009-4268-8473-ac95d0e4815b",
            "b01e2bad-e787-4777-a2f6-8219e609c503",
            "2fc42189-9497-4638-b5de-22ce8d46c595",
            "[{\"filename\":\"6db9c66b-86ea-4d18-8e73-d0b35c66bfb7-9056\",\"type\":\"application/pdf\"}]",
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null
        };
        yield return new object?[]
        {
            "CRM-2FC4-040257-174E",
            "Hello,I am just checking to see if the tests are running :)",
            "debfbd47-ffc8-478c-8f2e-4e98af9ba944",
            "0e06e836-471c-ec11-b063-14cb19ba19a9",
            "6aa1b670-2941-4f26-955a-7f6c236c8b3e",
            "b7020354-0009-4268-8473-ac95d0e4815b",
            "b01e2bad-e787-4777-a2f6-8219e609c503",
            "2fc42189-9497-4638-b5de-22ce8d46c595",
            "[{\"filename\":\"6db9c66b-86ea-4d18-8e73-d0b35c66bfb7-9056\",\"type\":\"application/pdf\"}]",
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null
            
        };
        yield return new object?[] { 
            "CRM-2FC4-040257-174E",
            "Hello,I am just checking to see if the tests are running :)",
            "debfbd47-ffc8-478c-8f2e-4e98af9ba944",
            "0e06e836-471c-ec11-b063-14cb19ba19a9",
            "6aa1b670-2941-4f26-955a-7f6c236c8b3e",
            "b7020354-0009-4268-8473-ac95d0e4815b",
            "b01e2bad-e787-4777-a2f6-8219e609c503",
            "2fc42189-9497-4638-b5de-22ce8d46c595",
            "[{\"filename\":\"6db9c66b-86ea-4d18-8e73-d0b35c66bfb7-9056\",\"type\":\"application/pdf\"}]",
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null};
        yield return new object?[]
        {
            "CRM-2FC4-040257-174E",
            "Hello,I am just checking to see if the tests are running :)",
            "debfbd47-ffc8-478c-8f2e-4e98af9ba944",
            "0e06e836-471c-ec11-b063-14cb19ba19a9",
            "6aa1b670-2941-4f26-955a-7f6c236c8b3e",
            "b7020354-0009-4268-8473-ac95d0e4815b",
            "b01e2bad-e787-4777-a2f6-8219e609c503",
            "2fc42189-9497-4638-b5de-22ce8d46c595",
            "[{\"filename\":\"6db9c66b-86ea-4d18-8e73-d0b35c66bfb7-9056\",\"type\":\"application/pdf\"}]",
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}