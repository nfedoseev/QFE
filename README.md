# QFE

![Screen of app](http://i.imgur.com/Z0Djuji.png)

QFE - Application for help to virual atc. Provide some helpful information.

##Current data
* ICAO
* Wind direction
* Wind speed 
* Wind gusts
* TL(not for all airports)
* QNH
* QFE

##API
This app use API of http://ivao.daitel.net
#####Example

Request:
```
http://ivao.daitel.net/api/qfe/UUEE
```
Response:
```
{"icao":"UUEE","time":"310000Z","wind":"240","value":"04","tl":"FL60","qnh":"1007","qfe":984}
```
