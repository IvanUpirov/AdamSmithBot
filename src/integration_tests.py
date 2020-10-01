import unittest
from handler import lambda_handler
from os import environ


class BasicTests(unittest.TestCase):

    def setUp(self):
        environ['url'] = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?valcode={}&date={}&json"

    def tearDown(self):
        del environ['url']

    def test_end_to_end(self):
        event = {
            'body': '{"message":{"chat":{"id":168407544},"text":"/usd"}}',
        }

        result = lambda_handler(event, None)

        self.assertEqual(result['statusCode'], 200)


if __name__ == '__main__':
    unittest.main()
