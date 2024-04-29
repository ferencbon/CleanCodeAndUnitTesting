import { Calculator } from "../src/calculator"

describe('Calculator tests', () => {

    let calculator: Calculator;

    beforeEach(() => {
        calculator = new Calculator();
    });

    describe('Add tests', () => {
        test('Given two positive numbers When adding them Then the result should be their sum', () => {
            //Arrange
            const a = 5;
            const b = 3;
            const expected = 8;
            //Act
            const actual = calculator.Add(a, b);
            //Assert
            expect(actual).toBe(expected);
        });
        test('Given two numbers When adding Then the result should be the same ', () => {
            //Arrange
            const a = 3;
            const b = 5;
            //Act
            const result1 = calculator.Add(a, b);
            const result2 = calculator.Add(b, a);
            //Assert
            expect(result1).toBe(result2);
        });
        test('Given a positive and a negative number When adding Then the result should be their sum', () => {
            //Arrange
            const a = -2;
            const b = 3;
            const expected = 1;
            //Act
            const actual = calculator.Add(a, b);
            //Assert
            expect(actual).toBe(expected);
        });
        test('Given two floating-point numbers When adding Then the result should be their sum', () => {
            //Arrange
            const a = 2.5;
            const b = 3.1;
            const expected = 5.6;
            //Act
            const actual = calculator.Add(a, b);
            //Assert
            expect(actual).toBeCloseTo(expected);
        });
        test('Given two zeros When adding Then the result should be zero', () => {
            //Arrange
            const a = 0;
            const b = 0;
            const expected = 0;
            //Act
            const actual = calculator.Add(a, b);
            //Assert
            expect(actual).toBe(expected);
        });
        test('Given two negative numbers When adding Then the result should be their sum', () => {
            //Arrange
            const a = -5;
            const b = -3;
            const expected = -8;
            //Act
            const actual = calculator.Add(a, b);
            //Assert
            expect(actual).toBe(expected);
        });
    });

    describe('Subtract Tests', () => {
        test('Given two numbers When subtracting Then the result should be their difference', () => {
            //Arrange
            const a = 10;
            const b = 5;
            const expected = 5;
            //Act
            const actual = calculator.Subtract(a, b);
            //Assert
            expect(actual).toBe(expected);
        });
        test('Given a smaller number from a larger When subtracting Then the result should be negative', () => {
            //Arrange
            const a = 5;
            const b = 10;
            const expected = -5;
            //Act
            const actual = calculator.Subtract(a, b);
            //Assert
            expect(actual).toBe(expected);
        });
        test('Given two floating-point numbers When subtracting Then the result should be their difference', () => {
            // Arrange
            const a = 5.5;
            const b = 3.2;
            const expected = 2.3;
            // Act
            const actual = calculator.Subtract(a, b);
            // Assert
            expect(actual).toBeCloseTo(expected);
        });
        test('Given zero as the first number and a positive number as the second When subtracting Then the result should be negative', () => {
            // Arrange
            const a = 0;
            const b = 5;
            const expected = -5;
            // Act
            const actual = calculator.Subtract(a, b);
            // Assert
            expect(actual).toBe(expected);
        });
        test('Given a positive number as the first number and zero as the second When subtracting Then the result should be the first number', () => {
            // Arrange
            const a = 10;
            const b = 0;
            const expected = 10;
            // Act
            const actual = calculator.Subtract(a, b);
            // Assert
            expect(actual).toBe(expected);
        });
        test('Given two negative numbers When subtracting Then the result should be their difference', () => {
            // Arrange
            const a = -10;
            const b = -5;
            const expected = -5;
            // Act
            const actual = calculator.Subtract(a, b);
            // Assert
            expect(actual).toBe(expected);
        });
    });

    describe('Multiply tests', () => {
        test('Given two positive integers When multiplying Then the result should be their product', () => {
            // Arrange
            const a = 5;
            const b = 4;
            const expected = 20;
            // Act
            const actual = calculator.Multiply(a, b);
            // Assert
            expect(actual).toBe(expected);
        });
        test('Given a negative and a positive integer When multiplying Then the result should be negative', () => {
            // Arrange
            const a = -5;
            const b = 4;
            const expected = -20;
            // Act
            const actual = calculator.Multiply(a, b);
            // Assert
            expect(actual).toBe(expected);
        });
        test('Given a number and zero When multiplying Then the result should be zero', () => {
            // Arrange
            const a = 10;
            const b = 0;
            const expected = 0;
            // Act
            const actual = calculator.Multiply(a, b);
            // Assert
            expect(actual).toBe(expected);
        });
        test('Given two floating-point numbers When multiplying Then the result should be their product', () => {
            // Arrange
            const a = 5.5;
            const b = 2.0;
            const expected = 11.0;
            // Act
            const actual = calculator.Multiply(a, b);
            // Assert
            expect(actual).toBeCloseTo(expected);
        });
        test('Given two numbers When multiplying Then the result should be the same regardless of the order', () => {
            // Arrange
            const a = 7;
            const b = 3;
            // Act
            const result1 = calculator.Multiply(a, b);
            const result2 = calculator.Multiply(b, a);
            // Assert
            expect(result1).toBe(result2);
        });
    });

    describe('Divide tests', () => {
        test('Given two numbers When dividing Then the result should be their quotient', () => {
            //Arrange
            const a = 10;
            const b = 2;
            const expected = 5;
            //Act
            const actual = calculator.Divide(a, b);
            //Assert
            expect(actual).toBe(expected);
        });
        test('Given two negative numbers When dividing Then the result should be their positive quotient', () => {
            // Arrange
            const a = -10;
            const b = -2;
            const expected = 5;
            // Act
            const actual = calculator.Divide(a, b);
            // Assert
            expect(actual).toBe(expected);
        });
        test('Given two floating-point numbers When dividing Then the result should be their quotient', () => {
            // Arrange
            const a = 5.5;
            const b = 2.2;
            const expected = 2.5;
            // Act
            const actual = calculator.Divide(a, b);
            // Assert
            expect(actual).toBeCloseTo(expected);
        });
        test('Given a dividend of zero and a non-zero divisor When dividing Then the result should be zero', () => {
            // Arrange
            const a = 0;
            const b = 5;
            const expected = 0;
            // Act
            const actual = calculator.Divide(a, b);
            // Assert
            expect(actual).toBe(expected);
        });
        test('Given a negative dividend and a negative divisor When dividing Then the result should be a positive quotient', () => {
            // Arrange
            const a = -10;
            const b = -5;
            const expected = 2;
            // Act
            const actual = calculator.Divide(a, b);
            // Assert
            expect(actual).toBe(expected);
        });
        test('Given a divisor greater than the dividend When dividing Then the result should be less than one', () => {
            // Arrange
            const a = 5;
            const b = 10;
            const expected = 0.5;
            // Act
            const actual = calculator.Divide(a, b);
            // Assert
            expect(actual).toBeCloseTo(expected);
        });
        test('Given a divisor of zero When dividing Then it throws Division by zero', () => {
            //Arrange
            const a = 10;
            const b = 0;

            //Assert
            expect(() => calculator.Divide(a, b)).toThrow('Division by zero is not allowed.');
        });
    });

    describe('Sqrt tests', () => {
        test('Given a positive number When calculating square root Then the result should be its square root', () => {
            //Arrange
            const a = 16;
            const expected = 4;
            //Act
            const actual = calculator.Sqrt(16);
            //Assert
            expect(actual).toBe(expected);
        });
        test('Given zero When calculating square root Then the result should be zero', () => {
            // Arrange
            const a = 0;
            const expected = 0;
            // Act
            const actual = calculator.Sqrt(a);
            // Assert
            expect(actual).toBe(expected);
        });
        test('Given a positive floating-point number When calculating square root Then the result should be its square root', () => {
            // Arrange
            const a = 18.49;
            const expected = 4.3;
            // Act
            const actual = calculator.Sqrt(a);
            // Assert
            expect(actual).toBeCloseTo(expected);
        });
        test('Given a negative number When calculating square root Then it throws Square root of negative number', () => {
            //Arrange
            const a = -1;

            //Assert
            expect(() => calculator.Sqrt(a)).toThrow('Square root of a negative number is not allowed.');
        });
    });

    describe('Power tests', () => {
        test('Given a base and an exponent When calculating power Then the result should be the base raised to the exponent', () => {
            //Arrange
            const a = 2;
            const b = 3;
            const expected = 8;
            //Act
            const actual = calculator.Power(a, b);
            //Assert
            expect(actual).toBe(expected);
        });
        test('Given a base and an exponent of zero When calculating power Then the result should be one', () => {
            // Arrange
            const a = 5;
            const b = 0;
            const expected = 1;
            // Act
            const actual = calculator.Power(a, b);
            // Assert
            expect(actual).toBe(expected);
        });
        test('Given a base and a negative exponent When calculating power Then the result should be one divided by the base raised to the absolute of the exponent', () => {
            // Arrange
            const a = 2;
            const b = -3;
            const expected = 0.125;
            // Act
            const actual = calculator.Power(a, b);
            // Assert
            expect(actual).toBeCloseTo(expected);
        });
        test('Given a base of zero and a positive exponent When calculating power Then the result should be zero', () => {
            // Arrange
            const a = 0;
            const b = 3;
            const expected = 0;
            // Act
            const actual = calculator.Power(a, b);
            // Assert
            expect(actual).toBe(expected);
        });
        test('Given a floating-point base and exponent When calculating power Then the result should be the base raised to the exponent', () => {
            // Arrange
            const a = 2.5;
            const b = 2.0;
            const expected = 6.25;
            // Act
            const actual = calculator.Power(a, b);
            // Assert
            expect(actual).toBeCloseTo(expected);
        });
    });
});