# SimpleApiBot
Wrote it intentional for icq, is pretty adaptable

Intention for this was to build a adaptable .net Api runner (bot) with these features
-easy implement complex structures (or avoid it)
-Serializeable Interfaces/abstracts to store, commands, responses, requests, whatever
-Make parts easy replaceable (well, atm I not splitted the assembly but it's coming)
-Write it without using Any stuff which is not .Net5 (to keep option for using it on other platforms)
-Create own certs to use Https by wanted standard

It's nothing special but, I like it :)

There are a lot naming issues and so on but it's production code so I am "Pretty Sowwy"

What works
Serialize and Deserialization of Commands and Responses -> maps to own implementable ENUM System

Todo:
A lot, I wrote a lot code in the implementation of the single api "adapters" I am focused on refactoring it and make everything always more generic and dynamically
And by the way a automatic generation of enums and maybe later commands is planned + ready to use Cronjob features.