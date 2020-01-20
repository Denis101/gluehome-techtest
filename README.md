# GlueHome Tech Test - Denis Craig (January 2020)

**NOTE** This app was developed on a macOS, it is not tested nor guaranteed to work as-is on a Windows machine. It should, however, work in a Linux environment so having docker & docker-compose installed on a VM will work.

## Installation

### Prerequisites

* `docker` & `docker-compose`
* .net core 3.1 sdk

### Running the API

`docker-compose up`

API is accessible at `localhost:8080`, you can navigate the available resources on the api at `localhost:8080/swagger`

### Running the test suite

`dotnet test`

## Usage

Authentication uses the Basic `Authorization` HTTP header, `<username>:<password>` as base64 encoded text.

Example request:
```curl -H "Authorization: $(echo -n 'user@gluehome.com:test' | openssl base64 | awk '{ print "Basic "$1 }')" http://localhost:8080/api/delivery```


## Additional considerations/assumptions/improvements

* All delivery windows are assumed to be in UTC date format.
* Passwords are stored as plaintext. Obviously in the real world, we would never ever ever ever want to do this. If I were to productionise this further, hashing the password would be a critical first step.
* Users are hard-coded in `scripts/mysql/init/01-users.sql`, and there is no code-path to create new users. For the sake of time, I just wanted to implement simple authentication to endpoints.
* Didn't have time to implement pub/sub or streaming APIs. I'm living under the assumption that we can have a lot of changing states within a given delivery time window, so having some event-based processing would be nice for this.
* Using nginx as a reverse proxy (due to insecure kestrel web server). With more time, I would likely terminate TLS here. I could also decide to terminate at a load balancer upstream depending on how concerned about security I might be. There are cases where we might even want over-the-wire encryption internally in our network.
* No transactional queries when running queries in quick succession. Would like to figure out a way to implement that.
* We're storing PII without asking for opt-in nor notifying customers, very naughty. GDPR no-no.
* Some sort of CI pipeline, that will create the build artifacts, run unit & integration tests, build docker containers and then deploy to an environment. I'm just using docker-compose here, but it could effectively be any container orchestration platform (e.g. apply k8s manifests)
* Have methods on repositories to get all rows from a MySQL table. Might not be so great when there are 1mil+ rows. (pagination)
* MySQL query memoization or something similar, to stop multiple reads for the same row when there have been no writes in between.
