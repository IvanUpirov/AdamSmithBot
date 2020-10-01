import unittest
from handler import lambda_handler

class BasicTests(unittest.TestCase):

    def test_basic(self):
        event = {
            body: 'test'
        }
        
        
        result = lambda_handler(event, None)
        
        self.assertEqual(1, 1)

if __name__ == '__main__':
    unittest.main()