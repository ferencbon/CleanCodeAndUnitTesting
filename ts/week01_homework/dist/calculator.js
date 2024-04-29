"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Calculator = void 0;
class Calculator {
    Add(a, b) {
        return a + b;
    }
    Subtract(a, b) {
        return a - b;
    }
    Multiply(a, b) {
        return a * b;
    }
    Divide(a, b) {
        if (b === 0) {
            throw new Error("Division by zero is not allowed.");
        }
        return a / b;
    }
    Sqrt(a) {
        if (a < 0) {
            throw new Error("Square root of a negative number is not allowed.");
        }
        return Math.sqrt(a);
    }
    Power(a, b) {
        return Math.pow(a, b);
    }
}
exports.Calculator = Calculator;
