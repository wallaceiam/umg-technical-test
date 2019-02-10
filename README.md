# Universal Music Group Technical Test

## Build and Run

You will require .NET Core 2.2 SDK installed in order to run and test this submission
https://dotnet.microsoft.com/download/dotnet-core/2.2

```
git clone https://github.com/wallaceiam/RecklassRekkids.GlobalRightsManagement.git
cd RecklassRekkids.GlobalRightsManagement
dotnet build
```

Running the Console application
```
cd RecklassRekkids.GlobalRightsManagement
dotnet run "ITunes 1st March 2012"
```

Running the tests
```
cd RecklassRekkids.GlobalRightsManagement.Tests
dotnet test
```

## General comments and findings

I have tried to keep this console application as minimal as possible without violating SOLID/DRY principals and still fulfilling the requirements.

Application Structure

+ Data            - Static reference data provided by the client

+ Extensions      - Helper methods for Strings and DateTime

+ Filters         - Used to build the search query

+ Models          - POCOs for Music and Distribution Partner Contracts

+ Repositories    - Data access & UI

+ Services        - Where the filter magic happens

I have also obmitted logging and full on error handling in favour of brevity.

I have not made the ProductFilter/ProductFilterBuilder/ProductSearchService as decoupled as I would have liked. This is mainly due to the fact that I tried to keep within the 2-3 hours and don't have requirements for all the ways in which the data could be searched/filtered so I didn't want to over engineer a solution. If application was to be expanded upon with additional criteria than this is one area I would want to focus more on.  (for example, Specification pattern seemed unnecessary and overkill at this time).

Because this is a small data set, I have not looked at optimizing the search but rather opted for readability.

Test have been completed using xUnit with xbehave.net, Moq and fluent assertions.

I have not provided tests for the Repositories as a) they are simple wrappers around third-party libraries and b) there is no business logic contained within. I would have liked to use async/await with the file IO but as CsvHelper doesn't require it to retrieve all the records and again due to the small data set - I have obmitted it. It this application was connected to a SQL/noSQL/REST endpoint or some other data store than I would have definitely used async methods to retrieve the data.

The date formatting and parsing seems really unneccessary and I'm not sure what skill it is actually testing but a recommendation would be to use a standard format like YYYY-MM-dd in order to greatly simplify this test.

Similar to the dates is the command input - I would recommend this be parameterized? ie, 
```
dotnet run -partner ITunes -effective 2012-03-01
```

There is a lot, IMHO, of unnecessary code and tests that has had to be writen to handle just these two things where in the real world a quick conversation could have avoid this. Talk is cheat, code is expensive... :-)

I have assumed all dates are in the local timezone from where the application is run.


## Provided Specification
```
GRM Developer test 
 
This should be done by the candidate over the course of ​2-3​ hours and delivered to us before we conduct the onsite or phone interview. 
 
[Coding Problem Description] 
 
A music label called Recklass Rekkids (aka RR) wants to build a Global Rights Management (aka GRM) platform to allow them to best utilise their collection of music assets. 
 
There are legal limitations in the ways in which RR can use the assets based on the contract signed with the artist. For example Monkey Claw agreed with RR to distribute his new song 'Motor Mouth' as a digital download starting 1st of feb 2012, and as a streaming product starting from the 1st of march. 
 
Agreements with distribution partners also have limitations. For example iTunes will only sell assets as digital downloads, while YouTube will only sell them as streaming products. 
 
[Task] 
 
Create a console application that determines products available for a given partner on a given date.  
 
The application should accept the reference data supplied below as text file inputs.  
 
The user will then supply a delivery partner name and an effective date as command line inputs.  
 
The output should describe the current active music contracts as applicable to the partner. 
 
[Submission checklist] 
 
- visual studio solution containing 
- executable application 
- source code 
- unit tests 
 
[Text File Input #1] 
 
Music Contracts 
 
Artist|Title|Usages|StartDate|EndDate 
Tinie Tempah|Frisky (Live from SoHo)|digital download, streaming|1st Feb 2012| 
Tinie Tempah|Miami 2 Ibiza|digital download|1st Feb 2012| 
Tinie Tempah|Till I'm Gone|digital download|1st Aug 2012| 
Monkey Claw|Black Mountain|digital download|1st Feb 2012| 
Monkey Claw|Iron Horse|digital download, streaming|1st June 2012| 
Monkey Claw|Motor Mouth|digital download, streaming|1st Mar 2011| 
Monkey Claw|Christmas Special|streaming|25st Dec 2012|31st Dec 2012 
 
[Text File Input #2] 
 
Distribution Partner Contracts 
 
Partner|Usage 
ITunes|digital download 
YouTube|streaming 
 
[Test scenarios] 
 
1. 
Given the supplied above reference data 
When user enters 'ITunes 1st March 2012' 
Then the output is: 
 
Artist|Title|Usage|StartDate|EndDate 
Monkey Claw|Black Mountain|digital download|1st Feb 2012| 
Monkey Claw|Motor Mouth|digital download|1st Mar 2011| 
Tinie Tempah|Frisky (Live from SoHo)|digital download|1st Feb 2012| 
Tinie Tempah|Miami 2 Ibiza|digital download|1st Feb 2012| 
 
2.  
 
Given the supplied above reference data 
When user enters 'YouTube 1st April 2012' 
Then the output is: 
 
Artist|Title|Usage|StartDate|EndDate 
Monkey Claw|Motor Mouth|streaming|1st Mar 2011| 
Tinie Tempah|Frisky (Live from SoHo)|streaming|1st Feb 2012| 
 
3.  
 
Given the supplied above reference data 
When user enters 'YouTube 27th Dec 2012' 
Then the output is: 
 
Artist|Title|Usage|StartDate|EndDate 
Monkey Claw|Christmas Special|streaming|25st Dec 2012|31st Dec 2012 Monkey Claw|Iron Horse|streaming|1st June 2012| 
Monkey Claw|Motor Mouth|streaming|1st Mar 2011| 
Tinie Tempah|Frisky (Live from SoHo)|streaming|1st Feb 2012| 
```