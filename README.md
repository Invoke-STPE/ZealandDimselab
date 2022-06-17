# ZealandDimselab
Exam Project for 2. semester created by [Steven Pedersen], [Christoper Jepsen], [Mikkel Meiling], [Oscar Hemmingsen].
I've forked the project, in order to have a project I can extend and improve.

The project was original intended to be implemented at our school Zealand Business College in Roskilde.
However due to our teacher getting a new offer at a school, the plans felt apart.

## Improvements added:
### Dependency Injection and AppSettings.json
For my self chosen learning path at school I decided to learn about Dependency Injection and AppSettings.json.
I implemented interfaces on my repository and service classes, so that razor pages no longer depended on the concrete implementation, I also refactored our DBContext to utilies a connectionString in appsettings.json rather than hardcoding it into our DContext class.

### Web API
Following TimCo Retail manager course, I decided that I wanted to implement the things I have learned there in this project aswell, first step was a major overhaul of how the project worked, added an API and class library project and moved code around.

[Steven Pedersen]: https://github.com/Invoke-STPE
[Christoper Jepsen]: https://github.com/ChristopherLoeve
[Mikkel Meiling]: https://github.com/mikkelm909
[Oscar Hemmingsen]: https://github.com/osca0339
