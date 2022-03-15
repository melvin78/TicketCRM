using System.Collections;
using System.Collections.Generic;

namespace TicketCRM.UnitTest.TestData;

public class TicketNumberTestData:IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        //first param is enquirycategoryId
        //second param is organizationId
        yield return new object[] { "6a693e84-b5d6-4e22-bfc1-3198cc6e4e1c", "7603b8ab-9e03-4fc1-873c-7c60614592f7","CRM-7603-082142-6A69"};
        yield return new object[] { "5f384c8d-01f5-43d6-9f87-39e2067764ad","16a833f2-c2ab-437e-af88-4f79db823ef9" ,"CRM-16A8-034452-5F38"};
        yield return new object[] { "7d03e683-42c0-4421-acdc-5b2614af7737", "8c66893b-4f4c-4950-b31b-839875d05b40","CRM-8C66-031452-7D03"};
        yield return new object[] { "3cd22bae-a716-4a2e-91c7-c3de5b827d92", "f00e1f85-4eae-41c4-8791-a2c9dcf209ad","CRM-F00E-036452-3CD2"};
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}