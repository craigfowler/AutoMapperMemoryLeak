# AutoMapper memory leak tester app

This is a tiny PoC app which was intended to determine whether or not the AutoMapper library has a memory leak issue when many `MapperConfiguration` instances are created.
In the end, it's failed to prove the case, and it seems there is no memory leak.

I have tried this with multiple AutoMapper versions from 8.0.0 through 11.0.1 and I get fairly consistent results.
Here are some indicative results from my system.
As can be seen, the RAM usage stabilises and does not appear to be markedly growing.

```txt
About to create config and map over 1 iterations
  Current process bytes used = 16138240
Completed creating config and mapping over 1 iterations
  Current process bytes used = 29478912
  Process bytes growth = 13340672

About to create config and map over 10 iterations
  Current process bytes used = 29491200
Completed creating config and mapping over 10 iterations
  Current process bytes used = 34955264
  Process bytes growth = 5464064

About to create config and map over 100 iterations
  Current process bytes used = 34955264
Completed creating config and mapping over 100 iterations
  Current process bytes used = 35688448
  Process bytes growth = 733184

About to create config and map over 1000 iterations
  Current process bytes used = 35688448
Completed creating config and mapping over 1000 iterations
  Current process bytes used = 34488320
  Process bytes growth = -1200128

About to create config and map over 10000 iterations
  Current process bytes used = 35069952
Completed creating config and mapping over 10000 iterations
  Current process bytes used = 35127296
  Process bytes growth = 57344
```