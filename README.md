#MediaRadar, Inc. Coding Test 

Ad Data Aggregation – Exercise Instructions

In this exercise you will write a simple web application that consumes a WCF (Windows Communication Foundation) service. This service will return some data that you will output to a webpage in various formats.

The service is available here: http://apps.mediaradar.com/AdDataService/AdService.svc and can be added to a Visual Studio project by adding a Service Reference to from Solution Explorer.

The AdDataClientService contains a method called GetAdDataByDateRange(Datetime startDate, Datetime endDate). This method will return a list of Ad objects contained in magazines published within the two dates specified.

You are to take all ad data run between January 1st, 2011 and April 1st, 2011 and display it in the following four ways:

1. A full list of the ads, including all object data (AdId, Brand Id and Name, NumPages, and Position). NumPages refers to how many pages the ad takes up in a magazine (0.5 would be a half-page ad) and Position is a string that is either “Page” or “Cover,” describing where in the magazine the ad ran. This list should be sortable and paged. You may use client-side or server-side code to accomplish this. Default sort should be by brand name alphabetically.

2. A list of ads which appeared in the “Cover” position and also had at least 50% page coverage. This list should also be sortable and paged, and sorted by brand name alphabetically.

3. The top five ads by page coverage amount, distinct by brand. Sort by page coverage amount (descending), then brand name alphabetically.

4. The top five brands by page coverage amount. Keep in mind that a single brand may run multiple ads. Also sorted by page coverage amount (descending), then brand name alphabetically.

You may use any resources you wish to accomplish this task – the project itself should be a C# MVC project. You are encouraged to utilize scripting libraries such as jQuery and any plugins that leverage these libraries.
