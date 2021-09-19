# CountableResourceService

A microservice exposing a restful HTTP endpoint which provides access to a countable resource.  
Solves concurrency issues by using a threadsafe approach with optimistic locking.

## Cluster setup
![Cluster setup](https://github.com/JesperMoellerJakobsen/CountableSwarm/blob/master/ArchitectureDiagram.png)

## Instructions for interacting with CountableResourceService
Hosted in a docker container on &lt;HOSTNAME&gt;:5001/counter

Read counter:
```
GET <HOSTNAME>:5001/counter
```

Increment counter:
```json
PATCH <HOSTNAME>:5001/counter 
{
    "version": "AAAAAAAAB9I=",
    "patchoption": 0
}
```

Decrement counter:
```json
PATCH <HOSTNAME>:5001/counter
{
    "version": "AAAAAAAAB9I=",
    "patchoption": 1
}
```
