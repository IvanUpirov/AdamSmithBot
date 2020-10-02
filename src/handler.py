# -*- coding: utf-8 -*-

from json import dumps, loads
from urllib3 import PoolManager
from datetime import date
from os import environ


def lambda_handler(event, _):
    bot_update = BotMessage(event['body'])
    message = bot_update.message
    curr_code = message['text'].replace('/', '')

    url = environ['url'].format(curr_code, date.today().strftime("%Y%m%d"))

    http = PoolManager()
    result = http.request('GET', url)
    curr_rate = CurrencyRate(result.data)

    return {
        'statusCode': 200,
        'body': dumps(
            {
                'method': 'sendMessage',
                'chat_id': message['chat']['id'],
                'text': 'Курс для {} на {}: {}'.format(curr_code, curr_rate.exchangedate, curr_rate.rate)
            })
    }


class BotMessage:
    def __init__(self, j):
        self.__dict__ = loads(j)


class CurrencyRate:
    def __init__(self, j):
        self.__dict__ = loads(j)[0]
