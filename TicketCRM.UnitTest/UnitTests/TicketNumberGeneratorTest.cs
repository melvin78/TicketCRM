using System;
using System.Linq;
using System.Text.RegularExpressions;
using TicketCRM.Infrastructure.Utilities.Utils;
using TicketCRM.UnitTest.TestData;
using Xunit;

namespace TicketCRM.UnitTest.UnitTests;

public class TicketNumberGeneratorTest
{
  [Theory]
  [ClassData(typeof(TicketNumberTestData))]
  public void Correct_Number_of_Characters_In_Ticket_Number_Generated(Guid enquiryCategoryId,Guid organizationId,string expected)
  {
    var result = TicketNumberGenerator.GenerateTicketNumber(enquiryCategoryId,organizationId);

    Assert.Equal(expected.Length==20,result.Length==20);
    
    
  }
  
  [Theory]
  [ClassData(typeof(TicketNumberTestData))]
  public void First_Four_Characters_In_Ticket_Number_Generated(Guid enquiryCategoryId,Guid organizationId,string expected)
  {
    var result = TicketNumberGenerator.GenerateTicketNumber(enquiryCategoryId,organizationId);

    string firstFourCharacters = new string(result.ToUpper().Skip(3).Take(4).ToArray());

    Assert.Equal(new string(expected.ToUpper().Skip(3).Take(4).ToArray()),firstFourCharacters);
    
    
  }
  
  [Theory]
  [ClassData(typeof(TicketNumberTestData))]
  public void Last_Four_Characters_In_Ticket_Number_Generated(Guid enquiryCategoryId,Guid organizationId,string expected)
  {
    var result = TicketNumberGenerator.GenerateTicketNumber(enquiryCategoryId,organizationId);

    string lastFourCharacters = new string(result.ToUpper().Skip(15).Take(4).ToArray());

    Assert.Equal(new string(expected.ToUpper().Skip(15).Take(4).ToArray()),lastFourCharacters);
    
    
  }
  
  [Theory]
  [ClassData(typeof(TicketNumberTestData))]
  public void Six_Random_Digits_Were_Generated(Guid enquiryCategoryId,Guid organizationId,string expected)
  {
    var result = TicketNumberGenerator.GenerateTicketNumber(enquiryCategoryId,organizationId);

    int lastFourCharacters = new string(result.ToUpper().Skip(8).Take(6).ToArray()).Length;

    Assert.Equal(new string(expected.ToUpper().Skip(8).Take(6).ToArray()).Length,lastFourCharacters);
    
    
  }
}