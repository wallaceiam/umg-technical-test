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

The date formatting and parsing seems really unneccessary and I'm not sure what skill it is actually testing from the candidate.  Using YYYY-MM-dd would greatly simplify this test.

Similar to the dates is the command input - why can't this be parameterized? ie, 
```
dotnet run -partner ITunes -effective 2012-03-01
```

There is a lot, IMHO, of unnecessary code and tests that has had to be writen to handle just these two things where in the real world a quick conversation could have avoid this. 

Because this is a small data set, I have not 


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