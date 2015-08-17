# QFE

[![GitHub license](https://img.shields.io/github/license/daitel/qfe.svg)](https://github.com/daitel/qfe/blob/master/LICENSE.md)
[![Issue](https://img.shields.io/github/issues-raw/daitel/qfe.svg)](https://github.com/daitel/qfe/issues)

QFE - Application for help with QFE. Provide some helpful information.

![Screen of app](http://i.imgur.com/Z0Djuji.png)

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
