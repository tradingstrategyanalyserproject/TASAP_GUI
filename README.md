# TASAP Project

This project is a C# project used to provide a WPF pricer front end communicating with the python part (in tradingstrategyanalyserproject).


## GUI Project
The idea is to create an option pricer capable to print curves according to the following parameters : 
* Option's type : call/put
* Spot price
* Strike price 
* Time to maturity
* Risk Free Rate
* And the volatility

Once we selected all of those paramters from the interface, the COM project arrives to make his work ...

## COM Project
This project is mainly here to ensure the good communication and handling of the JSON files genertated from the python part. 
A few steps to understand : 
* A http request is build from the front end and then get a JSON file
* Json informations are then stored in a dictionnary we will use to construct the curves. 

*To be continued ...*


NB : it is obvious before launching this project, you need to get the python project, run flask server as show in .py file and then use the C# project.
