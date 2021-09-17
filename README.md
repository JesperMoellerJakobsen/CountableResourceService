# CountableResourceService

A microservice using restful HTTP to provide access to a countable resource.
Solves concurrency issues by using a threadsafe transactional approach with optimistic locking.

Hosted in a docker container on &lt;HOSTNAME&gt;:5001/counter

## Instructions for interacting with microservice

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
