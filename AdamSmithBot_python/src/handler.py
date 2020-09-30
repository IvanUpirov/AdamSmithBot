from json import dumps, loads
import urllib3
from datetime import date
import os


def lambda_handler(event, context):
    bot_update = BotMessage(event['body'])
    message = bot_update.message
    chat_id = message['chat']['id']
    curr_code = message['text'].replace('/', '')

    url = os.environ['url'].format(curr_code, date.today().strftime("%Y%m%d"))

    http = urllib3.PoolManager()
    result = http.request('GET', url)
    curr_rate = CurrencyRate(result.data)

    return {
        'statusCode': 200,
        'body': dumps(
            {
                'method': 'sendMessage',
                'chat_id': chat_id,
                'text': f'Курс для {curr_code} на {curr_rate.exchangedate}: {curr_rate.rate}'
            })
    }


class BotMessage:
    def __init__(self, j):
        self.__dict__ = loads(j)


class CurrencyRate:
    def __init__(self, j):
        self.__dict__ = loads(j)[0]
