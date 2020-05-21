#language: en

Feature: Calculadora
Uma calculadora simples que tem apenas a operação de adição.

Scenario: Add two integer numbers
	Given the first int value is <value1>
	And the second int value is <value2>
	Then the int result should be <result>

	Examples:
    | value1     | value2 | result      |
    | 10         | 15     | 25          |
    | 45         | 1      | 46          |
    | -1         | -1     | -2          |
    | -10        | 10     | 0           |
    | 2147483647 | 1      | -2147483648 |

Scenario: Add two double numbers
	Given the first double value is <value1>
	And the second double value is <value2>
	Then the double result should be <result>

	Examples:
    | value1              | value2 | result              |
    | 10.56               | 15.47  | 26.03               |
    | 0.1                 | 0.01   | 0.11                |
    | 0                   | 0      | 0                   |
    | -55.48              | 50.47  | -5.01               |
    | 2147483647.67455434 | 1      | 2147483648.67455434 |

Scenario: Add two decimal numbers
	Given the first decimal value is <value1>
	And the second decimal value is <value2>
	Then the decimal result should be <result>
    
    Examples:
    | value1                        | value2 | result                        |
    | 10.56                         | 15.47  | 26.03                         |
    | 0.1                           | 0.01   | 0.11                          |
    | 0                             | 0      | 0                             |
    | -55.48                        | 50.47  | -5.01                         |
    | 79228162514264337593543950334 | 1      | 79228162514264337593543950335 |

Scenario: Concatenate two strings
	Given the first string value is <value1>
	And the second string value is <value2>
	Then the string result should be <result>
    
    Examples:
    | value1   | value2     | result           |
    | 'Nathan' | ' Lacerda' | 'Nathan Lacerda' |
    | 'Con'    | 'catenate' | 'Concatenate'    |
    | '12'     | '34'       | '1234'           |
    | 'ab'     | 'dc'       | 'abdc'           |
    | '"He'    | 'y!"'      | '"Hey!"'         |

Scenario: Try to add two bool
    Given the first bool is '<value1>'
    And the second bool is '<value2>'
    Then the bool result should be an exception
    
    Examples:
    | value1 | value2 |
    | true   | true   |
    | true   | false  |
    | false  | true   |
    | false  | false  |

Scenario: Try to add two date
    Given the first date is '<value1>'
    And the second date is '<value2>'
    Then the date result should be an exception
    
    Examples:
    | value1              | value2              |
    | 15/06/2009 13:45:30 | 01/05/1970 14:51:09 |
    | 12/05/1970 14:51:09 | 13/01/1970 04:12:49 |
    | 21/01/2001 00:21:59 | 30/03/1970 12:00:00 |
    | 12/05/1970 22:22:22 | 15/03/1970 02:01:09 |